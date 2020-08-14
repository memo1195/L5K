using System;
using System.Collections.Generic;

namespace L5K
{
    public class ModuleSection : L5Ksection, IAcquire
    {
        private readonly string _childrenName;
        private List<Module> _modules;
        private List<Module> _needed;

        public ModuleSection(L5KReadStream readings)
            : base(readings, readings.ModuleTypeSecStart, readings.ModuleTypeSecEnd)
        {
            _childrenName = "$NoName";
            
            _modules = new List<Module>();//This separates the content in Modules
            var moduleIndexes = _content.FindAllIndex(x => x.StartsWith(" " + _initializer) && !x.Contains(_childrenName));
            //This section reads each Index that has been found and builds a Module object for each 
            for (var i = 0; i < moduleIndexes.Count; i++)
            {
                var index = moduleIndexes[i];
                if (i + 1 != moduleIndexes.Count)
                {
                    var count = moduleIndexes[i + 1] - index;
                    _modules.Add(new Module(_content.GetRange(index, count)));
                }
                else
                {
                    var count = _content.Count - index;
                    _modules.Add(new Module(_content.GetRange(index, count)));
                }
            }
            _needed = new List<Module>();
        }


        public void AddModule(string name, string originalName)
        {
            var index = _modules.FindIndex(x => x.Name == originalName);
            var copyList = new List<string>();
            for (var i = 0; i < _modules[index].Length; i++)
            {
                copyList.Add(_modules[index][i]);
            }
            
            _needed.Add(new Module(name,copyList));
        }

        //This Method will read aquire the module names and return them as a list, this to show them to the user
        //for it to choose which ones it will use
        public override List<string> Acquire()
        {
            var output = new List<string>();
            foreach (var module in _modules)
                output.Add(module.Name);

            return output;
        }
    }
}
