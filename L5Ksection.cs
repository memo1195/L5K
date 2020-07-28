using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public abstract class L5Ksection : IAcquire
    {
        //This class is intended to get the entire section of a L5K file (ex: Programs Section)
        //this class is intended to be abstract since there will be a class for each type of section
        //(ex: ProgramsSection class, TagsSection class, ModulesSection class, IntroSection class, etc.)
        protected readonly List<string> _content;
        protected Dictionary<string, int[]> _names;
        protected string _initializer;

        public L5Ksection(List<string> content,int startSec,int endSec)
        {            
            int contentLength = endSec - startSec;
            _content = content.GetRange(startSec,contentLength);
            //_content.FindAllIndex(x => x.StartsWith(_initializer));//probably will have to user this in a
            //child class
        }

        public abstract List<string> Acquire();

    }
}
