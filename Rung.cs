using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace L5K
{
    public class Rung
    {
        private List<string> _comment;
        private List<string> _content;
        private List<Alarm> _alarmList;
        private HashSet<string> _tags;//changed to string Hashset because this will not be reading the tags from the tag section, this is reading it from the logic and 
                                      //only delivers the name of the Tag, not the content of it
        private List<string> _specialcasesList;//This is a list of Studio5000 Methods that dont necesarily implement a Tag
        private Regex _parametersRegex;
        private readonly string _action;
        private readonly string _parameters;
        private Regex _tagFinderRegex;
        private readonly string _tagName;

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
            _tags = new HashSet<string>();
            _specialcasesList = new List<string>() { "GSV", "SSV", "JSR" };
            _parametersRegex = new Regex(@"(?<action>[A-Z]{3,5})(?<parenthesis>\((?<params>[\w.,\[\]\?\+\-\#]*)\))");
            //This regex will be used to find functions and their parenthesis content
            _action = "action";
            _parameters = "params";
            _tagFinderRegex = new Regex(@"\b(?<tagname>[A-Za-z_][\w]*)((?<limiter>([.]|[\[]\d*]))(?<remain>[A-Za-z][\w]*))*");
            _tagName = "tagname";
            DefineTags();
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
            foreach (var line in _content)//Gets all the content and makes it a single line string;
            {
                testableString += line;
            }
            var matches = _parametersRegex.Matches(testableString);
            foreach (Match match in matches)
            {
                var actionType = match.Groups[_action].Value;
                var parameter = match.Groups[_parameters].Value;
                var tagMatches = _tagFinderRegex.Matches(parameter);
                if (_specialcasesList.Contains(actionType))
                {
                    if(actionType==_specialcasesList[0]|| actionType == _specialcasesList[1])
                        _tags.Add(tagMatches[tagMatches.Count-1].Groups[_tagName].Value);//This only gets the last value acquired
                                                                                         //Since SSV and GSV only use tags in the last parameter                   
                    else if (actionType == _specialcasesList[2])//used an else if in case I want to add more instructions later
                        //This are the instructions of a JSR, this instructions uses a program name as the first parameter and after that it uses only tags
                        //so this ignores only the first values
                    {
                        for(var i = 1; i < tagMatches.Count; i++) 
                        {
                            _tags.Add(tagMatches[i].Groups[_tagName].Value);
                        }
                    }
                        
                }
                else
                {                    
                    foreach (Match tag in tagMatches)
                    {
                        _tags.Add(tag.Groups[_tagName].Value);
                    }
                    //This needs an algorithm to separate parameters making a substring from commas, dots and '[' to get only the main tag name
                }
            }
        }
        //only for GM Alarms, probably will remove this from library and add it as an extention from other library specialized for GM programs
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
