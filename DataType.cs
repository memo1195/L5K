using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{

    public class DataType : L5KComponent, IBuildable
    {

        public DataType(List<string> content)
            :base(content, "DATATYPE")
        {
        }

    }
}
