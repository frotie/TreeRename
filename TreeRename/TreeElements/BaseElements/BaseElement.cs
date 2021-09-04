using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeRename.TreeElements.SpecialElements;

namespace TreeRename.TreeElements.BaseElements
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

            InitChild(child, NameResolver.GetName(child));
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
            if(string.IsNullOrEmpty(newName) || !NameResolver.Rename(this, newName)) 
                return false;

            Name = newName;
            return true;
        }

        public int AddChildren(List<IElement> children)
        {
            var names = NameResolver.GetNames(children.First(), children.Count);

            for(int i = 0; i < names.Count; ++i)
            {
                InitChild(children[i], names[i]);
                Children.Add(children[i]);
            }

            return names.Count;
        }

        public int RemoveChildren(List<IElement> children)
        {
            return 0;
        }

        private void InitChild(IElement child, string name)
        {
            if (child is ISpecialElement)
            {
                child.NameResolver = new NameResolver();
            }
            else
            {
                child.NameResolver = NameResolver;
            }
            child.Name = name;
            child.BaseElement = this;
        }
    }
}
