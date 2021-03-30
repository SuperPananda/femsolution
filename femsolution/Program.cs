using femsolution.Mesh;
using System;

namespace femsolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new ReadingMesh();

            f.ReadingFromFile();

            Console.Write("Введите свое имя: ");
            string name = Console.ReadLine();       // вводим имя
        }
    }
}
