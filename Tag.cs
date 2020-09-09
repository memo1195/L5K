using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace L5K
{
    public class Tag : L5KComponent, IBuildable
    {
        private string _originalAliasOrDT;

        public string AliasOrDT { get; set; }

        public bool IsAlias { get; private set; }
        

        public Tag(string name, List<string> content)
            :base(name,content, "		")//this needs adjustments
        {
            //_separator = new string[] { " : ", " OF " };
            _GetAliasOrDT();           
        }

        public Tag(List<string> content)
            : base(content, "		")//this needs adjustments
        {
            //_separator = new string[] { " : ", " OF " };
            _GetAliasOrDT();
        }

        private void _GetAliasOrDT()
        {
            var separator = new string[] { " : ", " OF " };
            IsAlias = _content[0].Contains(separator[1]);
            if (IsAlias)
                _originalAliasOrDT = _GetOriginalName(separator[1]);
            else
                _originalAliasOrDT = _GetOriginalName(separator[0]);
        }

        public string[] GetTagRelation()
        {
            return new string[] { _originalAliasOrDT, AliasOrDT };
        }

        public override void Build()
        {
            base.Build();
            _content[0] = _content[0].Replace(_originalAliasOrDT, AliasOrDT);
        }

        public List<Match> GetTagComments()
        {
            //@"\b(?<type>[F|M|W][B])(?<idnum>\d)\[(?<index>\d{1,3})]\.(?<bit>\d{1,2})" for excel
            //@"\bCOMMENT\[(?<index>\d{1,3})]\.(?<bit>\d{1,2}) := "(?<comment>.*)"," for L5K
            var commentRegex = new Regex(@"\bCOMMENT\[(?<index>\d{1,3})]\.(?<bit>\d{1,2}) := ""(?<comment>[^""]*)"",");
            var testable = string.Join(" ", _content);
            var output = new List<Match>();
            foreach(var line in _content)
            {
                output.Add(commentRegex.Match(line));
            }
            return output;

        }

    }
}
