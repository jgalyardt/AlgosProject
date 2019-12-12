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
        int maxColor = -1;
        int sizeAtLastIncrease = 0;
        int maxDegreeDeleted = 0;
        public Vertex[] stack;

        public Stack(int size)
        {
            stack = new Vertex[size];
        }

        public void Push(Vertex v)
        {
            //This will track the last time the deleted degree increased, finding the lower bound on number of colors needed
            if (top > 0 && v.deletedDegree > stack[top - 1].deletedDegree)
            {
                degreeAtLastIncrease = v.deletedDegree;
                sizeAtLastIncrease = top;
            }

            if (v.deletedDegree > maxDegreeDeleted)
                maxDegreeDeleted = v.deletedDegree;

            if (v.color > maxColor)
                maxColor = v.color;


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

        public Vertex Peek()
        {
            if (top < 0)
                return null;
            return stack[top - 1];
        }

        public int Size()
        {
            return top;
        }

        public void Print()
        {
            Console.WriteLine("Course\t\tDeg When Deleted\t\tOrder Colored\t\tColor");
            for (int i = top - 1; i >= 0; i--)
            {
                Console.WriteLine(stack[i].course.ToString() + "\t\t\t" + stack[i].deletedDegree.ToString() + "\t\t\t\t\t\t" + (top - i).ToString() + "\t\t\t\t\t\t\t" + stack[i].color.ToString());
            }
        }

        public void PrintDecreasing()
        {
            Console.WriteLine("Course\t\tDegree\t\tOrder Colored\t\tColor");
            for (int i = top - 1; i >= 0; i--)
            {
                Console.WriteLine(stack[i].course.ToString() + "\t\t\t" + stack[i].degree.ToString() + "\t\t\t" + (i).ToString() + "\t\t\t\t\t" + stack[i].color.ToString());
            }
        }

        public bool IsEmpty()
        {
            return top == 0;
        }

        public int GetColorsLowerBound()
        {
            return degreeAtLastIncrease;
        }

        public int GetMaxDegreeDeleted()
        {
            return maxDegreeDeleted;
        }

        public int GetTerminalCliqueSize()
        {
            return stack.Length - 1 - sizeAtLastIncrease;
        }

        public int GetMaxColor()
        {
            return maxColor;
        }
    }
}
