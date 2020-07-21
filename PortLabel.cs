using System.Collections.Generic;

namespace L5K
{
    class PortLabel : LineComponent
    {
        public bool IsParentBackplane
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
