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
            if (child == null) 
                return false;

            try
            {
                ElementCounter stat = NameResolver.GetElementCounter(child.GetType(), child.BaseName);
                string name = stat.GetNextDefaultName();
                stat.UseNextDefaultName();

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
            if(child == null || !Children.Contains(child)) 
                return false;

            try 
            {
                ElementCounter stat = NameResolver.GetElementCounter(child.GetType());
                bool removeStatus = stat.RemoveItem(child.Name);

                // Some error: bad name
                if (!removeStatus)
                    return false;

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
                ElementCounter stat = NameResolver.GetElementCounter(GetType());
                bool renameStatus = stat.ChangeItemName(Name, newName);

                if (!renameStatus)
                    return false;

                Name = newName;
                return true;
            }
            catch(ArgumentException)
            {
                // ex...
                return false;
            }
        }

        public int AddChildren(params IElement[] children)
        {
            if (children == null || children.Length == 0)
                return 0;

            try
            {
                IElement first = children.First();
                ElementCounter stat = NameResolver.GetElementCounter(first.GetType(), first.BaseName);

                string[] names = stat.TakeDefaultNamesList((uint)children.Length);

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
            if (children == null || children.Count == 0)
                throw new ArgumentNullException();

            foreach (var child in children)
            {
                try
                {
                    ElementCounter stat = NameResolver.GetElementCounter(child.GetType());
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
                child.NameResolver = new NameResolver();
            else
                child.NameResolver = NameResolver;

            child.Name = name;
            child.BaseElement = this;
            Children.Add(child);
        }
    }
}
