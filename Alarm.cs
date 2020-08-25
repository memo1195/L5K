using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public struct Alarm
    {
        public static string AlarmInit { 
            get 
            {
                return "\"#     <Alarm[";
            } 
        } 
        public string AlarmComment { get; private set; }
        public int Index { 
            get 
            {
                var index = AlarmComment.IndexOf(']');
                var length = AlarmInit.Length - index;
                return Int32.Parse(AlarmComment.Substring(AlarmInit.Length, --length));
            }
            set
            {
                string change = $"[{Index}]";
                AlarmComment = AlarmComment.Replace(change, value.ToString());
            }
        }

        public Alarm(string comment)
        {
            AlarmComment = comment;

        }

    }
}
