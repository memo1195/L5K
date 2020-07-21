using System.Collections.Generic;
using System.IO;

namespace L5K
{
    public class L5KReadStream
    {
        //This class reads a L5K file and gets its content into a List all of this content is accessible from an
        //indexer and defines the line in which each of the sections starts and ends
        private readonly string _controllerSecStart;
        private readonly string _dataTypeSecStart;
        private readonly string _moduleSecStart;
        private readonly string _addonSecStart;
        private readonly string _tagSecStart;
        private readonly string _programSecStart;
        private readonly string _taskSecStart;
        private readonly string _outroStart;
        private readonly List<string> _content;

        public L5KReadStream()
        {
            _content = new List<string>();
        }
        public int Length
        {
            get
            {
                return _content.Count;
            }
        }
        public int BegginingSecStart
        {
            get
            {
                return 0;
            }
        }
        public int BegginingSecEnd
        {
            get
            {
                return ControllerSecStart;
            }
        }
        public int ControllerSecStart
        {
            get
            {
                return _content.FindIndex(x => x.StartsWith(_controllerSecStart));
            }
        }
        public int ControllerSecEnd
        {
            get
            {
                return DataTypeSecStart;
            }
        }
        public int DataTypeSecStart
        {
            get
            {
                return _content.FindIndex(x => x.StartsWith(_dataTypeSecStart));
            }
        }
        public int DataTypeSecEnd
        {
            get
            {
                return ModuleTypeSecStart;
            }
        }
        public int ModuleTypeSecStart
        {
            get
            {
                return _content.FindIndex(x => x.StartsWith(_moduleSecStart));
            }
        }
        public int ModuleTypeSecEnd
        {
            get
            {
                return AddonSecStart;
            }
        }
        public int AddonSecStart
        {
            get
            {
                return _content.FindIndex(x => x.StartsWith(_addonSecStart));
            }
        }
        public int AddonSecEnd
        {
            get
            {
                return TagSecStart;
            }
        }
        public int TagSecStart
        {
            get
            {
                return _content.FindIndex(x => x.StartsWith(_tagSecStart));
            }
        }
        public int TagSecEnd
        {
            get
            {
                return ProgramSecStart;
            }
        }
        public int ProgramSecStart
        {
            get
            {
                return _content.FindIndex(x => x.StartsWith(_programSecStart));
            }
        }
        public int ProgramSecEnd
        {
            get
            {
                return TaskSecStart;
            }
        }
        public int TaskSecStart
        {
            get
            {
                return _content.FindIndex(x => x.StartsWith(_taskSecStart));
            }
        }
        public int TaskSecEnd
        {
            get
            {
                return OutroStart;
            }
        }
        public int OutroStart
        {
            get
            {
                return _content.FindIndex(x => x.StartsWith(_outroStart));
            }
        }
        public int OutroEnd
        {
            get
            {
                return Length;
            }
        }


        public L5KReadStream(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string read;
                while ((read = sr.ReadLine()) != null)
                    _content.Add(read);
            }

            _controllerSecStart = "CONTROLLER ";
            _dataTypeSecStart = " DATATYPE";
            _moduleSecStart = "	MODULE";
            _addonSecStart = " ADD_ON_INSTRUCTION_DEFINITION";
            _tagSecStart = " TAG";
            _programSecStart = " PROGRAM";
            _taskSecStart = " TASK";
            _outroStart = "CONFIG CST";
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
