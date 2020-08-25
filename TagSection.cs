using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    class TagSection : L5Ksection, IAcquire
    {
        //TagSection should be implemented once the program section has been implemented to find all the necessary
        //alarms
        

        public TagSection(L5KReadStream readings)
            : base(readings,readings.TagSecStart,readings.TagSecEnd)
        {

        }



        public override List<string> Acquire()
        {
            throw new NotImplementedException();
        }
    }
}
