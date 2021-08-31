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

        // TODO: Check for an existing name?
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
            if(string.IsNullOrEmpty(newName)) 
                return false;

            var list = GetSameElementsFromTree(this);
            if (list.Any(e => e.Name == newName))
                return false;

            Name = newName;
            ElementNumber = 0;

            return true;
        }

        // Iterator pattern?
        private int GetCurrentNumber(IElement element)
        {
            var elements = GetSameElementsFromTree(element);
            for (int i = 0; i < elements.Count; ++i)
            {
                if(!elements.Any(e => e.ElementNumber == i + 1))
                {
                    return i + 1;
                }
            }

            return elements.Count + 1;
        }

        private List<IElement> GetSameElementsFromTree(IElement element, IElement root = null)
        {
            if (root == null) root = GetTreeRoot(element);
            List<IElement> currentElements = new List<IElement>();

            foreach (var child in root.Children)
            {
                if (Type.Equals(child.GetType(), element.GetType()))
                {
                    currentElements.Add(child);
                }
                currentElements.AddRange(GetSameElementsFromTree(element, child));
            }
            return currentElements;
        }

        private IElement GetTreeRoot(IElement current)
        {
            if (current.BaseElement != null) 
                return GetTreeRoot(current.BaseElement);
            else 
                return current;
        }
    }
}
