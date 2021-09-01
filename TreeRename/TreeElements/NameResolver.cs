using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public class NameResolver
    {
        private Dictionary<string, List<string>> _typesCollection;
        public NameResolver()
        {
            _typesCollection = new Dictionary<string, List<string>>();
        }

        public void RemoveElement(IElement element)
        {
            if(_typesCollection.ContainsKey(element.BaseName))
            {
                _typesCollection[element.BaseName].Remove(element.Name);
            }
        }
        
        public string AddElement(IElement element)
        {
            if(!_typesCollection.ContainsKey(element.BaseName))
            {
                _typesCollection.Add(element.BaseName, new List<string>() { element.Name });
            }
            else
            {
                _typesCollection[element.BaseName].Add(element.Name);
            }

            element.Name = Rename(element);
            return element.Name;
        }

        public string Rename(IElement element, string name = null)
        {
            if (name == null) name = element.BaseName;
            for (int i = 0; i < _typesCollection[element.BaseName].Count; ++i)
            {
                if (_typesCollection[element.BaseName].Any(elName => elName == name + " " + (i + 1).ToString()))
                {
                    continue;
                }
                return name + " " + (i + 1).ToString();
            }
            return name + " " + _typesCollection[element.BaseName].Count.ToString();
        }
    }
}
