﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{

    class Stack
    {
        int top = 0;
        Vertex[] stack;

        public Stack(int size)
        {
            stack = new Vertex[size];
        }

        public void Push(Vertex v)
        {
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
            return top - 1;
        }
    }
}
