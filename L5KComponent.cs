using System;
using System.Collections.Generic;

namespace L5K
{
    public abstract class L5KComponent : IBuildable, ICloneable
    {
        public string Name { get; set; }
        public int Length
        {
            get
            {
                return _content.Count;
            }
        }
        protected List<string> _content;
        protected string _originalName;
        protected string _initializer;

        public L5KComponent(List<string> content,string initializer)
        {
            _initializer = initializer;
            Name = _GetOriginalName(initializer);
            _originalName = Name;
            _content=content;
        }
        public L5KComponent(string name, List<string> content, string initializer)
        {
            _initializer = initializer;
            Name = name;
            _content = content;
            _originalName = _GetOriginalName(initializer);
        }
        private string _GetNameBetweenElements(string line, int initIndex, char element)
        {
            line = line.Substring(initIndex);
            var originalNameLength = line.IndexOf(element);
            return line.Substring(0, originalNameLength);
        }

        protected virtual string _GetOriginalName(string initializer)
        {
            var initIndex = _content[0].IndexOf(initializer) + initializer.Length + 1;
            return _GetNameBetweenElements(_content[0], initIndex, ' ');
        }

        public virtual void Build()
        {
            _content[0] = _content[0].Replace(_originalName, Name);
        }

        public object Clone()
        {
            return new List<string>(_content.Clone());
        }


        public string this[int key]
        {
            //This Indexer is intended to be used once the Build() Method has run, this to avoid having original
            //values in the output, this indexer will be used to print all the content to the L5K. Each Build()
            //method of each class should be personalized to adapt to this indexer.
            get
            {
                return _content[key];
            }

        }
    }
}
