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
            StreamReader srE = new StreamReader(pathToE);
            
            //Skip to second line since first is always 0 (course 0 doesn't exist)
            String pLine = srP.ReadLine();
            pLine = srP.ReadLine();

            String eLine = srE.ReadLine();
            eLine = srE.ReadLine();

            int curr;
            int prev;
            int.TryParse(pLine, out prev);

            while (pLine != null)
            {
                pLine = srP.ReadLine();
                while (pLine == "0")
                {
                    pLine = srP.ReadLine();
                }
                if (pLine == null)
                {
                    while (eLine != null)
                    {
                        Console.WriteLine(prev.ToString() + " - " + eLine);
                        eLine = srE.ReadLine();
                    }
                }
                else 
                {
                    int.TryParse(pLine, out curr);
                    for (int i = 0; i < curr - prev; i++)
                    {
                        Console.WriteLine(prev.ToString() + " - " + eLine);
                        eLine = srE.ReadLine();
                    }
                    prev = curr;
                }
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
