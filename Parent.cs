using System.Collections.Generic;

namespace L5K
{

    class Parent : LineComponent, IBuildable
    {

        public Parent(List<string> content)
            :base(content,"Parent := ")
        {

        }
    }
}
