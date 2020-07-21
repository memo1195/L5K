using System.Collections.Generic;

namespace L5K
{
    class PortLabel : LineComponent, IBuildable
    {
        public bool InChassis
        {
            get
            {
                return Attribute == "RxBACKPLANE";
            }
        }

        public PortLabel(List<string> content)
            : base(content,"PortLabel := ")
        {

        }
    }
}
