using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;

namespace SIGBOT
{
    class Bot
    {
        DiscordClient client;
        Controller controller = new Controller();

        public Bot(string token)
        {
            client = new DiscordClient(new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot
            });

            RegisterEvents(client);

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
                Console.WriteLine("reaction!");
                Console.WriteLine(react.User.Presence.Guild);
            };

            client.MessageCreated += async messageCreate =>
            {
                var command = messageCreate.Message.Content.ToLower();
                if (controller.ContainsKey(command))
                {
                    await controller[command].Execute(
                        messageCreate.Author,
                        messageCreate.Message,
                        messageCreate.Message.Content.Remove(0, command.Length).Split(" ")
                    );
                }
            };

        }
    }
}
