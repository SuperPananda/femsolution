using femsolution.Mesh;
using System;

namespace femsolution
{
    class Program
    {
        static void Main(string[] args)
        {
            //var area = new Area().GetArea("guad4.msh");

            var nodes = new ReadingMesh("guad5.msh").GetNodes();

            new AnalyticalSolution().GetAnalyticalSolution(nodes);

            Console.Write("Введите свое имя: ");
            string name = Console.ReadLine();       // вводим имя
        }
    }
}
