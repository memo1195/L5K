using System.Collections.Generic;

namespace L5K
{
    class NodeAddress : LineComponent, IBuildable
    {
        public NodeAddress(List<string> content)
            : base(content,"NodeAddress := ")
        {

        }
    }
}
