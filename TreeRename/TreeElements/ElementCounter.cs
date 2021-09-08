using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRename.TreeElements
{
    public class ElementCounter
    {
        private readonly string BaseName;
        private readonly List<uint> FreeNumbers;
        private readonly List<string> CustomNames;
        private uint StandartElementsCount;
        public ElementCounter(string baseName)
        {
            if (string.IsNullOrEmpty(baseName))
                throw new ArgumentNullException();

            BaseName = baseName;
            CustomNames = new List<string>();
            FreeNumbers = new List<uint>();
        }

        public string GetNextDefaultName()
        {
            uint number;
            if (FreeNumbers.Count > 0)
                number = FreeNumbers[0];
            else
                number = StandartElementsCount + 1;

            string name = BuildStandartName(number);
            return name;
        }
        public void TakeNextDefaultName()
        {
            StandartElementsCount++;
            if (FreeNumbers.Count > 0)
                FreeNumbers.RemoveAt(0);
        }

        public string[] TakeDefaultNamesList(uint itemCount)
        {
            if(itemCount == 0)
                throw new ArgumentOutOfRangeException();

            string[] result = new string[itemCount];
            int freeCount = FreeNumbers.Count;

            for (uint i = 0; i < itemCount; ++i)
            {
                uint number;
                if (i < freeCount)
                    number = FreeNumbers[(int)i];
                else
                    number = StandartElementsCount + i + 1;

                result[i] = BuildStandartName(number);
            }

            StandartElementsCount += itemCount;
            FreeNumbers.RemoveRange(0, (int)Math.Min(itemCount, freeCount));

            return result;
        }

        public bool RemoveItem(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            string baseParamName = GetNameFromStandartName(name);
            uint itemNumber = GetNumberFromName(name);

            if(baseParamName == BaseName && itemNumber != 0)
            {
                if(!FreeNumbers.Contains(itemNumber) && StandartElementsCount > 0)
                {
                    AddFreeNumber(itemNumber);
                    StandartElementsCount--;

                    return true;
                }
            }
            else
            {
                if (CustomNames.Contains(name))
                {
                    CustomNames.Remove(name);
                    return true;
                }
            }
            return false;
        }

        public bool ChangeItemName(string oldName, string name)
        {
            if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            if (CustomNames.Contains(name))
                throw new ArgumentException();

            uint nameNumber = GetNumberFromName(name);
            string baseItemName = GetNameFromStandartName(name);

            if (baseItemName == BaseName && nameNumber != 0)
            {
                if (FreeNumbers.Contains(nameNumber))
                {
                    FreeNumbers.Remove(nameNumber);
                }
                else if (StandartElementsCount < nameNumber)
                {
                    uint offset = (uint)(StandartElementsCount + FreeNumbers.Count + 1);
                    for (uint i = offset; i < nameNumber; ++i)
                        FreeNumbers.Add(i);
                }
                else
                {
                    return false;
                }

                StandartElementsCount++;
            }
            else
            {
                CustomNames.Add(name);
            }

            RemoveItem(oldName);
            return true;
        }

        private void AddFreeNumber(uint number)
        {
            if (number <= 0)
                throw new ArgumentException();

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
            if (string.IsNullOrEmpty(name) || !name.Contains(' '))
                return 0;

            if (uint.TryParse(name.Split(' ').Last(), out uint result))
                return result;

            return 0;
        }
        private static string GetNameFromStandartName(string name)
        {
            if (string.IsNullOrEmpty(name) || !name.Contains(' '))
                return "";

            uint nameNumber = GetNumberFromName(name);
            if(nameNumber != 0)
            {
                string result = name.Substring(0, name.Length - nameNumber.ToString().Length - 1);
                return result;
            }
            return "";
        }
    }
}
