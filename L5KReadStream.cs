using System.Collections.Generic;
using System.IO;

namespace L5K
{
    public class L5KReadStream
    {
        //This class reads a L5K file and gets its content into a List all of this content is accessible from an
        //indexer and defines the line in which each of the sections starts and ends
        public string ControllerInit { get; }
        public string DataTypeInit { get; }
        public string ModuleInit { get; }
        public string AddonInit { get; }
        public string TagInit { get; }
        public string ProgramInit { get; }
        public string TaskInit { get; }
        public string OutroInit { get; }

        private List<string> _content;

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
                return _content.FindIndex(x => x.StartsWith(ControllerInit));
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
                return _content.FindIndex(x => x.StartsWith(DataTypeInit));
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
                return _content.FindIndex(x => x.StartsWith(ModuleInit));
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
                return _content.FindIndex(x => x.StartsWith(AddonInit));
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
                return _content.FindIndex(x => x.StartsWith(TagInit));
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
                return _content.FindIndex(x => x.StartsWith(ProgramInit));
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
                return _content.FindIndex(x => x.StartsWith(TaskInit));
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
                return _content.FindIndex(x => x.StartsWith(OutroInit));
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

            ControllerInit = "CONTROLLER ";
            DataTypeInit = " DATATYPE";
            ModuleInit = "	MODULE";
            AddonInit = " ADD_ON_INSTRUCTION_DEFINITION";
            TagInit = " TAG";
            ProgramInit = " PROGRAM";
            TaskInit = " TASK";
            OutroInit = "CONFIG CST";
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
