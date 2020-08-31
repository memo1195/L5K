using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L5K
{
    public static class CollectionsExtensions
    {
        public static List<int> FindAllIndex<T>(this List<T> container, Predicate<T> match)
        {
            var items = container.FindAll(match);
            var indexes = new List<int>();
            foreach (var item in items)
            {
                indexes.Add(container.IndexOf(item));
            }

            return indexes;
        }
        /*public static List<int[]> FindAllIndex<T>(this List<T> container, Predicate<T> match1, Predicate<T> match2)
        {
            var items1 = container.FindAllIndex(match1);
            var items2 = container.FindAllIndex(match2);
            var indexes = new List<int>();
            for

            return indexes;
        }*/

        //Has not been implemented yet
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

    }
}
