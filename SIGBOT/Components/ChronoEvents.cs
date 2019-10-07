using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SIGBOT.Components
{
    public class ChronoEvents
    {
        public Dictionary<int, Action> events = new Dictionary<int, Action>();

        int lastDay = 0;
        Action lastEvent = null;

        public void RegisterAtTime(string time, Action deleg)
        {
            var dTime = DateTime.Parse(time);
            var seconds = dTime.Second + dTime.Minute * 60 + dTime.Hour * 3600;
            Console.WriteLine("Registered events at " + seconds + " seconds");
            events[seconds] = deleg;
        }

        public void Clear()
        {
            events.Clear();
        }

        public void CheckDate()
        {
            if (DateTime.Now.Day != lastDay)
                ResetCurrentEvent();

            var curr = CurrentEvent();
            if (curr != lastEvent && curr != null)
            {
                lastEvent = curr;
                Task.Run(curr);
            }
        }

        Action CurrentEvent()
        {
            var lastTime = 0;
            foreach(var time in events.Keys)
            {
                if (time > Second())
                {
                    return events[lastTime];
                }
                lastTime = time;
            }
            return null;
        }

        void ResetCurrentEvent()
        {
            lastDay = DateTime.Now.Day;
            lastEvent = null;
        }

        int Second()
        {
            return DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 3600;
        }
    }
}
