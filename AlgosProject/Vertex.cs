﻿using System;
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
        public int deletedDegree = -1;
        public Vertex degPrev = null;
        public Vertex degNext = null;

        public int[] bannedColors;

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

        public void Delete(ref AdjList adjList, ref Stack stack)
        {
            deletedDegree = degree;
            stack.Push(this);
    
            adjList.TraverseOnDelete(course);
        }

        public int SmallestLastPass(int numColors, ref AdjList adjList)
        {
            if (bannedColors == null)
            {
                bannedColors = new int[numColors];
            }
            for (int i = 0; i < numColors; i++)
            {
                if (bannedColors[i] == 0)
                {
                    color = i;
                    break;
                }
            }
            adjList.BanColor(course, color, numColors);
            return color;
        }

        public void WelshPowellPass(int numColors, ref AdjList adjList, ref Stack stack)
        {
            if (bannedColors == null)
            {
                bannedColors = new int[numColors];
            }
            for (int i = 0; i < numColors; i++)
            {
                if (bannedColors[i] == 0)
                {
                    color = i;
                    break;
                }
            }
            adjList.BanColor(course, color, numColors);
            stack.Push(this);
        }

        public void RandomPass(ref AdjList adjList, ref Stack stack)
        {
            int newColor = 0;
            while (adjList.IsAdjacentToColored(course, newColor))
                ++newColor;

            color = newColor;
            stack.Push(this);
        }
    }
}
