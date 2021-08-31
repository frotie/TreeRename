using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeRename.TreeElements;

namespace TreeRename
{
    class Program
    {
        static void Main(string[] args)
        {
            Apartments aps = new Apartments();

            Room kitchen = new Room();
            Room livingRoom = new Room();
            Room bedroom = new Room();

            // Appending
            aps.AddChild(kitchen);
            aps.AddChild(livingRoom);

            for (int i = 0; i < 4; ++i)
            {
                WallElement wall1 = new WallElement();
                WallElement wall2 = new WallElement();

                kitchen.AddChild(wall1);
                livingRoom.AddChild(wall2);
            }

            Console.WriteLine("Appending\n\n");
            PrintTree(aps);
            Console.WriteLine("=========\n\n");

            // Renaming + appending
            kitchen.Rename("Кухня");
            aps.AddChild(bedroom);

            Console.WriteLine("Renaming + appending\n\n");
            PrintTree(aps);
            Console.WriteLine("=========\n\n");


            // Removing
            aps.RemoveChild(livingRoom);

            Console.WriteLine("Removing\n\n");
            PrintTree(aps);
            Console.ReadKey();
        }

        private static void PrintTree(IElement root)
        {
            foreach (var el in root.Children)
            {
                Console.WriteLine(el.Name + " impl of " + el.BaseElement?.Name);
                PrintTree(el);
            }
        }
    }
}
