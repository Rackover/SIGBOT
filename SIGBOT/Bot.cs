﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using Newtonsoft.Json;
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
            Log.Info("Running!");
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

            // Update role color postiion
            client.GuildRoleCreated += async roleCreate => {

                var assoc = new ColoredRoleAssociations();
                var path = "out/colors.json";
                if (File.Exists(path)) {
                    while (true) {
                        try {
                            assoc = JsonConvert.DeserializeObject<ColoredRoleAssociations>(File.ReadAllText(path));
                            break;
                        }
                        catch (IOException e) {
                            Log.Warn("Waiting for file lock READ on " + path + "\n" + e.ToString());
                            await Task.Delay(100);
                        }
                    }
                }

                var isOurs = assoc.FindAll(o => o.roleId == roleCreate.Role.Id).Count > 0;
                if (isOurs) {
                    var me = await roleCreate.Guild.GetMemberAsync(client.CurrentUser.Id);
                    var myRole = me.Roles.OrderByDescending(o => o.Position).First();
                    var index = myRole.Position - 1;
                    await me.Guild.UpdateRolePositionAsync(roleCreate.Role, index);
                    Log.Trace("Put role " + roleCreate.Role.Name + ":" + roleCreate.Role.Id + " to position " + index);
                }
            };

            client.ClientErrored += Client_ClientErrored;

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
                        Log.Trace(messageCreate.Author.Username + "#" + messageCreate.Author.Discriminator + ":" + messageCreate.Author.Id + " said: " + messageCreate.Message.Content);
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
                            Log.Warn("Wrong number of arguments for command: " + command + "(" + (args.Length) + ")\n" + e);
                        }
                        catch (ArgumentException e)
                        {
                            Log.Warn("Internal error for command: " + command + "(" + (args.Length) + ")\n" + e);
                        }
                    }
                }
            };

        }

        private async Task Client_ClientErrored(DSharpPlus.EventArgs.ClientErrorEventArgs e)
        {
            Log.Err(e.Exception.ToString());
            
        }
    }
}
