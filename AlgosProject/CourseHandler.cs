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
        private IEnumerable<double> distribution;

        public CourseHandler(int C, int S, int K, IEnumerable<double> DIST)
        {
            numCourses = C;
            numStudents = S;
            coursesPerStudent = K;
            distribution = DIST;
        }

        public void MethodOne()
        {
            //Select courses for each student
            for (int i = 0; i < numStudents; i++)
            {
                Course course = new Course(coursesPerStudent);
                //Normalize results from 0 to 1 to course numbers (1 to C)
                foreach (double datum in distribution.Take(coursesPerStudent))
                {
                    double normalized = (numCourses - 1) * datum + 1;
                    int courseResult = (int)Math.Round(normalized);

                    

                }
            }
        }
    }
}
