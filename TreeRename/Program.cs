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
            int elementsCount = (int)1e6;
            Apartments aps = new Apartments();

            var list = new List<IElement>();
            for (int i = 0; i < elementsCount; ++i)
            {
                list.Add(new Room());
            }

            var time = DateTime.Now;
            aps.AddChildren(list);
            TimeSpan resTime = DateTime.Now - time;

            Console.WriteLine($"Spec method time: {resTime.TotalMilliseconds}ms");

            Apartments aps1 = new Apartments();

            time = DateTime.Now;
            for(int i = 0; i < elementsCount; ++i)
            {
                aps1.AddChild(list[i]);
            }
            resTime = DateTime.Now - time;
            Console.WriteLine($"Often method time: {resTime.TotalMilliseconds}ms");

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
