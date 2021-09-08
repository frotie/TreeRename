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
            int elementsCount = (int)5e4;
            Apartments aps = new Apartments();

            //for(int i = 0; i < 10; ++i)
            //{
            //    var room = new Room();
            //    aps.AddChild(room);

            //    if (i % 2 == 0)
            //        aps.RemoveChild(room);
            //}

            //Console.WriteLine(aps.Children.First().Rename("Помещение 6"));

            //PrintTree(aps);


            CRUD(aps);
            // SpeedTest(elementsCount);

            Console.ReadKey();
        }

        private static void SpeedTest(int elementsCount)
        {
            Apartments aps1 = new Apartments();
            Apartments aps2 = new Apartments();
            AppendElements(aps1, aps2, elementsCount);


            Console.WriteLine("Deleting some elements");
            int elsToDeleteCount = new Random().Next(0, elementsCount);

            var elementsToDelete1 = Enumerable
                .Range(0, elsToDeleteCount)
                .Select(e => aps1.Children[e])
                .ToList();

            var elementsToDelete2 = Enumerable
                .Range(0, elsToDeleteCount)
                .Select(e => aps2.Children[e])
                .ToList();

            Console.WriteLine($"Elements to delete: {elsToDeleteCount}");

            var time = DateTime.Now;
            aps1.RemoveChildren(elementsToDelete1);
            TimeSpan resSpecTimeRemove = DateTime.Now - time;
            Console.WriteLine($"[-] Spec method time: {resSpecTimeRemove.TotalMilliseconds}ms");

            time = DateTime.Now;
            for (int i = 0; i < elsToDeleteCount; ++i)
            {
                aps2.RemoveChild(elementsToDelete2[i]);
            }
            TimeSpan resOftenTimeRemove = DateTime.Now - time;

            Console.WriteLine($"[-] Usual method time: {resOftenTimeRemove.TotalMilliseconds}ms\n");

            AppendElements(aps1, aps2, elementsCount);

            Console.WriteLine($"First tree elements: {aps1.Children.Count}");
            Console.WriteLine($"Second tree elements: {aps2.Children.Count}");
        }

        private static void CRUD(IElement element)
        {
            var room1 = new Room();
            var room2 = new Room();
            var room3 = new Room();

            element.AddChildren(room1, room2, room3);
            PrintTree(element);
            Console.WriteLine("\n");

            element.RemoveChild(room2);
            PrintTree(element);
            Console.WriteLine("\n");

            room1.Rename("kitchen");
            PrintTree(element);
            Console.WriteLine("\n");

            element.AddChild(new Room());
            element.AddChild(new Room());
            element.AddChild(new Room());

            
            PrintTree(element);
            Console.WriteLine("\n");

            room1.Rename("Помещение 6");
            PrintTree(element);
            Console.WriteLine("\n");

            for (int i = 0; i < 3; ++i)
            {
                element.AddChild(new Room());
            }

            PrintTree(element);
            Console.WriteLine("\n");
        }

        private static void AppendElements(IElement aps1, IElement aps2, int elementsCount)
        {
            var list = new List<IElement>();
            for (int i = 0; i < elementsCount; ++i)
            {
                list.Add(new Room());
            }

            Console.WriteLine("Appending elements");
            Console.WriteLine($"All elements count to append: {list.Count}");

            var time = DateTime.Now;
            aps1.AddChildren(list.ToArray());
            TimeSpan resSpecTimeAdd = DateTime.Now - time;
            Console.WriteLine($"[-] Spec method time: {resSpecTimeAdd.TotalMilliseconds}ms");

            time = DateTime.Now;
            for (int i = 0; i < elementsCount; ++i)
            {
                aps2.AddChild(list[i]);
            }
            TimeSpan resOftenTimeAdd = DateTime.Now - time;

            Console.WriteLine($"[-] Usual method time: {resOftenTimeAdd.TotalMilliseconds}ms\n");
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
