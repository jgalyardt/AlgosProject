using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{
    class Vertex
    {
        public int name;
        public int degree;
        public int color;
        public Vertex degPrev = null;
        public Vertex degNext = null;
        public bool deleted;

        public Vertex(int Name)
        {
            name = Name;
            degree = -1;
            color = -1;
            deleted = false;
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
    }
}
