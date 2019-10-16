using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{
    class Course
    {
        public int id;
        public int[] conflicts;
        private int index;

        public Course(int coursesPerStudent)
        {
            conflicts = new int[coursesPerStudent - 1];
            index = 0;
            id = -1;
        }

        public void Add(int courseID)
        {
            conflicts[index] = courseID;
            index++;
        }

        public bool HasConflict(int courseID)
        {
            return conflicts.Contains(courseID);
        }
    }
}
