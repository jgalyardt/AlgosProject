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

        AdjList adjList;

        public GraphHandler(string pPath, string ePath)
        {
            pathToP = pPath;
            pathToE = ePath;

            adjList = new AdjList(10000);
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

            int courseNumber = 1;
            int extra = 0;
            int parsed;
            int curr;
            int prev;
            int.TryParse(pLine, out prev);

            while (pLine != null)
            {
                pLine = srP.ReadLine();
                while (pLine == "0")
                {
                    ++extra;
                    pLine = srP.ReadLine();
                }
                if (pLine == null)
                {
                    while (eLine != null)
                    {
                        int.TryParse(eLine, out parsed);
                        Vertex v = new Vertex(parsed);
                        adjList.Insert(courseNumber, v);
                        //Console.WriteLine(courseNumber.ToString() + " - " + eLine);
                        eLine = srE.ReadLine();
                    }
                }
                else 
                {
                    int.TryParse(pLine, out curr);
                    for (int i = 0; i < curr - prev; i++)
                    {
                        int.TryParse(eLine, out parsed);
                        Vertex v = new Vertex(parsed);
                        adjList.Insert(courseNumber, v);
                        //Console.WriteLine(courseNumber.ToString() + " - " + eLine);
                        eLine = srE.ReadLine();
                    }
                    prev = curr;
                    courseNumber = courseNumber + extra + 1;
                    extra = 0;
                }
            }

            adjList.Print();
            graphBuilt = true;
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
