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
            :base(name,content,"ROUTINE ")
        {
            DefineConstants();
            SetRungs();
        }

        public Routine(List<string> content)
            :base(content, "ROUTINE ")
        {
            DefineConstants();
            SetRungs();
        }

        private void DefineConstants()
        {
            _rcInit=Rung.RCInit;
            _nInit =Rung.NInit;
        }
        private void SetRungs()
        {
            _rungs = new List<Rung>();
            var nIndex = _content.IndexOf(_nInit);
            var rcIndex = _content.IndexOf(_rcInit);
            var startIndex = nIndex < rcIndex || rcIndex == -1 ? nIndex : rcIndex;//This identifies which appears first in the routine if a rung comment or logic
            for(var i = startIndex; i < _content.Count; i++)
            {
                i = _content.IndexOf(_nInit, i);
                var length = i + 1 - startIndex;
                _rungs.Add(new Rung(_content.GetRange(startIndex, length)));
                startIndex = i + 1;
            }
        }

        public IEnumerable<string> GetTags() 
        {
            var output = new HashSet<string>();
            foreach(var rung in _rungs)//Gets Tags in other TagList, appends them and then returns it as one Hashset
            {
                output.UnionWith(rung.GetTags());
            }
            return output;
        }
    }
}
