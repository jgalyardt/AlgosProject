using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{

    class AdjList
    {
        public AdjVertex[] AL;
        public int size;

        public AdjList(int Size)
        {
            size = Size;
            AL = new AdjVertex[size];
        }

        public void Insert(int courseNumber, Vertex v)
        {
            AdjVertex temp = new AdjVertex(v);
            if (AL[courseNumber] == null)
            {
                AL[courseNumber] = temp;
            }
            else
            {
                temp.next = AL[courseNumber];
                AL[courseNumber] = temp;
            }
            
        }

        public void Print()
        {
            for (int i = 0; i < AL.Length; i++)
            {
                if (AL[i] == null)
                    continue;

                AdjVertex curr = AL[i];
                string result = i.ToString() + ": ";
                while(curr != null)
                {
                    result += " " + curr.target.course.ToString();
                    curr = curr.next;
                }

                Console.WriteLine(result);
            }
        }

        public void BuildDegreeList(ref Vertex[] verticies, ref DegreeList degList)
        {
            for (int i = 0; i < AL.Length; i++)
            {
                if (AL[i] == null)
                    continue;

                int count = 0;
                AdjVertex curr = AL[i];
                while (curr != null)
                {
                    ++count;
                    curr = curr.next;
                }
                verticies[i].degree = count;
                degList.Insert(verticies[i]);
            }
        }

        public bool TraverseOnDelete(int course, int removedDegree, ref DegreeList degList, ref Stack stack)
        {
            AdjVertex curr = AL[course];
            bool backtrackFlag = false;
            while (curr != null)
            {
                if (curr.target.deletedDegree != -1)
                {
                    curr = curr.next;
                    continue;
                }

                int newDegree = --curr.target.degree;

                if (newDegree == -1)
                    return false;

                if (newDegree <= removedDegree)
                    backtrackFlag = true;

                degList.MoveVertex(curr.target, newDegree);

                curr = curr.next;
            }
            return backtrackFlag;
        }
    }

}
