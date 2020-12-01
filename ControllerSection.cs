using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public class ControllerSection : L5Ksection, IAcquire
    {
        private Controller _mainController;

        public string ControllerName 
        {
            get
            {
                return _mainController.OriginalName;
            }
        }

        public ControllerSection(L5KReadStream readings)
            :base(readings,readings.ControllerSecStart,readings.ControllerSecEnd)
        {
            _initializer = readings.ControllerInit;
            _mainController = new Controller(_content);
        }


        public override List<string> Acquire()
        {
            return _content;
        }
    }
}
