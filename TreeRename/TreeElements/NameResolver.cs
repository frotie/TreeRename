using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public class NameResolver
    {
        private Dictionary<string, List<IElement>> _typesCollection;
        public NameResolver()
        {
            _typesCollection = new Dictionary<string, List<IElement>>();
        }

        public void RemoveElement(IElement element)
        {
            if(_typesCollection.ContainsKey(element.BaseName))
            {
                _typesCollection[element.BaseName].Remove(element);
            }
        }

        public string AddElement(IElement element)
        {
            if(!_typesCollection.ContainsKey(element.BaseName))
            {
                _typesCollection.Add(element.BaseName, new List<IElement>() { element });
            }
            else
            {
                _typesCollection[element.BaseName].Add(element);
            }

            return Rename(element);
        }

        public string Rename(IElement element, string name = null)
        {
            if (name == null) name = element.BaseName;
            for (int i = 0; i < _typesCollection[element.BaseName].Count; ++i)
            {
                if (_typesCollection[element.BaseName][i].Name == name + " " + (i + 1).ToString())
                {
                    continue;
                }
                return name + " " + (i + 1).ToString();
            }
            return name + " " + _typesCollection[element.BaseName].Count.ToString();
        }
    }
}
