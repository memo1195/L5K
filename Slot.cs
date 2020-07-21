﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    class Slot : LineComponent, IBuildable
    {
        public Slot(List<string> content)
            : base(content,"Slot := ")
        {

        }
    }
}
