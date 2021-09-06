using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeRename.TreeElements.BaseElements;

namespace TreeRename.TreeElements.SpecialElements
{
    abstract class SpecialBaseElement : BaseElement
    {
        protected override void InitChild(IElement child, string name)
        {
            child.NameResolver = new NameResolver();
            child.Name = name;
            child.BaseElement = this;
            Children.Add(child);
        }
    }
}
