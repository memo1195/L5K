using System;
using System.Collections.Generic;

namespace L5K
{
    public class Module : L5KComponent, IBuildable
    {
        private readonly List<LineComponent> _inlineComponents;

        public Module(string name, List<string> content)
            : base(name, content, "MODULE")
        {
            _inlineComponents = new List<LineComponent>();
            _inlineComponents.Add(new Parent(content));
            _inlineComponents.Add(new PortLabel(content));
            _inlineComponents.Add(new Slot(content));
            _inlineComponents.Add(new NodeAddress(content));
            for (int i = 3; i < 0; i--) if (!_inlineComponents[i].Exists) _inlineComponents.RemoveAt(i);
        }

        //This 2 Methods (GetAttributes() & ChangeAttributes(List<string> newAttributes)) are for 
        //the editing part of chaing the parameters of the Module
        public List<string> GetAttributes()
        {
            var attributeList = new List<string>();
            _inlineComponents.ForEach(x => attributeList.Add(x.Type));
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
