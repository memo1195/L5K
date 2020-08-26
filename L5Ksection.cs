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
        protected List<string> _content;
        protected string _initializer;
        protected string _projectName;
        //protected Dictionary<string, int[]> _names;
        
        public int Length
        {
            get
            {
                return _content.Count;
            }
        }

        public L5Ksection(L5KReadStream readings,int startSec,int endSec)
        {            
            _content = new List<string>();
            for (int i = startSec; i <= endSec; i++)
                _content.Add(readings[i]);
            //_content.FindAllIndex(x => x.StartsWith(_initializer));//probably will have to use this in a
            //child class
            _projectName = readings.ProjectName;
        }

        public L5Ksection(List<string> content)
        {
            //probably wont be using this, still maybe for local tags section this could be used
            _content=content.Clone().ToList();
            //_content = content.GetRange(startSec, contentLength);

        }

        protected string _GetNameBetweenElements(string line, int initIndex, char element)
        {
            line = line.Substring(initIndex);
            var originalNameLength = line.IndexOf(element);
            return line.Substring(0, originalNameLength);
        }

        public abstract List<string> Acquire();


        public string this[int key]
        {
            get
            {
                return _content[key];
            }
        }

    }
}
