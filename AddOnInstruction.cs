using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public class AddOnInstruction : L5KComponent, IBuildable
    {
        public AddOnInstruction(List<string> content)
            :base(content, "ADD_ON_INSTRUCTION_DEFINITION")
        {
            _initializer = "ADD_ON_INSTRUCTION_DEFINITION";
        }
    }
}
