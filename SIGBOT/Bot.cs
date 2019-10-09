using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using SIGBOT.Components;

namespace SIGBOT
{
    public class Bot
    {
        public const char prefix = '>';

        public readonly DiscordClient client;
        public readonly Controller controller = new Controller();
        public readonly RoleOnReact roleOnReact = new RoleOnReact();
        public readonly ChronoEvents chronoEvents = new ChronoEvents();

        public readonly Task dateCheckInterval;

        public Bot(string token)
        {
            client = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            RegisterEvents(client);

            dateCheckInterval = Task.Run(async () => {
                for (; ; )
                {
                    await Task.Delay(1000);
                    chronoEvents.CheckDate();
                }
            });

            Run().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        async Task Run()
        {
            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        void RegisterEvents(DiscordClient client)
        {
            client.MessageReactionAdded += async react =>
            {
                if (react.Message == roleOnReact.message)
                {
                    var users = new List<DiscordMember>(await react.Message.Channel.Guild.GetAllMembersAsync());
                    var reacter = users.Find(o => o.Id == react.User.Id);
                    var emoji = react.Emoji;

                    if (roleOnReact.ContainsKey(emoji)) {
                        await reacter.GrantRoleAsync(roleOnReact[emoji]);
                    }
                }
            };

            client.MessageReactionRemoved += async react =>
            {
                if (react.Message == roleOnReact.message)
                {
                    var users = new List<DiscordMember>(await react.Message.Channel.Guild.GetAllMembersAsync());
                    var reacter = users.Find(o => o.Id == react.User.Id);
                    var emoji = react.Emoji;

                    if (roleOnReact.ContainsKey(emoji))
                    {
                        await reacter.RevokeRoleAsync(roleOnReact[emoji]);
                    }
                }
            };

            client.MessageCreated += async messageCreate =>
            {
                foreach (var content in messageCreate.Message.Content.Split("\n"))
                {
                    if (!content.StartsWith(prefix))
                    {
                        return;
                    }

                    var parts = content.ToLower().Trim().Remove(0, 1).Split(" ");
                    var command = parts[0];

                    // +1 because the prefix is 1 character long
                    var args = content.Remove(0, command.Length + 1).Trim().Split(" ");
                    if (controller.ContainsKey(command))
                    {
                        try
                        {
                            await controller[command].Execute(
                                bot: this,
                                user: messageCreate.Author,
                                message: messageCreate.Message,
                                args: args
                            );
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            Console.WriteLine("Wrong number of arguments for command: " + command + "(" + (args.Length) + ")\n" + e);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine("Internal error for command: " + command + "(" + (args.Length) + ")\n" + e);
                        }
                    }
                }
            };

        }
    }
}
