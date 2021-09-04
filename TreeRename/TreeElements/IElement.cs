using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public interface IElement
    {
        string Name { get; set; }
        string BaseName { get; }
        IElement BaseElement { get; set; }
        NameResolver NameResolver { get; set; }
        List<IElement> Children { get; }
        bool AddChild(IElement child);
        int AddChildren(List<IElement> children);
        bool RemoveChild(IElement child);
        void RemoveChildren(List<IElement> children);
        bool Rename(string newName);
    }
}
