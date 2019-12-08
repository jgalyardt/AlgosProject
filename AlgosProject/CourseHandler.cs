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

        public void MethodOne(bool enforceUniqueCourses, bool outputToFile)
        {
            //Stopwatch info from +https://stackoverflow.com/questions/14019510/calculate-the-execution-time-of-a-method
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            //The first method utilizes AVL trees
            //Since the AVL Tree doesn't allow duplicates, all you need to do is insert every conflict
            //This also means that no more than O(M) space is needed to store the conflicts, at the cost of maintaining an AVL tree
            AVLTree conflicts = new AVLTree();
            int[] data = new int[coursesPerStudent];

            //Select courses for each student
            for (int i = 0; i < numStudents; i++)
            {
                distribution.GetCourses(ref data, coursesPerStudent, numCourses, enforceUniqueCourses);
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
            if (outputToFile)
                conflicts.toFile(numCourses);
            else
                conflicts.countNodes();

            int distinctConflicts = conflicts.nodeCount;
            int duplicates = conflicts.duplicateCount;
            //Print out results
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("[Method 1] Number of distinct conflicts: " + distinctConflicts + "\nNumber of duplicates: " + duplicates + "\nCompleted in " + elapsedMs + "ms");
        }

        public void MethodTwo(bool enforceUniqueCourses, bool outputToFile)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            //The second method again takes advantage of the 10,000 course limit, allocating a 100,009,999‬ element array
            //The number itself comes from the maximum possible course conflict between courses 10000 and 9999
            //This makes inserting a new conflict O(1), prevents duplicates, and reads out conflicts in O(n) time
            int[] conflicts = new int[100009999];

            //Initialize all entries to zero
            for (int i = 0; i < conflicts.Length; i++)
            {
                conflicts[i] = 0;
            }

            int[] data = new int[coursesPerStudent];

            //Store all conflicts in the array
            for (int i = 0; i < numStudents; i++)
            {
                distribution.GetCourses(ref data, coursesPerStudent, numCourses, enforceUniqueCourses);
                for (int j = 0; j < coursesPerStudent; j++)
                {
                    for (int k = j + 1; k < coursesPerStudent; k++)
                    {
                        int result = data[j] < data[k] ? (data[j] * 10001) + data[k] : (data[k] * 10001) + data[j];
                        //Add one at the index of the conflict
                        //This makes tracking duplicates easy, since any number above 1 at an index is a duplicate
                        conflicts[result]++;
                    }
                }
            }

            //Count and print results
            int distinctConflicts = 0;
            int duplicates = 0;

            if (outputToFile)
            {
                //Mostly same code as in toFile() in AVLTree.cs
                string[] P = new string[numCourses + 1];
                P[0] = numCourses.ToString();
                for (int i = 1; i < P.Length; i++)
                    P[i] = "0";

                string E = "0,";
                int prev = 0;
                int eIndex = 0;

                for (int i = 0; i < conflicts.Length; i++)
                {
                    if (conflicts[i] > 0)
                    {
                        int courseOne = i / 10001;
                        int courseTwo = i % 10001;

                        E += courseTwo.ToString() + ",";
                        eIndex++;
                        if (courseOne != prev)
                            P[courseOne] = eIndex.ToString();
                        prev = courseOne;

                        distinctConflicts++;
                        duplicates += conflicts[i] - 1;
                    }
                }

                //Write out the arrays to files
                System.IO.File.WriteAllLines("P.txt", P);
                //Remove the extra last comma
                E = E.Substring(0, E.Length - 1);
                System.IO.File.WriteAllLines("E.txt", E.Split(','));
            }
            else
            {
                for (int i = 0; i < conflicts.Length; i++)
                {
                    if (conflicts[i] > 0)
                    {
                        distinctConflicts++;
                        duplicates += conflicts[i] - 1;
                    }
                }
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("[Method 2] Number of distinct conflicts: " + distinctConflicts + "\nNumber of duplicates: " + duplicates + "\nCompleted in " + elapsedMs + "ms");
        }
    }
}
