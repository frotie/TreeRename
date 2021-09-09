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
        public void UseNextDefaultName()
        {
            StandartElementsCount++;
            if (FreeNumbers.Count > 0)
                FreeNumbers.RemoveAt(0);
        }

        public string[] TakeDefaultNamesList(uint itemCount)
        {
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

            if(IsNameDefaultTemplate(name))
            {
                uint itemNumber = GetNumberFromName(name);
                return TryRealizeNumber(itemNumber);
            }
            else
            {
                return TryRealizeCustomName(name);
            }
        }

        public bool ChangeItemName(string oldName, string name)
        {
            if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            if (IsNameDefaultTemplate(name))
            {
                if (!TryAddCustomStandartName(name))
                {
                    return false;
                }
            }
            else if (!TryAddCustomName(name))
            {
                return false;
            }

            return RemoveItem(oldName);
        }



        private void AddFreeNumberWithSort(uint number)
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

        private bool IsNameDefaultTemplate(string name)
        {
            if (string.IsNullOrEmpty(name) || !name.Contains(' '))
                return false;

            uint nameNumber = GetNumberFromName(name);
            if(nameNumber != 0)
            {
                string result = name.Substring(0, name.Length - nameNumber.ToString().Length - 1);
                return result == BaseName;
            }
            return false;
        }

        private static uint GetNumberFromName(string name)
        {
            if (uint.TryParse(name.Split(' ').Last(), out uint result))
                return result;

            return 0;
        }

        private bool TryRealizeNumber(uint number)
        {
            if (!FreeNumbers.Contains(number) && StandartElementsCount > 0)
            {
                AddFreeNumberWithSort(number);
                StandartElementsCount--;

                return true;
            }
            return false;
        }
        private bool TryRealizeCustomName(string name)
        {
            if (CustomNames.Contains(name))
            {
                CustomNames.Remove(name);
                return true;
            }
            return false;
        }
        private bool TryAddCustomName(string name)
        {
            if (!CustomNames.Contains(name))
            {
                CustomNames.Add(name);
                return true;
            }
            return false;
        }

         /// <summary>
         /// Добавляет пользовательское стандартное имя
         /// </summary>
         /// <param name="name"></param>
         /// <returns></returns>
        private bool TryAddCustomStandartName(string name)
        {
            uint nameNumber = GetNumberFromName(name);
            int index;

            if ((index = FreeNumbers.IndexOf(nameNumber)) != -1)
            {
                FreeNumbers.RemoveAt(index);
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
            return true;
        }
    }
}
