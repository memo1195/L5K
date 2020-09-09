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
        public string ProjectName { get; set; }//maybe trigger an event from here and and subscribe all the sections to it

        private List<string> _content;

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
                return ControllerSecStart-1;
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
                return DataTypeSecStart-1;
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
                return ModuleTypeSecStart-1;
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
                return AddonSecStart-1;
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
                return TagSecStart-1;
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
                return ProgramSecStart-1;
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
                return TaskSecStart-1;
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
                return OutroStart-1;
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
            _content = new List<string>();
            using (StreamReader sr = File.OpenText(path))
            {
                string read;
                while ((read = sr.ReadLine()) != null)
                    _content.Add(read);
            }

            ControllerInit = "CONTROLLER ";
            DataTypeInit = "	DATATYPE";
            ModuleInit = "	MODULE";
            AddonInit = "	ADD_ON_INSTRUCTION_DEFINITION";
            TagInit = "	TAG";
            ProgramInit = "	PROGRAM";
            TaskInit = "	TASK";
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
