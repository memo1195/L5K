using System.Collections.Generic;

namespace L5K
{
    public class Program : L5KComponent, IBuildable
    {
        //private readonly List<Tag> _localTags;
        private TagSection _localTags;
        private readonly List<Routine> _routines;

        public bool IsSafetyProgram { get; }

        public Program(string name, List<string> content)
            : base(name, content, "PROGRAM")
        {
            _routines = new List<Routine>();
            var localTagStartIndex = content.FindIndex(x => x.Contains("TAG"));
            var localTagEndIndex = content.FindIndex(x => x.Contains("END_TAG"));
            _localTags = new TagSection(content.GetRange(localTagStartIndex,++localTagEndIndex-localTagStartIndex));
            //Need to test this, and see if this should be implemented like this
            //_localTags = new List<Tag>();
            FindRoutines();
        }

        private void FindRoutines()
        {
            var rotuineInitializer = "  ROUTINE";
            var routineIndexes = _content.FindAllIndex(x => x.StartsWith(rotuineInitializer));
            for (var i = 0; i < routineIndexes.Count; i++)
            {
                var index = routineIndexes[i];
                if (i + 1 != routineIndexes.Count)
                {
                    var count = routineIndexes[i + 1] - index;
                    _routines.Add(new Routine(_content.GetRange(index, count)));
                }
                else
                {
                    var count = _content.Count - index;
                    _routines.Add(new Routine(_content.GetRange(index, count)));
                }
            }

        }

        public void AddRoutine(string name, List<string> content)
        {
            _routines.Add(new Routine(name, content));
        }

        public void AddRoutine(List<string> content)
        {
            _routines.Add(new Routine(content));
        }

        public void AddRoutine(Routine routine)
        {
            _routines.Add(routine);
        }

        public IEnumerable<string> GetTagsInCode()
        {
            var output = new HashSet<string>();
            foreach(var rotuine in _routines)
            {
                output.UnionWith(rotuine.GetTags());
            }
            //This should comapare the tags in tag section and remove them from the HashSet for the List only to get global tags 09/05/2020
            var compare = new HashSet<string>(_localTags.Acquire());//This gets the names of all the tags in local tags section
            foreach(var tag in compare)
            {
                output.RemoveWhere(x => output.Contains(x));
            }
            //This compares the list with the local tag list and erases local tags from the output list, this to avoid having them declared at 
            //global tags, this output only contains tag names, there should be an algorithm in the TagSection that takes this list
            //and looks for them in the original content and converts the string data to Tag Data.
            //output.UnionWith(_localTags.GetGlobalTags().Keys);//this adds the global tags found in the aliases to the list
            return output;
        }

        public Dictionary<string,string> GetTagsInSection()
        {
            return GetTagsInSection();
        }

        public List<string> GetRoutines()
        {
            //This routine get the list of routines in the current program and outputs them as a string for user to 
            //see the Program content
            var output = new List<string>();
            _routines.ForEach(x => output.Add(x.Name));
            return output;
        }

        public void DeleteRoutine(string name)
        {
            var index = _routines.FindIndex(x => x.Name == name);
            _routines.RemoveAt(index);
        }
    }
}
