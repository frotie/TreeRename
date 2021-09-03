using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeRename.TreeElements;
using TreeRename.TreeElements.BaseElements;
using TreeRename.TreeElements.SpecialElements;

namespace TreeRename
{
    class Program
    {
        static void Main(string[] args)
        {
            Apartments aps = new Apartments();

            FloorElement fe1 = new FloorElement();
            FloorElement fe2 = new FloorElement();


            // Appending
            aps.AddChild(fe1);
            aps.AddChild(fe2);

            FillFloor(fe1);
            FillFloor(fe2);

            PrintTree(aps);
            Console.ReadKey();

            Console.ReadKey();
        }

        private static void FillFloor(FloorElement floor)
        {
            for(int i = 0; i < 2; ++i)
            {
                var room = new Room();
                floor.AddChild(room);
                
                for(int j = 0; j < 4; ++j)
                {
                    room.AddChild(new WallElement());
                }
            }
        }

        private static void PrintTree(IElement root, int row = 0)
        {
            
            string offset = string.Join("", Enumerable.Repeat("  ", row));
            foreach (var el in root.Children)
            {
                Console.WriteLine(offset + el.Name + (el.Children.Count > 0 ? ":":""));
                PrintTree(el, row + 1);
            }
        }
    }
}
