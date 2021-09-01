using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    internal abstract class BaseElement : IElement
    {
        public string Name { get; set; }
        public abstract string BaseName { get; }
        public List<IElement> Children { get; private set; }
        IElement IElement.BaseElement { get; set; }
        public NameResolver NameResolver { get; set; }

        public BaseElement()
        {
            Children = new List<IElement>();
            Name = BaseName;
            NameResolver = new NameResolver();
        }

        public bool AddChild(IElement child)
        {
            if (child == null) return false;

            child.NameResolver = NameResolver;
            child.BaseElement = this;
            child.Name = NameResolver.AddElement(child);
            Children.Add(child);

            return true;
        }

        public bool RemoveChild(IElement child)
        {
            if(!Children.Contains(child)) return false;

            NameResolver.RemoveElement(child);
            Children.Remove(child);
            return true;
        }

        public bool Rename(string newName)
        {
            if(string.IsNullOrEmpty(newName)) 
                return false;

            Name = NameResolver.Rename(this, newName);

            return true;
        }
    }
}
