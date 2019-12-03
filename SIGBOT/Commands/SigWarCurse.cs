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
		public Dictionary<byte, List<DiscordUser>> cursePools = new Dictionary<byte, List<DiscordUser>>();
		public DiscordMessage status;

		public override async Task Execute(Bot bot, DiscordUser user, DiscordMessage message, string[] args)
        {
            if (args.Length < 1)
            {
                await message.RespondAsync("You need to supply a target for the curse.\nNo curse was cast.");
                return;
            }

            if (Program.game != null && Program.game.rule is OneTakeRandomStreakCurse)
            {
                var curseRule = (OneTakeRandomStreakCurse)Program.game.rule;
                if (curseRule.canResetCurseGivers)
                {
					cursePools.Clear();
                    curseRule.canResetCurseGivers = false;
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
                

                if (!curseRule.curses.ContainsKey(target))
                    curseRule.curses[target] = 0;

                if (HasAlreadyCursed(user))
                {
                    await message.RespondAsync("You have already cast a curse for the next battle.\nYou can no longer cast a curse at the moment.\nNo curse was cast.");
                    return;
                }

                if (team.territory.Count <= 0)
                {
                    await message.RespondAsync("It is no good to curse on the dead. 💀\nNo curse was cast.");
                    return;
                }

                curseRule.curses[target] += 5; //Added one curse 

				// IF TEAM IS ALREADY CURSED
				if (!cursePools.ContainsKey(team.id)) cursePools[team.id] = new List<DiscordUser>();
				cursePools[team.id].Add(user);

                await message.RespondAsync("The curse on `"+ team.name + "` is cast!\nMay their fate be terrible and their life short.");

				if(status == null)
					status = await Program.game.channel.SendMessageAsync(GetCurseStatus(bot));
				else
					await status.ModifyAsync(GetCurseStatus(bot));

				curseRule.curses.Clear();
				foreach (var teamByte in cursePools.Keys)
				{
					curseRule.curses[teamByte] = (cursePools[teamByte].Count);
				}
			}
            else
            {
                await message.RespondAsync("The game is not running, or the current ruleset does not support a Curse command.");
            }
        }

		public string GetCurseStatus(Bot bot)
		{
			StringBuilder text = new StringBuilder();
			text.Append("Curses: ");
			text.AppendLine();
			foreach (var teamByte in cursePools.Keys)
			{
				text.Append(string.Format("**{0}**", Program.game.map.teams[teamByte].name));
				text.Append(": ");
				foreach (DiscordUser u in cursePools[teamByte])
				{
					text.Append(GetEmoji(u, (id) => { return DiscordEmoji.FromGuildEmote(bot.client, id); }));
				}
				text.AppendLine();
			}
			return text.ToString();
		}

		public string GetEmoji(DiscordUser user, Func<ulong, DiscordEmoji> fetchingFunction)
		{
            try {
                if (Program.game.map.emblems.ContainsKey(user.Id)) {
                    Log.Trace("Getting emblem " + Program.game.map.emblems[user.Id]+"");
                    var emoji = Program.game.map.emblems[user.Id];
                    return emoji;
                }
            }
            catch (Exception e){
                Log.Warn("Could not fetch stored emoji for user " + user.Username);
                Log.Warn(e.ToString());
            }


            if (user.Id % 5 == 0) return "☄";
            else if (user.Id % 3 == 0) return "⚡";
            else if (user.Id % 2 == 0) return "🌟";
            else return "🌠";
		}

		public bool HasAlreadyCursed(DiscordUser user)
		{
			foreach (KeyValuePair<byte, List<DiscordUser>> c in cursePools)
			{
				foreach(DiscordUser u in c.Value)
					if (u == user) return true;
			}
			return false;
		}
    }
}
