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
            client.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                    await e.Message.RespondAsync("pong!");
            };

        }
    }
}
