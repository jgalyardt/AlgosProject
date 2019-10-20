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
            //The first method utilizes AVL trees
            //Since the AVL Tree doesn't allow duplicates, all you need to do is insert every conflict
            AVLTree conflicts = new AVLTree();
            int[] data;

            //Select courses for each student
            for (int i = 0; i < numStudents; i++)
            {
                data = distribution.GetCourses(coursesPerStudent, numCourses);
                for (int j = 0; j < coursesPerStudent; j++)
                {
                    for (int k = j + 1; k < coursesPerStudent; k++)
                    {
                        //Taking advantage of the max course number being 10000, we can store both sides of a conflict in a single int
                        //A conflict between course 4 and 10 is the same as a conflict between 10 and 4, so treat them as the same.
                        int result = data[j] < data[k] ? (data[j] * 10001) + data[k] : (data[k] * 10001) + data[j];
                        conflicts.insert(result);
                    }
                }
            }

            //Print out results
            Console.WriteLine("Number of distinct conflicts: " + conflicts.countNodes() + "\nNumber of duplicates: " + conflicts.duplicateCount);
        }

    }
}
