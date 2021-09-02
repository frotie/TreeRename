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

            Room room1 = new Room();
            Room room2 = new Room();

            // Appending
            aps.AddChild(room1);
            aps.AddChild(room2);

            Console.WriteLine("Initial tree");
            PrintTree(aps);
            Console.WriteLine();

            Console.WriteLine("Removed room1");
            aps.RemoveChild(room1);
            PrintTree(aps);
            Console.WriteLine();

            Console.WriteLine("Renamed room2 to kitchen");
            room2.Rename("kitchen");
            PrintTree(aps);
            Console.WriteLine();

            Console.WriteLine("Added room");
            aps.AddChild(new Room());
            PrintTree(aps);
            Console.WriteLine();

            Console.WriteLine("Added room");
            aps.AddChild(new Room());
            PrintTree(aps);
            Console.WriteLine();

            Console.WriteLine("Added room");
            aps.AddChild(new Room());
            PrintTree(aps);
            Console.WriteLine();

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
