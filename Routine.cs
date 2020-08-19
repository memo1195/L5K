using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public class Routine : L5KComponent
    {
        private List<Rung> _rungs;

        public Routine(string name, List<string> content)
            :base(name,content,"ROUTINE")
        {

        }

        public Routine(List<string> content)
            :base(content, "ROUTINE")
        {

        }
    }
}
