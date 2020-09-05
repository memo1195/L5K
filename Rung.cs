using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace L5K
{
    public class Rung
    {
        private List<string> _comment;
        private List<string> _content;
        private List<Alarm> _alarmList;
        private HashSet<Tag> _tags;
        private List<string> _specialcasesList;//This is a list of Studio5000 Methods that dont necesarily implement a Tag
        private Regex _regex;
        private readonly string _action;
        private readonly string _parameters;

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
            _alarmList = new List<Alarm>();
            _specialcasesList = new List<string>() { "GSV", "SSV" };
            _regex = new Regex(@"(?<action>[A-Z]{3})(?<parenthesis>\((?<params>[\w.,\[\]\?\+\-\#]*)\))");
            _action = "action";
            _parameters = "params";
            //This regex will be used to find functions and their parenthesis content

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
        private void DefineTags()
        {
            var testableString = "";//This will carry all the  content of N: and be tested in a Regex
            foreach(var line in _content)//Gets all the content and makes it a single line string;
            {
                testableString += line;
            }
            var matches=_regex.Matches(testableString);
            foreach(Match match in matches)
            {
                if (_specialcasesList.Contains(match.Groups[_action].Value)){
                    //This will contain the algorithm to retrieve the Tags in special cases 09/05/2020
                }
                else
                {
                    //var params=match.Groups[_parameters].Value;
                    //This needs an algorithm to separate parameters making a substring from commas, dots and '[' to get only the main tag name
                }
            }
        }

        private void FindAlarms()
        {
            //This is an algorithm that checks the content of the RC and identifies the number of alamrs it has,
            //gets them in an array with their Index number and adds them to a List of Alarms
            var numberofAlarms = _comment.FindAllIndex(x => x.StartsWith(Alarm.AlarmInit)).ToArray();
            foreach (var alarmIndex in numberofAlarms)
            {
                _alarmList.Add(new Alarm(_comment[alarmIndex]));
            }
        }


    }
}
