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

        int lastDay = 0;
        Action lastPlayedEvent = null;

        public void RegisterAtTime(string time, Action deleg)
        {
            var dTime = DateTime.Parse(time);
            var seconds = dTime.Second + dTime.Minute * 60 + dTime.Hour * 3600;
          //  Console.WriteLine("Registered events at " + seconds + " seconds");
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

        public void Clear()
        {
            events.Clear();
        }

        public void CheckDate()
        {
           // Console.WriteLine("===================== {0} =====================".Format(Second().ToString()));
            if (DateTime.Now.Day != lastDay)
                ResetLastPlayedEvent();

            var curr = LastAvailableEvent();
           // Console.WriteLine("Current event: [{0}]".Format(curr != null ? curr.GetHashCode().ToString() : "NULL"));
           // Console.WriteLine("Last event: ");
           // Console.Write(lastPlayedEvent);
            if (curr != lastPlayedEvent && curr != null)
            {
                //Console.WriteLine("RUNNING CURR and setting it as last event");
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
               // Console.WriteLine("Checking event at time {0} for second {1}...".Format(time.ToString(), Second().ToString()));

                if (time < Second())
                    lastTime = time;

                else
                    break;
            }

            if (lastTime == -1)
                return null;

            //Console.WriteLine("Returning event for time {0}".Format(lastTime.ToString()));

            return events[lastTime];

        }

        void ResetLastPlayedEvent()
        {
            lastDay = DateTime.Now.Day;
            lastPlayedEvent = null;
            //Console.WriteLine("Reset last event to NULL");
        }

        int Second()
        {
            return DateTime.Now.Second + DateTime.Now.Minute * 60 + DateTime.Now.Hour * 3600;
        }
    }
}
