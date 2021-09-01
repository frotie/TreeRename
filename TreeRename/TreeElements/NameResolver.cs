using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public class NameResolver
    {
        // key - BaseName of Element
        private Dictionary<string, List<ElementToDelete>> _elements;
        public NameResolver()
        {
            _elements = new Dictionary<string, List<ElementToDelete>>();
        }

        public string GetName(IElement element)
            => GetNameWithNumber(element);

        public string Rename(IElement element, string name)
        {
            RemoveElement(element);
            return GetNameWithNumber(element, name);
        }

        public void RemoveElement(IElement element)
        {
            if (_elements.Any(e => e.Key == element.BaseName))
            {
                var namesToDelete = _elements.First(e => e.Key == element.BaseName);
                var toDelete = namesToDelete.Value.First(e => e.Name == GetSourseName(element.Name));

                toDelete.FreeNumbers.Add(GetNumberFromName(element.Name));
                toDelete.ElementsCount--;

                if(toDelete.ElementsCount == 0)
                {
                    namesToDelete.Value.Remove(toDelete);
                }
            }
        }

        private string GetNameWithNumber(IElement element, string newName = null)
        {
            if (!_elements.Any(e => e.Key == element.BaseName))
                _elements.Add(element.BaseName, new List<ElementToDelete>());

            string name = GetSourseName(newName ?? element.Name);
            int number = GetNameNumber(element.BaseName, name);

            return name + " " + number.ToString();
        }

        private int GetNameNumber(string baseName, string normalizedName)
        {
            int number = 0;
            List<ElementToDelete> baseElements = _elements.First(e => e.Key == baseName).Value;

            if (baseElements.Any(t => t.Name == normalizedName))
            {
                var toDelete = baseElements.First(t => t.Name == normalizedName);
                toDelete.ElementsCount++;
                if (toDelete.FreeNumbers.Count > 0)
                {
                    number = toDelete.FreeNumbers.First();
                    toDelete.FreeNumbers.Remove(number);
                }
                else
                {
                    number = toDelete.ElementsCount;
                }
            }
            else
            {
                var el = new ElementToDelete(normalizedName);
                baseElements.Add(el);
                el.ElementsCount++;

                number = el.ElementsCount;
            }

            return number;
        }
        private int GetNumberFromName(string name)
        {
            if (!name.Contains(' ')) throw new ArgumentException("Element name gave in incorrect format");
            return int.Parse(name.Split(' ').Last());
        }
        private string GetSourseName(string name)
        {
            if (!name.Contains(' ')) 
                return name;

            var arr = name.Split(' ');
            return string.Join("", name.Split(' ').TakeWhile(o => o != arr.Last()));
        }
    }
    internal class ElementToDelete
    {
        public string Name { get; set; }
        public int ElementsCount { get; set; }
        public List<int> FreeNumbers { get; set; }
        public ElementToDelete(string name = null)
        {
            Name = name;
            FreeNumbers = new List<int>();
        }
    }
}
