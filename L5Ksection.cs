using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public abstract class L5Ksection : IAcquire
    {
        //This class is intended to get the entire section of a L5K file (ex: Program Section)
        protected readonly List<object> _content;//content this list, still neeeds to be defined
        private List<string> _names;

        public L5Ksection(List<object> content,int startSec,int endSec)
        {            
            int contentLength = endSec - startSec;
            _content = content.GetRange(startSec,contentLength);
            _names = new List<string>();
        }

        public abstract List<string> Acquire();

    }
}
