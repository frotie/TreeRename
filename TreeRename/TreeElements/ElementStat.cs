using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    class ElementStat
    {
        public string BaseName { get; set; }
        public int ElementsCount { get; set; }
        public List<int> FreeNumbers { get; set; }
        public List<string> CustomNames { get; set; }
        public ElementStat()
        {
            CustomNames = new List<string>();
            FreeNumbers = new List<int>();
        }

        public void AddFreeNumber(int number)
        {
            FreeNumbers.Add(number);
            FreeNumbers.Sort();
        }
    }
}
