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

            int elNumber = GetNumberForItem(elStat);
            string name = BuildStandartName(elStat, elNumber);

            return name;
        }

        public string[] GetStandartNames(Type itemType, string baseName, int itemCount)
        {
            if (itemType == null || itemCount == 0) 
                throw new ArgumentNullException();

            string[] result = new string[itemCount];
            ElementStat stat = GetStatOrCreate(itemType, baseName);

            int freeCount = stat.FreeNumbers.Count;
             
            for (int i = 0; i < itemCount; ++i)
            {
                int number;
                if (i < freeCount)
                    number = stat.FreeNumbers[i];
                else
                    number = stat.ElementsCount + i + 1;

                result[i] = BuildStandartName(stat, number);
            }

            stat.ElementsCount += itemCount;
            stat.FreeNumbers.RemoveRange(0, Math.Min(itemCount, freeCount));

            return result;
        }

        public string ChangeNameInStatistic(Type itemType, string name, string oldName)
        {
            if (itemType == null || !_elements.ContainsKey(itemType))
                throw new ArgumentNullException();

            var elStat = _elements[itemType];

            if (elStat.CustomNames.Contains(name))
                throw new ArgumentException();

            string result;
            RemoveElement(itemType, oldName);
            if (name == elStat.BaseName)
            {
                result = GetStandartName(itemType, elStat.BaseName);
            }
            else
            {
                elStat.CustomNames.Add(name);
                result = name;
            }

            return result;
        }

        public void RemoveElement(Type itemType, string name)
        {
            if (itemType == null || !_elements.ContainsKey(itemType))
                throw new ArgumentException();

            ElementStat elStat = _elements[itemType];

            if (elStat.CustomNames.Contains(name))
            {
                elStat.CustomNames.Remove(name);
            }
            else
            {
                int number = GetNumberFromName(name);
                elStat.AddFreeNumber(number);
                elStat.ElementsCount--;
            }
        }

        private string BuildStandartName(ElementStat element, int number) => 
            element.BaseName + " " + number.ToString();

        private int GetNumberForItem(ElementStat elementStat)
        {
            int number;
            elementStat.ElementsCount++;
            if (elementStat.FreeNumbers.Count > 0)
            {
                number = elementStat.FreeNumbers[0];
                elementStat.FreeNumbers.RemoveAt(0);
            }
            else
                number = elementStat.ElementsCount;

            return number;
        }
        private int GetNumberFromName(string name)
        {
            if (!name.Contains(' ')) 
                throw new ArgumentException("Element name gave in incorrect format");
            return int.Parse(name.Split(' ').Last());
        }

        private ElementStat GetStatOrCreate(Type type, string baseName)
        {
            ElementStat result = new ElementStat();
            if (!_elements.ContainsKey(type))
            {
                result.BaseName = baseName;
                _elements.Add(type, result);
            }
            else
                result = _elements[type];
            return result;
        }
    }
}
