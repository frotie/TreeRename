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

            var room = new Room();
            var bRoom = new BigRoom();

            aps.AddChild(room);
            aps.AddChild(bRoom);

            PrintTree(aps);

            Console.ReadKey();
        }

        private static void AddMuchElements(IElement root, int rows, int cols)
        {
            for(int i = 0; i < rows; ++i)
            {
                var room = new Room();
                root.AddChild(room);

                for(int j = 0; j < cols; ++j)
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
