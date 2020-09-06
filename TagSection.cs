using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    class TagSection : L5Ksection, IAcquire
    {
        //TagSection should be implemented once the program section has been implemented to find all the necessary
        //alarms
        private HashSet<Tag> _needed;
        private bool _localTagSection;

        public TagSection(L5KReadStream readings)
            : base(readings,readings.TagSecStart,readings.TagSecEnd)
        {
            _needed = new HashSet<Tag>();
            _initializer = readings.TagInit;
            _localTagSection = false;
        }

        public TagSection(List<string> content)
            :base(content)
        {
            //This constructor will be used with local tags only
            _needed = new HashSet<Tag>();
            //This gets all the lines in which a tag is delclared and adds it to a List of Tags
            var tagIndexes=_content.FindAllIndex(x => x.StartsWith("			") && !x.StartsWith("				"));
            _localTagSection = true;
            for (var i = 0; i < tagIndexes.Count; i++)
            {
                var index = tagIndexes[i];
                if (i + 1 != tagIndexes.Count) 
                {
                    var length = tagIndexes[i + 1] - tagIndexes[i];
                    _needed.Add(new Tag(_content.GetRange(tagIndexes[index], ++length)));
                }
                else
                {
                    var lastIndex= _content.FindIndex(x => x.Contains("END_" + content[content.Count - 1].Trim()));
                    var length = tagIndexes[i] - lastIndex;
                    _needed.Add(new Tag(_content.GetRange(tagIndexes[index],length)));
                }                 
            }
        }

        public void AddTags(List<Program> programs)
        {
            //This should have an algorithm to find all the tags in the programs, this to get the global tags
            //local tags should be gathered with the constructor of Program(content)
            //This method should read all the local tags in the program and get the ones that have an Alias
            //Also this should compare the tag list I have in Rung class that lists all the tags found in logic
            //This is done to discar missing any tags that are used as global 09/05/2020
            foreach(var program in programs)
            {
                
            }
        }

        public override List<string> Acquire()
        {
            //Not sure if this is ok, maybe it will also be necessary to get the list of DTs
            return _needed.Select(x => x.Name).ToList();
        }
    }
}
