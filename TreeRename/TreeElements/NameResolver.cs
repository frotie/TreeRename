﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public class NameResolver
    {
        private Dictionary<Type, ElementStat> _elements;
        public NameResolver()
        {
            _elements = new Dictionary<Type, ElementStat>();
        }

        public string GetName(IElement element)
        {
            if (!_elements.ContainsKey(element.GetType()))
                _elements.Add(element.GetType(), new ElementStat());

            return element.BaseName + " " + GetElementNumber(element);
        }

        // Throw exception if element was not added to the statistics. Call GetName first
        public bool Rename(IElement element, string name)
        {
            var elStat = _elements[element.GetType()];
            if(!elStat.CustomNames.Contains(name))
            {
                RemoveElement(element);
                elStat.CustomNames.Add(name);
                return true;
            }
            return false;
        }

        // Throw exception if element was not added to the statistics. Call GetName first
        public void RemoveElement(IElement element)
        {
            var elStat = _elements[element.GetType()];

            // if element name is custom
            if (elStat.CustomNames.Contains(element.Name))
            {
                elStat.CustomNames.Remove(element.Name);
            }
            else
            {
                try
                {
                    elStat.FreeNumbers.Add(GetNumberFromName(element.Name));
                }
                catch (ArgumentException)
                {
                }
            }
            
            elStat.ElementsCount--;
        }

        private int GetElementNumber(IElement element)
        {
            ElementStat elementStat = _elements[element.GetType()];
            elementStat.ElementsCount++;

            int number;
            if (elementStat.FreeNumbers.Count > 0)
            {
                number = elementStat.FreeNumbers.Min();
                elementStat.FreeNumbers.Remove(number);
            }
            else
                number = elementStat.ElementsCount;

            return number;
        }
        private int GetNumberFromName(string name)
        {
            if (!name.Contains(' ')) throw new ArgumentException("Element name gave in incorrect format");
            return int.Parse(name.Split(' ').Last());
        }
    }
    internal class ElementStat
    {
        public int ElementsCount { get; set; }
        public List<int> FreeNumbers { get; set; }
        public List<string> CustomNames { get; set; }
        public ElementStat()
        {
            CustomNames = new List<string>();
            FreeNumbers = new List<int>();
        }
    }
}
