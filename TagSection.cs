using System;
using System.Collections.Generic;
using System.Linq;

namespace L5K
{
    class TagSection : L5Ksection, IAcquire
    {
        //TagSection should be implemented once the program section has been implemented to find all the necessary
        //alarms
        private HashSet<Tag> _needed;
        private bool _localTagSection;

        public TagSection(L5KReadStream readings)
            : base(readings, readings.TagSecStart, readings.TagSecEnd)
        {
            _needed = new HashSet<Tag>();
            _initializer = readings.TagInit;
            _localTagSection = false;
        }

        public TagSection(List<string> content)
            : base(content)
        {
            //This constructor will be used with local tags only
            _needed = new HashSet<Tag>();
            //This gets all the lines in which a tag is delclared and adds it to a List of Tags
            var tagIndexes = _content.FindAllIndex(x => x.StartsWith("			") && !x.StartsWith("				"));
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
                    var lastIndex = _content.FindIndex(x => x.Contains("END_" + content[content.Count - 1].Trim()));
                    var length = tagIndexes[i] - lastIndex;
                    _needed.Add(new Tag(_content.GetRange(tagIndexes[index], length)));
                }
            }
        }
        public Dictionary<string, string> GetGlobalTags()//used only with localtags
        {
            var output = new Dictionary<string, string>();
            if (_localTagSection)
            {
                foreach (var tag in _needed)
                {
                    //Gets the tag relation OriginalTagName and TagName and lists it to a dictionary, this way
                    //we can send this to the global tag section and build the _needed section with the correct names
                    if (tag.IsAlias)
                    {
                        var tagrelation = tag.GetTagRelation();
                        output.Add(tagrelation[0], tagrelation[1]);
                    }
                }
                return output;
            }
            else
                throw new Exception("This method should only be used with local tags");
        }

        public void AddTags(List<Program> programs)
        {
            //This should have an algorithm to find all the tags in the programs, this to get the global tags
            //local tags should be gathered with the constructor of Program(content)
            //This method should read all the local tags in the program and get the ones that have an Alias
            //Also this should compare the tag list I have in Rung class that lists all the tags found in logic
            //This is done to discar missing any tags that are used as global 09/05/2020
            if (!_localTagSection)
                throw new Exception("This method should only be used with global tags");
            foreach (var program in programs)
            {
                var tagsFromCode = program.GetTagsInCode();
                
                foreach (var tag in tagsFromCode)
                {
                    var tagIndex = _content.IndexOf(tag);
                    var length = _content.IndexOf(";", tagIndex) + 1 - tagIndex;
                    _needed.Add(new Tag(_content.GetRange(tagIndex, length)));
                }
                var tagsFromTagSection = program.GetTagsInSection();

                foreach(var tag in tagsFromTagSection)
                {
                    var tagIndex = _content.IndexOf(tag.Key);
                    var length = _content.IndexOf(";", tagIndex) + 1 - tagIndex;
                    _needed.Add(new Tag(tag.Value,_content.GetRange(tagIndex, length)));
                }
            }
        }

        public override List<string> Acquire()
        {
            //Not sure if this is ok, maybe it will also be necessary to get the list of DTs
            return _needed.Select(x => x.Name).ToList();
        }
    }
}
