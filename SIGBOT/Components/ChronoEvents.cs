using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SIGBOT.Components
{
    public class ChronoEvents
    {
        public Dictionary<int, Action> events = new Dictionary<int, Action>();

        public static DateTime now = DateTime.Now;
        int lastDay = 0;
        Action lastPlayedEvent = null;
        List<DayOfWeek> skippedDays = new List<DayOfWeek>();

        public void RegisterAtTime(string time, Action deleg)
        {
            var dTime = DateTime.Parse(time);
            var seconds = dTime.Second + dTime.Minute * 60 + dTime.Hour * 3600;

            events[seconds] = deleg;
        }

        public bool ClearAtTime(string time)
        {
            var dTime = DateTime.Parse(time);
            var seconds = dTime.Second + dTime.Minute * 60 + dTime.Hour * 3600;
            var here = events.ContainsKey(seconds);
            if (here)
            {
                events.Remove(seconds);
                return true;
            }
            return false;
        }

        public void SkipDay(string day)
        {
            DayOfWeek weekDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day);
            skippedDays.Add(weekDay);
        }

        public void Clear()
        {
            events.Clear();
            skippedDays.Clear();
        }

        public void CheckDate()
        {   
            now = DateTime.Now;
            //now = now.AddHours(1);
            
            Console.WriteLine("===================== {0} {1} {2} =====================".Interpolate(Second().ToString(), now.Day.ToString(), lastDay.ToString()));
            if (now.Day != lastDay)
            {
                ResetLastPlayedEvent();
            }

            if (skippedDays.Contains(now.DayOfWeek))
            {
                Console.WriteLine(now.DayOfWeek + " is skipped.");
                return;
            }

            var curr = LastAvailableEvent();
            Console.WriteLine("Current event: [{0}]".Interpolate(curr != null ? curr.GetHashCode().ToString() : "NULL"));
            if (curr != lastPlayedEvent && curr != null)
            {
                Console.WriteLine("RUNNING CURR and setting it as last event");
                lastPlayedEvent = curr;
                Task.Run(curr);
            }
        }

        Action LastAvailableEvent()
        {
            if (events.Count == 0)
                return null;

            var lastTime = -1;
            foreach (var time in events.Keys)
            {
               Console.WriteLine("Checking event at time {0} for second {1}...".Interpolate(time.ToString(), Second().ToString()));

                if (time < Second())
                    lastTime = time;

                else
                    break;
            }

            if (lastTime == -1)
                return null;

            Console.WriteLine("Returning event for time {0}".Interpolate(lastTime.ToString()));
            
            return events[lastTime];

        }

        void ResetLastPlayedEvent()
        {
            lastDay = now.Day;
            lastPlayedEvent = null;
            Console.WriteLine("Reset last event to NULL");
        }

        int Second()
        {
            return now.Second + now.Minute * 60 + now.Hour * 3600;
        }
    }
}
