using System.Collections.Generic;

namespace L5K
{
    public abstract class L5KComponent : IBuildable
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
            int originalNameLength = line.IndexOf(element);
            return line.Substring(0, originalNameLength);
        }

        protected virtual string _GetOriginalName(string initializer)
        {
            int initIndex = _content[0].IndexOf(initializer) + initializer.Length + 1;
            return _GetNameBetweenElements(_content[0], initIndex, ' ');
        }

        public virtual void Build()
        {
            _content[0] = _content[0].Replace(_originalName, Name);
        }

        public string this[int key]
        {
            get
            {
                return _content[key];
            }

        }
    }
}
