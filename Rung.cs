using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public class Rung
    {
        private List<string> _comment;

        private List<Alarm> _alarmList;

        public string[] Comment
        {
            get
            {
                return _comment.ToArray();
            }
        }
        public string Content { get; }
        public bool HasComment { get; }
        public bool HasAlarm { 
            get 
            {
                return _alarmList.Count > 0;
            } 
        }

        public Rung(List<string> comment, string content)
        {
            HasComment = true;
            _comment = comment;
            Content = content;
            FindAlarms();
        }

        public Rung(string content)
        {
            HasComment = false;
            _comment = new List<string>();
            Content = content;
        }

        private void FindAlarms()
        {
            //This is an algorithm that checks the content of the RC and identifies the number of alamrs it has,
            //gets them in an array with their Index number and adds them to a List of Alarms
            int[] numberofAlarms = _comment.FindAllIndex(x => x.StartsWith(Alarm.AlarmInit)).ToArray();
            if (numberofAlarms.Length > 0)
                _alarmList = new List<Alarm>();
            foreach(var alarmIndex in numberofAlarms)
            {
                _alarmList.Add(new Alarm(_comment[alarmIndex]));
            }
        }

        
    }
}
