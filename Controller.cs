using System.Collections.Generic;

namespace L5K
{
    public class Controller : L5KComponent, IBuildable
    {
        private readonly string _safetyTagInit;
        private readonly List<string> _safetyTagMapping;

        public Controller(string name, List<string> content)
            : base(name, content, "CONTROLLER")
        {
            _safetyTagInit = "                                  SafetyTagMap :=";
            _safetyTagMapping = new List<string>();
        }


        public void AddSafetyTagMap(string safetyTag, string standardTag)
        {
            string line = standardTag + '=' + safetyTag + ',';
            _safetyTagMapping.Add(line);
        }

        public override void Build()
        {
            base.Build();
            int safetytagIndex = _content.FindIndex(x => x.StartsWith(_safetyTagInit));
            _content[safetytagIndex] = _safetyTagInit + " \" ";
            foreach (var map in _safetyTagMapping)
            {
                _content[safetytagIndex] += map;
            }
            _content[safetytagIndex] += "\",";
        }



    }
}
