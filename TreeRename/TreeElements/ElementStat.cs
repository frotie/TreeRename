using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public class ElementStat
    {
        private readonly string BaseName;
        private readonly List<uint> FreeNumbers;
        private readonly List<string> CustomNames;
        private uint ElementsCount;
        public ElementStat(string baseName)
        {
            BaseName = baseName;
            CustomNames = new List<string>();
            FreeNumbers = new List<uint>();
        }

        public string GetNextDefaultName()
        {
            uint number;
            ElementsCount++;
            if (FreeNumbers.Count > 0)
            {
                number = FreeNumbers[0];
                FreeNumbers.RemoveAt(0);
            }
            else
                number = ElementsCount;

            string name = BuildStandartName(number);

            return name;
        }

        public string[] GetDefaultNamesList(uint itemCount)
        {
            string[] result = new string[itemCount];
            int freeCount = FreeNumbers.Count;

            for (uint i = 0; i < itemCount; ++i)
            {
                uint number;
                if (i < freeCount)
                    number = FreeNumbers[(int)i];
                else
                    number = ElementsCount + i + 1;

                result[i] = BuildStandartName(number);
            }

            ElementsCount += itemCount;
            FreeNumbers.RemoveRange(0, (int)Math.Min(itemCount, freeCount));

            return result;
        }

        public void RemoveItem(string name)
        {
            if (CustomNames.Contains(name))
            {
                CustomNames.Remove(name);
            }
            else
            {
                uint number = GetNumberFromName(name);
                AddFreeNumber(number);
                ElementsCount--;
            }
        }

        public string ChangeItemName(string oldName, string name)
        {
            if (CustomNames.Contains(name))
                throw new ArgumentException();

            string result;
            RemoveItem(oldName);
            if (name == BaseName)
            {
                result = GetNextDefaultName();
            }
            else
            {
                CustomNames.Add(name);
                result = name;
            }

            return result;
        }

        private void AddFreeNumber(uint number)
        {
            for(int i = 0; i < FreeNumbers.Count; ++i)
            {
                if(FreeNumbers[i] > number)
                {
                    FreeNumbers.Insert(i, number);
                    return;
                }
            }
            FreeNumbers.Add(number);
        }

        private string BuildStandartName(uint number) =>
            BaseName + " " + number.ToString();

        private static uint GetNumberFromName(string name)
        {
            if (!name.Contains(' '))
                throw new ArgumentException("Element name gave in incorrect format");

            return uint.Parse(name.Split(' ').Last());
        }
    }
}
