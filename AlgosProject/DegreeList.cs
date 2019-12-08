using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{
    class DegreeList
    {
        int maxDegree;
        public Vertex[] DL;

        public DegreeList(int MaxDegree)
        {
            maxDegree = MaxDegree;
            DL = new Vertex[maxDegree + 1];

            for (int i = 0; i < maxDegree + 1; i++)
            {
                DL[i] = null;
            }
        }

        public bool Insert(Vertex v)
        {
            if (v.course <= 0 || v.degree < 0 || v.degree > maxDegree)
            {
                return false;
            }

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
    }
}
