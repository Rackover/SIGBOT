using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace SIGBOT.Commands
{
    class SigWarTeams : Command
    {
        public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            List<string> keywords = new List<string>();
            foreach(var team in Program.game.map.teams)
            {
                keywords.Add(team.name);
            }

            await message.RespondAsync("List of the SIGWAR teams keywords:\n```" + string.Join('\n', keywords)+"```");
        }
    }
}
