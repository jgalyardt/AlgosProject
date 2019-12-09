using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{
    class AdjVertex
    {
        public AdjVertex next = null;
        public Vertex target;

        public AdjVertex(Vertex t)
        {
            target = t;
        }
    }
}
