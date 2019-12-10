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

                if (newDegree < removedDegree)
                    backtrackFlag = true;

                //Remove from current degree level
                if (curr.target.degNext == null && curr.target.degPrev == null)
                {
                    curr.target.degNext = degList.DL[newDegree];
                    degList.DL[newDegree] = curr.target;
                    degList.DL[newDegree + 1] = null;
                    curr = curr.next;
                    continue;
                }

                if (curr.target.degNext != null)
                    curr.target.degNext.degPrev = curr.target.degPrev;
                else
                    curr.target.degPrev.degNext = null;

                if (curr.target.degPrev != null)
                    curr.target.degPrev.degNext = curr.target.degNext;
                else
                    curr.target.degNext.degPrev = null;

                //Add to lower degree level at front
                curr.target.degNext = degList.DL[newDegree];
                degList.DL[newDegree] = curr.target;

                curr = curr.next;
            }
            return backtrackFlag;
        }
    }

}
