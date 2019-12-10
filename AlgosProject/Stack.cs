using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{

    class Stack
    {
        int top = 0;
        int degreeAtLastIncrease = int.MaxValue;
        int maxDegreeDeleted = 0;
        Vertex[] stack;

        public Stack(int size)
        {
            stack = new Vertex[size];
        }

        public void Push(Vertex v)
        {
            //This will track the last time the deleted degree increased, finding the lower bound on number of colors needed
            if (top > 0 && v.deletedDegree > stack[top - 1].deletedDegree)
                degreeAtLastIncrease = v.deletedDegree;

            if (v.deletedDegree > maxDegreeDeleted)
                maxDegreeDeleted = v.deletedDegree;

            stack[top] = v;
            ++top;
        }

        public Vertex Pop()
        {
            --top;
            if (top < 0)
                return null;
            return stack[top];
        }

        public int Size()
        {
            return top;
        }

        public void Print()
        {
            Console.WriteLine("(top)");
            for (int i = top - 1; i >= 0; i--)
            {
                Console.WriteLine(stack[i].course.ToString() + " " + stack[i].deletedDegree.ToString());
            }
            Console.WriteLine("(bottom)");
        }

        public bool IsEmpty()
        {
            return top == 0;
        }

        public int GetColorsLowerBound()
        {
            return degreeAtLastIncrease;
        }

        public int GetMinColors()
        {
            return maxDegreeDeleted + 1;
        }
    }
}
