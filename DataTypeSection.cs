using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    class DataTypeSection : L5Ksection, IAcquire
    {
        private Dictionary<string,int[]> _datatypes;
        private HashSet<string> _needed;
        public DataTypeSection(L5KReadStream readings)
            :base(readings,readings.DataTypeSecStart,readings.DataTypeSecEnd)
        {
            var datatypeIndexes = _content.FindAllIndex(x => x.StartsWith(_initializer));
            _datatypes = new Dictionary<string, int[]>();
            for(var i=0;i<datatypeIndexes.Count;i++)
            {
                var index = datatypeIndexes[i];
                if(i+1!=datatypeIndexes.Count)
                    _datatypes.Add(_GetNameBetweenElements(_content[index], _initializer.Length + 1, ' ')
                        ,new int[] {index,datatypeIndexes[i+1] });
                else
                    _datatypes.Add(_GetNameBetweenElements(_content[index], _initializer.Length + 1, ' ')
                        , new int[] { index, _content.Count-1 });
            }
            _needed = new HashSet<string>();

        }
        
        public override List<string> Acquire()
        {
            /*var output = new List<string>();
            foreach(var datatype in _datatypes)
            {
            }*/
            return _datatypes.Keys.ToList();
        }

        //here the method should be provided with all the tags in the program, this method should be called once all
        //the programs are already defined and no changes will be made.
        public void AddDataTypes(List<Tag> tags) 
        { 
            foreach(var tag in tags)
            {
                if (!tag.IsAlias)
                    _needed.Add(tag.AliasOrDT);
            }
        }
        //this methods is intended to use for printing the final output of the datatypes listed
        public List<string> GetFinalContent()
        {
            var output = new List<string>();
            foreach(var datatype in _needed)
            {
                output.AddRange(this[datatype]);
            }
            return output;
        }

        //in case I want to consult the content of a DT, most likely this will not be used, probably making this private
        public List<string> this[string key]
        {
            get
            {
                var startValue = _datatypes[key][0];
                var endValue = _datatypes[key][1];
                var range = endValue - startValue;

                return _content.GetRange(startValue,range);
            }
        }
    }
}
