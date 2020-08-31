using System.Collections.Generic;

namespace L5K
{
    public class Rung
    {
        private List<string> _comment;
        private List<string> _content;
        private List<Alarm> _alarmList;
        public static string RCInit
        {
            get
            {
                return "				RC:";
            }
        }
        public static string NInit
        {
            get
            {
                return "				N:";
            }
        }

        public string[] Comment
        {
            get
            {
                return _comment.ToArray();
            }
        }
        //public string Content { get; }maybe wont be using this
        public bool HasComment { get; }
        public bool HasAlarm
        {
            get
            {
                return _alarmList.Count > 0;
            }
        }

        /*public Rung(List<string> comment, string content)
        {
            _comment = comment;
            Content = content;
            FindAlarms();
        }*/

        public Rung(List<string> content)
        {
            //This will be the only constructor, Routine will identify each rung and rung class will take care of 
            //identifying if there is a rung comment or not and separate them.            
            _content = new List<string>();
            _comment = new List<string>();
            HasComment = content.Contains(RCInit);
            var viewingComment = HasComment;
            //If the code finds an RC: then it will add the content to _comment List, else it will add it will identify
            //that it is not reading a comment and then will be adding the content to _content which is the actual logic of the rung
            for (var i = 0; i < content.Count; i++)
            {
                if (!content[i].StartsWith(NInit) && viewingComment)
                    _comment.Add(content[i]);
                else
                {
                    viewingComment = false;
                    _content.Add(content[i]);
                }
            }
            if (HasComment)
                FindAlarms();

            //Content = content;
        }

        private void FindAlarms()
        {
            //This is an algorithm that checks the content of the RC and identifies the number of alamrs it has,
            //gets them in an array with their Index number and adds them to a List of Alarms
            var numberofAlarms = _comment.FindAllIndex(x => x.StartsWith(Alarm.AlarmInit)).ToArray();
            if (numberofAlarms.Length > 0)
                _alarmList = new List<Alarm>();
            foreach (var alarmIndex in numberofAlarms)
            {
                _alarmList.Add(new Alarm(_comment[alarmIndex]));
            }
        }


    }
}
