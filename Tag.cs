using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    class Tag : L5KComponent 
    {
        private string _originalAliasOrDT;

        private string[] _separator;

        public string AliasOrDT { get; private set; }
        public bool IsAlias { get; private set; }
        
        public Tag(string name, List<string> content)
            :base(name,content, "		")
        {
            _separator = new string[] { " : ", " OF " };
            _GetAliasOrDT();           
        }
        private void _GetAliasOrDT()
        {
            IsAlias = _content[0].Contains(_separator[1]);
            if (IsAlias)
                _originalAliasOrDT = _GetOriginalName(_separator[1]);
            else
                _originalAliasOrDT = _GetOriginalName(_separator[0]);
        }

    }
}
