using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public class NameResolver
    {
        private Dictionary<Type, ElementStat> _elements;
        public NameResolver()
        {
            _elements = new Dictionary<Type, ElementStat>();
        }
        public ElementStat GetElementStatistic(Type itemType, string baseName = null)
        {
            if (_elements.ContainsKey(itemType))
                return _elements[itemType];

            if (baseName == null)
                throw new ArgumentNullException();

            ElementStat stat = new ElementStat(baseName);
            _elements.Add(itemType, stat);

            return stat;
        }
    }
}
