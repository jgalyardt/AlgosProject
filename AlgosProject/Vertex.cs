using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{
    class Vertex
    {
        public int course;
        public int degree;
        public int color;
        public int deletedDegree;
        public Vertex degPrev = null;
        public Vertex degNext = null;

        public Vertex(int Course)
        {
            course = Course;
            degree = -1;
            color = -1;
        }

        public void DegPush(Vertex v)
        {
            if (degNext == null)
            {
                degNext = v;
                v.degPrev = this;
            }
            else
            {
                degNext.DegPush(v);
            }
        }

        public void Delete(ref AdjList adjList, DegreeList degList)
        {
            deletedDegree = degree;
            degList.DL[degree] = degNext;
            adjList.TraverseOnDelete(course, ref degList);
        }
    }
}
