using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements.SpecialElements
{
    class FloorElement : BaseElements.BaseElement, ISpecialElement
    {
        public override string BaseName => "Этаж";
        public FloorElement() : base()
        {
        }
    }
}
