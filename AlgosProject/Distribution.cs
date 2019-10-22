using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgosProject
{
    public class Distribution
    {
        private static Random random;
        private static Func<double, double> distFunction;
        public Distribution(string type)
        {
            random = new Random();

            switch (type)
            {
                case ("uniform"):
                    distFunction = UniformQuantile;
                    break;
                case ("skewed"):
                    distFunction = SkewedQuantile;
                    break;
                case ("tiered"):
                    distFunction = TieredQuantile;
                    break;
                case ("cauchy"):
                    distFunction = CauchyQuantile;
                    break;
                default:
                    Console.WriteLine($"Unexpected distribution: '{type}'");
                    break;
            }
        }

        public double Benchmark(int coursesPerStudent, int numStudents, int numCourses, bool unique)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < numStudents; i++)
            {
                int[] data = new int[coursesPerStudent];
                if (unique)
                {
                    for (int j = 0; j < coursesPerStudent; j++)
                    {
                        //If each course selection needs to be unique, re-roll any duplicate choice
                        int result = (int)Math.Round((numCourses - 1) * distFunction(random.NextDouble()) + 1);
                        while (data.Contains(result))
                        {
                            result = (int)Math.Round((numCourses - 1) * distFunction(random.NextDouble()) + 1);
                        }
                        data[j] = result;
                    }
                }
                else
                {
                    for (int j = 0; j < coursesPerStudent; j++)
                    {
                        //Normalizes the result between 1 and numCourses (effectively selecting a course)
                        data[j] = (int)Math.Round((numCourses - 1) * distFunction(random.NextDouble()) + 1);
                    }
                }
            }
            return watch.ElapsedMilliseconds;
        }

        public double[] Take(int amount)
        {
            double[] data = new double[amount];
            for (int i = 0; i < amount; i++)
            {
                data[i] = distFunction(random.NextDouble());
            }
            return data;
        }

        public void GetCourses(ref int[] data, int amount, int numCourses, bool unique)
        {
            if (unique)
            {
                for (int i = 0; i < amount; i++)
                {
                    //If each course selection needs to be unique, re-roll any duplicate choice
                    int result = (int)Math.Round((numCourses - 1) * distFunction(random.NextDouble()) + 1);
                    while (data.Take(i).Contains(result))
                    {
                        result = (int)Math.Round((numCourses - 1) * distFunction(random.NextDouble()) + 1);
                    }
                    data[i] = result;
                }
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    //Normalizes the result between 1 and numCourses (effectively selecting a course)
                    data[i] = (int)Math.Round((numCourses - 1) * distFunction(random.NextDouble()) + 1);
                }
            }
        }

        //+https://blogs.msdn.microsoft.com/ericlippert/2012/02/21/generating-random-non-uniform-data-in-c/
        private static double UniformQuantile(double p) //Range: 0.0 to 1.0
        {
            return p;
        }

        private static double SkewedQuantile(double p) //Range: 1.0 to 2.0 --> normalize
        {
            return Math.Abs(1.0 - (((1 + Math.Sqrt(1 - p)) - 1.0) / (2.0 - 1.0)));
        }

        private static double TieredQuantile(double p) //Range: 0.0 to 1.0
        {
            //This is effectively a piecewise function
            if (p <= 0.4) return random.NextDouble() * (0.25);
            if (p <= 0.7) return random.NextDouble() * (0.50 - 0.25) + 0.25;
            if (p <= 0.9) return random.NextDouble() * (0.75 - 0.50) + 0.50;
            return random.NextDouble() * (1.0 - 0.75) + 0.75;
        }

        private static double CauchyQuantile(double p) //Range: -5.0 to 5.0 --> normalize
        {
            return ((Math.Tan(Math.PI * (p - 0.5))) - -5.0) / (5.0 - -5.0);
        }
    }
}
