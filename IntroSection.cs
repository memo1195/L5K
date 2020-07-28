using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    class IntroSection : L5Ksection, IAcquire
    {
        public IntroSection(L5KReadStream readings):
            base(readings,readings.BegginingSecStart,readings.BegginingSecEnd)
        {

        }
        public override List<string> Acquire()
        {
            return _content;
        }
    }
}
