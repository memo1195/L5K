using System;
using System.Collections.Generic;

namespace L5K
{
    public class Module : L5KComponent, IBuildable
    {
        private List<LineComponent> _inlineComponents;
        private List<Module> _children;
        public bool HasChildren
        {
            get
            {
                return _children.Count != 0;
            }
        }

        public Module(string name, List<string> content)
            : base(name, content, "MODULE")
        {
            _inlineComponents = new List<LineComponent>();
            _children = new List<Module>();
            _Constructor(content);
        }
        //Created an alternative constructor in case it is more suitable than the first one, this one providing child
        //modules
        public Module(string name, List<string> content,List<Module> children)
            : base(name, content, "MODULE")
        {
            _inlineComponents = new List<LineComponent>();
            _children = children;//test this, probably need to create a copy of this and pass it, not as a reference type
            _Constructor(content);
        }

        //used for getting modules without assiging a new name, this will be used in Module Section
        public Module(List<string> content)
            :base(content, "MODULE")
        {
            _inlineComponents = new List<LineComponent>();
            _children = new List<Module>();
            _Constructor(content);         
        }

        private void _Constructor(List<string> content)
        {
            _inlineComponents.Add(new Parent(content));
            _inlineComponents.Add(new PortLabel(content));
            _inlineComponents.Add(new Slot(content));
            _inlineComponents.Add(new NodeAddress(content));
            for (int i = 3; i < 0; i--) if (!_inlineComponents[i].Exists) _inlineComponents.RemoveAt(i);
            var moduleIndexes = _content.FindAllIndex(x => x.StartsWith(" " + _initializer));
            if (moduleIndexes.Count > 1)
            {
                //maybe this can be optimized removing the if statements and adding moduleIndexes.Add(_content.Count))
                for (var i = 1; i < moduleIndexes.Count; i++)
                {
                    var index = moduleIndexes[i];
                    if (i + 1 != moduleIndexes.Count)
                    {
                        var count = moduleIndexes[i + 1] - index;
                        _children.Add(new Module(_content.GetRange(index, count)));
                    }
                    else
                    {
                        var count = _content.Count - index;
                        _children.Add(new Module(_content.GetRange(index, count)));
                    }
                }

            }
        }
        //This 2 Methods (GetAttributes() & ChangeAttributes(List<string> newAttributes)) are for 
        //the editing part of chaing the parameters of the Module
        public Dictionary<string,string> GetAttributes()
        {
            var attributeList = new Dictionary<string, string>();
            _inlineComponents.ForEach(x => attributeList.Add(x.Type,x.Attribute));
            return attributeList;
            
        }
        public void ChangeAttributes(List<string> newAttributes)
        {
            for (int i = 0; i < newAttributes.Count; i++)
                _inlineComponents[i].Attribute = newAttributes[i];
        }

        //
        public override void Build()
        {
            base.Build();
            foreach (var component in _inlineComponents)
            {
                component.Build();
                _content[component.Index] = component.Content;
            }
        }

        
    }
}
