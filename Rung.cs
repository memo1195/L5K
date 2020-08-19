using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public struct Rung
    {
        public string Comment { get; }
        public string Content { get; }
        public bool HasComment { get; }


        public Rung(string comment, string content)
        {
            HasComment = true;
            Comment = comment;
            Content = content;
        }
        public Rung(string content)
        {
            HasComment = false;
            Comment = "";
            Content = content;
        }

        
    }
}
