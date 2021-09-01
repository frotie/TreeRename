using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public class NameResolver
    {
        private Dictionary<string, List<string>> _namesCollection;
        public NameResolver()
        {
            _namesCollection = new Dictionary<string, List<string>>();
        }

        public void RemoveElement(IElement element)
        {
            if(_namesCollection.ContainsKey(element.BaseName))
            {
                _namesCollection[element.BaseName].Remove(element.Name);
            }
        }

        public string AddElement(IElement element)
        {
            string name = NormalizeName(element, element.Name);
            if (_namesCollection.ContainsKey(element.BaseName))
            {
                _namesCollection[element.BaseName].Add(name);
            }
            else
            {
                _namesCollection.Add(element.BaseName, new List<string> { name });
            }

            return name;
        }

        public string Rename(IElement element, string name)
        {
            string result = NormalizeName(element, name);

            int index = _namesCollection[element.BaseName].IndexOf(element.Name);
            _namesCollection[element.BaseName][index] = result;

            return result;
        }

        private string NormalizeName(IElement element, string name)
        {
            string result = name + " 1";

            if (!_namesCollection.ContainsKey(element.BaseName))
                return result;

            int recordsCount = _namesCollection[element.BaseName].Count;
            bool isOverflow = true;

            for (int i = 0; i < recordsCount; ++i)
            {
                result = name + " " + (i + 1).ToString();
                if (_namesCollection[element.BaseName].Any(elName => elName == result))
                    continue;

                isOverflow = false;
                break;
            }

            if (isOverflow)
                result = name + " " + (recordsCount + 1).ToString();

            return result;
        }
    }
}
