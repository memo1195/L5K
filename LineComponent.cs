using System.Collections.Generic;

namespace L5K
{

    public abstract class LineComponent : IBuildable
    {
        protected string _Init;
        protected string _linecontent;
        private int _initIndex;
        private string _originalAttribute;
        public bool Exists 
        { 
            get 
            {
                return Attribute != null;
            } 
        }

        public string Content
        {
            get
            {
                return _linecontent;
            }
        }

        public string Type { get; protected set; }

        public string Attribute { get; set; } 

        public int Index { get; private set; }

        public LineComponent(List<string> content,string init)
        {
            _Init = init;
            _GetOriginalName(content);
            _DefineType();
        }

        private string _GetNameBetweenElements(string line, char element)
        {
            var originalNameLength = line.IndexOf(element);
            return line.Substring(0, originalNameLength);
        }

        private void _DefineType()
        {
            Type = _Init.Substring(0, _Init.LastIndexOf(' '));
        }
        private void _GetOriginalName(List<string> content)
        {
            Index = content.FindIndex(x => x.Contains(_Init));
            if (Index != -1) 
            {
                _linecontent = content[Index];
                _initIndex = _linecontent.IndexOf(_Init) + _Init.Length + 1;
                _originalAttribute = _GetNameBetweenElements(_linecontent.Substring(_initIndex), '"');
            }
            
        }

        public void Build()
        {
            _linecontent = _linecontent.Replace(_originalAttribute, Attribute);
        }
    }
}
