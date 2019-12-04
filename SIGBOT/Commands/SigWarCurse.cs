using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using SIGBOT.Components.War.Rules;
using static SIGBOT.Components.War.Map;

namespace SIGBOT.Commands
{
    class SigWarCurse : Command
    {
		public DiscordMessage status;

		public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {

            if (args.Length < 1)
            {
                await message.RespondAsync("You need to supply a target for the curse.\nNo curse was cast.");
                return;
            }
            
            if (Program.game != null && Program.game.rule is ICurseable) {
                var cursePools = Program.game.map.cursePools;
                var curseRule = (ICurseable)Program.game.rule;
                if (curseRule.ShouldResetCurses())
                {
					cursePools.Clear();
                    curseRule.DeclareCursesResetted();
					status = null;
                }

                string givenName = string.Join(' ', args).Replace(" ", string.Empty);

                var team = Program.game.map.teams.Find(o => o.name.Replace(" ", string.Empty).Equals(givenName, StringComparison.OrdinalIgnoreCase));
                if (team == null)
                {
                    await message.RespondAsync("I couldn't find the team [" + args[0] + "].\nPlease fire >sigwarteams to get a list of keywords.\nNo curse was cast.");
                    return;
                }

                byte target = team.id;

                if (cursePools.HasCastCurse(user.Id))
                {
                    await message.RespondAsync("You have already cast a curse for the next battle.\nYou can no longer cast a curse at the moment.\nNo curse was cast.");
                    return;
                }

                if (team.territory.Count <= 0)
                {
                    await message.RespondAsync("It is no good to curse on the dead. 💀\nNo curse was cast.");
                    return;
                }

                curseRule.AddCurse(target);
                cursePools.AddCurse(team.id, user.Id);


                await message.RespondAsync("The curse on `"+ team.name + "` is cast!\nMay their fate be terrible and their life short.");

				if(status == null)
					status = await Program.game.channel.SendMessageAsync(GetCurseStatus(bot));
				else
					await status.ModifyAsync(GetCurseStatus(bot));

            }
            else
            {
                await message.RespondAsync("The game is not running, or the current ruleset does not support a Curse command.");
            }
        }

		public string GetCurseStatus(Bot bot)
        {
            var curseRule = (ICurseable)Program.game.rule;
            StringBuilder text = new StringBuilder();
			text.Append("Curses: ");
			text.AppendLine();
			foreach (var pool in Program.game.map.cursePools)
			{
                var teamByte = pool.teamId;

				text.Append(string.Format("**{0}**", Program.game.map.teams[teamByte].name));
				text.Append(": ");
				foreach (ulong u in pool.cursers)
				{
					text.Append(GetEmoji(u, (id) => { return DiscordEmoji.FromGuildEmote(bot.client, id); }));
				}
				text.AppendLine();
			}
			return text.ToString();
		}

		public string GetEmoji(ulong user, Func<ulong, DiscordEmoji> fetchingFunction)
		{
            try {
                if (Program.game.map.emblems.HasEmblem(user)) {
                    var emoji = Program.game.map.emblems.GetEmblem(user);
                    return emoji.character;
                }
            }
            catch (Exception e){
                Log.Warn("Could not fetch stored emoji for user " + user);
                Log.Warn(e.ToString());
            }


            if (user % 5 == 0) return "☄";
            else if (user % 3 == 0) return "⚡";
            else if (user % 2 == 0) return "🌟";
            else return "🌠";
		}
    }
}
