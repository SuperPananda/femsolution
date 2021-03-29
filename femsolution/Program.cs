using femsolution.Mesh;
using System;

namespace femsolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new ReadMesh();

            f.ReadMeshFile();

            Console.Write("Введите свое имя: ");
            string name = Console.ReadLine();       // вводим имя
        }
    }
}
