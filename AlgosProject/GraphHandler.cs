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

        int numCourses = -1;

        AdjList adjList;
        DegreeList degList;
        Vertex[] verticies;

        public GraphHandler(string pPath, string ePath)
        {
            pathToP = pPath;
            pathToE = ePath;
        }

        public void BuildGraph()
        {
            StreamReader srP = new StreamReader(pathToP);
            StreamReader srE = new StreamReader(pathToE);
            
            //First line of P tells you the number of courses
            String pLine = srP.ReadLine();
            int.TryParse(pLine, out numCourses);
            //Allocate memory
            adjList = new AdjList(numCourses + 1);
            degList = new DegreeList(numCourses + 1);
            verticies = new Vertex[numCourses + 1];

            pLine = srP.ReadLine();

            //First line of E is always 0 so skip it
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

                        AddVertex(parsed, courseNumber);

                        //Console.WriteLine(courseNumber.ToString() + " - " + eLine);
                        eLine = srE.ReadLine();
                    }
                }
                else 
                {
                    int.TryParse(pLine, out curr);
                    int difference = curr - prev;
                    for (int i = 0; i < difference; i++)
                    {
                        int.TryParse(eLine, out parsed);

                        AddVertex(parsed, courseNumber);

                        //Console.WriteLine(courseNumber.ToString() + " - " + eLine);
                        eLine = srE.ReadLine();
                    }
                    prev = curr;
                    courseNumber = courseNumber + extra + 1;
                    extra = 0;
                }
            }

            adjList.Print();
            adjList.BuildDegreeList(ref verticies, ref degList);
            degList.Print();
            graphBuilt = true;
        }

        private void AddVertex(int a, int b)
        {
            //Given two courses, a and b, create them if they don't exist and add them to each other's adj list
            if (verticies[a] == null)
            {
                verticies[a] = new Vertex(a);
            }
            adjList.Insert(b, verticies[a]);

            if (verticies[b] == null)
            {
                verticies[b] = new Vertex(b);
            }
            adjList.Insert(a, verticies[b]);
        }

        public void SmallestLast()
        {
            if (!graphBuilt)
            {
                BuildGraph();
            }

            for (int i = 0; i < degList.maxDegree; i++)
            {
                Vertex curr = degList.DL[i];
                
                while (curr != null)
                {
                    //TODO
                }
            }


        }
    }
}
