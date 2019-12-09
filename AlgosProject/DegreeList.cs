using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{
    class DegreeList
    {
        int numCourses;
        public int maxDegree = -1;
        public int minDegree = int.MaxValue;
        public Vertex[] DL;

        public DegreeList(int NumCourses)
        {
            numCourses = NumCourses;
            DL = new Vertex[numCourses + 1];

            for (int i = 0; i < numCourses + 1; i++)
            {
                DL[i] = null;
            }
        }

        public bool Insert(Vertex v)
        {
            if (v.course <= 0 || v.degree < 0 || v.degree > numCourses)
            {
                return false;
            }

            if (v.degree > maxDegree)
                maxDegree = v.degree;

            if (v.degree < minDegree)
                minDegree = v.degree;

            if (DL[v.degree] == null)
            {
                DL[v.degree] = v;
            }
            else
            {
                DL[v.degree].DegPush(v);
            }

            return true;
        }

        public void Print()
        {

            for (int i = 0; i < DL.Length; i++)
            {
                if (DL[i] == null)
                    continue;

                Vertex curr = DL[i];
                string result = "Degree-" + i.ToString() + ": ";
                while (curr != null)
                {
                    result += " " + curr.course.ToString();
                    curr = curr.degNext;
                }

                Console.WriteLine(result);
            }
        }

        public int GetMinColors(ref AdjList adjList, ref Stack stack)
        {
            int result = 0;

            Vertex curr = DL[minDegree];

            while (curr != null)
            {
                curr = curr.Delete(ref adjList, this, ref stack);
                if (stack.Size() == 2)
                {
                    Console.WriteLine("stop");
                }
            }

            stack.Print();

            return result;
        }
    }
}
