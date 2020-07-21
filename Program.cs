using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public class Program:L5KComponent,IBuildable
    {
        public Program(string name, List<string> content)
            :base(name,content,"PROGRAM")
        {

        }
    }
}
