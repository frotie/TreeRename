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

        public string GetStandartName(Type itemType, string baseName)
        {
            if (itemType == null || baseName == null) 
                throw new ArgumentNullException();

            ElementStat elStat = GetStatOrCreate(itemType, baseName);

            return elStat.GetNextDefaultName();
        }

        public string[] GetStandartNames(Type itemType, string baseName, int itemCount)
        {
            if (itemType == null || itemCount == 0 || itemCount < 0) 
                throw new ArgumentNullException();

            ElementStat stat = GetStatOrCreate(itemType, baseName);
            string[] result = stat.GetDefaultNamesList((uint)itemCount);

            return result;
        }

        public string ChangeNameInStatistic(Type itemType, string oldName, string name)
        {
            if (itemType == null || !_elements.ContainsKey(itemType))
                throw new ArgumentNullException();

            ElementStat elStat = _elements[itemType];
            string result = elStat.ChangeItemName(oldName, name);
            
            return result;
        }

        public void RemoveElement(Type itemType, string name)
        {
            if (itemType == null || !_elements.ContainsKey(itemType))
                throw new ArgumentException();

            ElementStat elStat = _elements[itemType];
            elStat.RemoveItem(name);
        }

        private ElementStat GetStatOrCreate(Type type, string baseName)
        {
            ElementStat result = new ElementStat(baseName);
            if (!_elements.ContainsKey(type))
            {
                _elements.Add(type, result);
            }
            else
                result = _elements[type];
            return result;
        }
    }
}
