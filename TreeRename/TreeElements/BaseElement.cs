using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    abstract class BaseElement : IElement
    {
        public string Name { get; set; }
        public int ElementNumber { get; set; }
        public List<IElement> Children { get; private set; }
        IElement IElement.BaseElement { get; set; }
        public virtual string BaseName { get; } = "Base Element";
        public BaseElement()
        {
            Children = new List<IElement>();
            Name = BaseName;
        }

        public bool AddChild(IElement child)
        {
            if (child == null) return false;

            child.BaseElement = this;

            child.ElementNumber = GetCurrentNumber(child);
            child.Name = child.BaseName + " " + child.ElementNumber.ToString();

            Children.Add(child);

            return true;
        }

        public bool RemoveChild(IElement child)
        {
            if(!Children.Contains(child)) return false;

            Children.Remove(child);
            return true;
        }

        public bool Rename(string newName)
        {
            if(string.IsNullOrEmpty(newName)) return false;

            Name = newName;
            ElementNumber = 0;

            return true;
        }

        // Iterator pattern?
        private int GetCurrentNumber(IElement element)
        {
            var elements = GetSameElements(element);
            for (int i = 0; i < elements.Count; ++i)
            {
                if(!elements.Any(e => e.ElementNumber == i + 1))
                {
                    return i + 1;
                }
            }

            return elements.Count + 1;
        }

        private List<IElement> GetSameElements(IElement element, IElement root = null)
        {
            List<IElement> currentElements = new List<IElement>();
            if (root == null) root = GetTreeBase(element);
            foreach (var child in root.Children)
            {
                if (Type.Equals(child.GetType(), element.GetType()))
                {
                    currentElements.Add(child);
                }
                currentElements.AddRange(GetSameElements(element, child));
            }
            return currentElements;
        }

        private IElement GetTreeBase(IElement current)
        {
            if (current.BaseElement != null) 
                return GetTreeBase(current.BaseElement);
            else 
                return current;
        }
    }
}
