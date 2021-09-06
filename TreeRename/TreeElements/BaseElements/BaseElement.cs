﻿using System;
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

            try
            {
                string name = NameResolver.GetStandartName(child.GetType(), child.BaseName);
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
                NameResolver.RemoveElement(child.GetType(), child.Name);
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
                string name = NameResolver.ChangeNameInStatistic(GetType(), newName, Name);
                Name = name;

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
                string[] names = NameResolver.GetStandartNames(first.GetType(), first.BaseName, children.Length);

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
                    NameResolver.RemoveElement(child.GetType(), child.Name);
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
