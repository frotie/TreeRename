using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public class NameResolver
    {
        private Dictionary<Type, ElementCounter> _elements;
        public NameResolver()
        {
            _elements = new Dictionary<Type, ElementCounter>();
        }

        public ElementCounter GetElementCounter(Type itemType, string baseName = null)
        {
            if (itemType == null)
                throw new ArgumentNullException();

            if (_elements.ContainsKey(itemType))
                return _elements[itemType];

            if (string.IsNullOrEmpty(baseName))
                throw new ArgumentNullException();

            ElementCounter stat = new ElementCounter(baseName);
            _elements.Add(itemType, stat);

            return stat;
        }
    }
}
