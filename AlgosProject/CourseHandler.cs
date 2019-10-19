using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{

    class CourseHandler
    {
        private int numCourses;
        private int numStudents;
        private int coursesPerStudent;
        private Distribution distribution;

        public CourseHandler(int C, int S, int K, Distribution DIST)
        {
            numCourses = C;
            numStudents = S;
            coursesPerStudent = K;
            distribution = DIST;
        }

      
        public void MethodOne()
        {
            //The first method utilizes AVL trees to store the course data
            AVLTree[] conflicts = new AVLTree[numCourses];
            int numTrees = 0;
            int selectedTree;

            //Select courses for each student
            for (int i = 0; i < numStudents; i++)
            {
                selectedTree = -1;

                foreach (double datum in )
                {
                    //Normalize results from 0 to 1 to course numbers (1 to C)
                    double normalized = (numCourses - 1) * datum + 1;
                    int courseResult = (int)Math.Round(normalized);
                    
                    
                    for (int j = 0; j < numTrees && selectedTree == -1; j++)
                    {
                        if (conflicts[j].isInTree(courseResult))
                        {
                            selectedTree = j;
                        }
                    }
                    if (selectedTree == -1)
                    {
                        conflicts[numTrees] = new AVLTree();
                        selectedTree = numTrees;
                        numTrees++;
                        
                    }
                    conflicts[selectedTree].insert(courseResult);
                }
            }

            //Print out results
            for (int i = 0; i < numTrees; i++)
            {
                conflicts[i].print();
            }
        }
    }
}
