using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    class ControllerSection : L5Ksection, IAcquire
    {
        public ControllerSection(L5KReadStream readings)
            :base(readings,readings.ControllerSecStart,readings.ControllerSecEnd)
        {
            _initializer = readings.ControllerInit;
        }

        public override List<string> Acquire()
        {
            return _content;
        }
    }
}
