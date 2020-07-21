using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public abstract class L5Ksection : IAcquire
    {
        protected readonly List<object> _content;
        public L5Ksection(List<object> content,int startSec,int endSec)
        {            
            int contentLength = endSec - startSec;
            _content = content.GetRange(startSec,contentLength);
        }

        public abstract void Acquire();

    }
}
