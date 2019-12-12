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
        bool verbose = false;
        bool timingMode = false;

        int numCourses = -1;

        AdjList adjList;
        DegreeList degList;
        Vertex[] verticies;
        Stack stack;

        public GraphHandler(string pPath, string ePath, bool Verbose, bool TimingMode)
        {
            pathToP = pPath;
            pathToE = ePath;
            verbose = Verbose;
            timingMode = TimingMode;
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
            stack = new Stack(numCourses + 1);

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
                        eLine = srE.ReadLine();
                    }
                    prev = curr;
                    courseNumber = courseNumber + extra + 1;
                    extra = 0;
                }
            }
            
            adjList.BuildDegreeList(ref verticies, ref degList);
            if (verbose)
            {
                adjList.Print();
                degList.Print();
            }
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

        public long SmallestLast()
        {
            if (!graphBuilt)
            {
                BuildGraph();
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            int numColors = degList.GetMinColors(ref adjList, ref stack);

            int maxColor = -1;
            //Go back through stack and assign colors starting at the top (which is really an array... I know I know)
            for (int i = stack.Size() - 1; i >= 0; i--)
            {
                int colorResult = stack.stack[i].SmallestLastPass(numColors, ref adjList);
                if (colorResult > maxColor)
                    maxColor = colorResult;
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            if (!timingMode)
            {
                Console.WriteLine("Smallest Last Results:");
                if (verbose)
                {
                    stack.Print();
                }
                Console.WriteLine("Number of colors used: " + (maxColor + 1).ToString());
                Console.WriteLine("Terminal clique size: " + stack.GetTerminalCliqueSize().ToString());
                Console.WriteLine("Maximmum degree deleted: " + stack.GetMaxDegreeDeleted().ToString());
                Console.WriteLine("Lower bound on colors: " + stack.GetColorsLowerBound().ToString());
                Console.WriteLine("Completed in " + elapsedMs + "ms");
            }
            return elapsedMs;
        }

        public long WelshPowell()
        {
            if (!graphBuilt)
            {
                BuildGraph();
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            degList.WelshPowell(ref adjList, ref stack);
            
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            if (!timingMode)
            {
                Console.WriteLine("Welsh Powell Results:");
                if (verbose)
                {
                    stack.PrintDecreasing();
                }
                Console.WriteLine("Number of colors used: " + stack.GetMaxColor().ToString());
                Console.WriteLine("Completed in " + elapsedMs + "ms");
            }

            return elapsedMs;
        }

        public long RandomOrdering()
        {
            if (!graphBuilt)
            {
                BuildGraph();
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            int[] order = new int[numCourses];
            for (int i = 0; i < order.Length; i++)
            {
                order[i] = i + 1;
            }

            Random random = new Random();

            //Fisher-Yates shuffle
            for (int i = order.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i);
                int temp = order[i];
                order[i] = order[j];
                order[j] = temp;
            }

            for (int i = 0; i < order.Length; i++)
            {
                if (verticies[order[i]] != null)
                    verticies[order[i]].RandomPass(ref adjList, ref stack);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            if (!timingMode)
            {
                Console.WriteLine("Random Results:");
                if (verbose)
                {
                    stack.PrintDecreasing();
                }
                Console.WriteLine("Number of colors used: " + stack.GetMaxColor().ToString());
                Console.WriteLine("Completed in " + elapsedMs + "ms");
            }
            return elapsedMs;
        }

        public long BogoColoring()
        {
            //Loosely based on bogo sort. 
            //Assign random colors from 0-n for each vertex. Check if valid.
            //If not, increase n and try again.

            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (!graphBuilt)
            {
                BuildGraph();
            }

            Random random = new Random();
            int numColors = 1;

            for (int i = 1; i < verticies.Length; i++)
            {
                if (verticies[i] != null)
                    verticies[i].color = random.Next(numColors);
            }

            while(adjList.HasConflicts(ref verticies))
            {
                ++numColors;
                for (int i = 1; i < verticies.Length; i++)
                {
                    if (verticies[i] != null)
                        verticies[i].color = random.Next(numColors);
                }
            }

            int numUnique = 0;
            int[] colors = new int[numColors];
            for (int i = 0; i < verticies.Length; i++)
            {
                if (verticies[i] != null && !colors.Contains(verticies[i].color))
                {
                    colors[numUnique] = verticies[i].color;
                    numUnique++;
                }
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            if (!timingMode)
            {
                Console.WriteLine("Bogo Results:");
                Console.WriteLine("Value of n at finish: " + numColors.ToString());
                Console.WriteLine("Number of colors used: " + numUnique.ToString());
                Console.WriteLine("Completed in " + elapsedMs + "ms");
            }

            return elapsedMs;
        }
    }
}
