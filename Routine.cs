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
        private string _rcInit;
        private string _nInit;

        public Routine(string name, List<string> content)
            :base(name,content,"ROUTINE")
        {
            DefineConstants();
        }

        public Routine(List<string> content)
            :base(content, "ROUTINE")
        {
            DefineConstants();
        }

        private void DefineConstants()
        {
            _rcInit=Rung.RCInit;
            _nInit =Rung.NInit;
        }
        private void GetRungs()
        {
            
        }
    }
}
