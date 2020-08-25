﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public class Tag : L5KComponent, IBuildable
    {
        private string _originalAliasOrDT;

        //private string[] _separator;

        public string AliasOrDT { get; private set; }
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

        public override void Build()
        {
            base.Build();
            _content[0] = _content[0].Replace(_originalAliasOrDT,AliasOrDT);
        }

    }
}
