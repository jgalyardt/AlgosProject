using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlgosProject
{
    class GraphHandler
    {
        string pathToP;
        string pathToE;
        bool graphBuilt = false;

        public GraphHandler(string pPath, string ePath)
        {
            pathToP = pPath;
            pathToE = ePath;
        }

        public void BuildGraph()
        {
            StreamReader srP = new StreamReader(pathToP);
            String line = srP.ReadLine();
            while (line != null)
            {
                try
                {

                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{line}'");
                }

                line = srP.ReadLine();
            }
        }

        public void SmallestLast()
        {
            if (!graphBuilt)
            {
                BuildGraph();
            }

        }
    }
}
