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
        private readonly List<int> FreeNumbers;
        private readonly List<string> CustomNames;
        private int StandartElementsCount;
        public ElementCounter(string baseName)
        {
            BaseName = baseName;
            CustomNames = new List<string>();
            FreeNumbers = new List<int>();
        }

        public string GetNextDefaultName()
        {
            int number;
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

        public string[] TakeDefaultNamesList(int itemCount)
        {
            string[] result = new string[itemCount];
            int freeCount = FreeNumbers.Count;

            for (int i = 0; i < itemCount; ++i)
            {
                int number;
                if (i < freeCount)
                    number = FreeNumbers[i];
                else
                    number = StandartElementsCount + i + 1;

                result[i] = BuildStandartName(number);
            }

            StandartElementsCount += itemCount;
            FreeNumbers.RemoveRange(0, Math.Min(itemCount, freeCount));

            return result;
        }

        public void RemoveItem(string name)
        {
            int itemNumber = GetNumberFromName(name);
            if(itemNumber == 0)
            {
                if (CustomNames.Contains(name))
                {
                    CustomNames.Remove(name);
                }
            }
            else
            {
                AddFreeNumber(itemNumber);
                StandartElementsCount--;
            }
        }

        public bool ChangeItemName(string oldName, string name)
        {
            if (CustomNames.Contains(name))
                throw new ArgumentException();

            int nameNumber = GetNumberFromName(name);
            string baseItemName = GetNameFromStandartName(name);

            if (baseItemName == BaseName && nameNumber != 0)
            {
                if (FreeNumbers.Contains(nameNumber))
                {
                    FreeNumbers.Remove(nameNumber);
                }
                else if (StandartElementsCount < nameNumber)
                {
                    int offset = StandartElementsCount + FreeNumbers.Count + 1;
                    FreeNumbers.AddRange(Enumerable.Range(offset, nameNumber - offset));
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

        private void AddFreeNumber(int number)
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

        private string BuildStandartName(int number) =>
            BaseName + " " + number.ToString();

        private static int GetNumberFromName(string name)
        {
            int result = 0;
            if (!name.Contains(' '))
                return 0;

            if (int.TryParse(name.Split(' ').Last(), out result))
                return result;
            else 
                return 0;
        }
        private static string GetNameFromStandartName(string name)
        {
            if (!name.Contains(' '))
                return "";

            int nameNumber = GetNumberFromName(name);
            if(nameNumber != 0)
            {
                string result = name.Substring(0, name.Length - nameNumber.ToString().Length - 1);
                return result;
            }
            return "";
        }
    }
}
