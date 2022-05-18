using System;

namespace Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Graph!");
            Digraph dg = new Digraph();
            if (!dg.LoadVertices(args[0]))
            {
                Console.WriteLine("booga booga!");
            }
            else
            {
                foreach (Vertex v in dg.Vertices)
                {
                    Console.Write(v.Name + ": ");
                    foreach (Vertex nbr in v.Neighbors)
                    {
                        Console.Write(nbr.Name + " ");
                    }
                    Console.WriteLine();
                }
                
            }
        }
    }
}
