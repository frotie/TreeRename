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
            NameResolver = new NameResolver();
            Name = BaseName;
        }

        public bool AddChild(IElement child)
        {
            if (child == null) return false;

            try
            {
                ElementStat stat = NameResolver.GetElementStatistic(child.GetType(), child.BaseName);
                string name = stat.GetNextDefaultName();

                InitChild(child, name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveChild(IElement child)
        {
            if(!Children.Contains(child)) return false;

            try 
            {
                ElementStat stat = NameResolver.GetElementStatistic(child.GetType());
                stat.RemoveItem(child.Name);

                Children.Remove(child);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Rename(string newName)
        {
            if(string.IsNullOrEmpty(newName)) 
                return false;

            try
            {
                ElementStat stat = NameResolver.GetElementStatistic(GetType());
                Name = stat.ChangeItemName(Name, newName);

                return true;
            }
            catch(ArgumentException)
            {
                // ex...
            }
            return false;
        }

        public int AddChildren(params IElement[] children)
        {
            try
            {
                IElement first = children.First();
                ElementStat stat = NameResolver.GetElementStatistic(first.GetType(), first.BaseName);

                string[] names = stat.GetDefaultNamesList((uint)children.Length);

                for (int i = 0; i < names.Length; ++i)
                {
                    InitChild(children[i], names[i]);
                }
                return names.Length;
            }
            catch
            {
                return 0;
            }
        }

        public void RemoveChildren(List<IElement> children)
        {
            if (children == null)
                throw new ArgumentNullException();

            foreach (var child in children)
            {
                try
                {
                    ElementStat stat = NameResolver.GetElementStatistic(child.GetType());
                    stat.RemoveItem(child.Name);

                    Children.Remove(child);
                }
                catch
                {
                    continue;
                }
            }
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
            Children.Add(child);
        }
    }
}
