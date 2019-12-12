using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AlgosProject
{
    public partial class Form1 : Form
    {
        private System.Drawing.SolidBrush brush;
        private System.Drawing.Graphics formGraphics;
        private int[] histogram;
        private int histogramMax;
        private Distribution graphDistribution;

        private bool SHOW_GRAPH = false;
        public Form1()
        {
            PartOne(Directory.GetCurrentDirectory() + "\\config.txt");
            PartTwo(Directory.GetCurrentDirectory() + "\\P.txt", Directory.GetCurrentDirectory() + "\\E.txt");

            if (SHOW_GRAPH)
            {
                brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                formGraphics = CreateGraphics();
                graphDistribution = new Distribution("cauchy");
                histogram = CreateHistogram(graphDistribution.Take(1000000), 50, 0.0, 1.0);
                histogramMax = histogram.Max();

                InitializeComponent();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (SHOW_GRAPH)
            {
                base.OnPaint(e);

                for (int i = 0; i < histogram.Length; i++)
                {
                    int bucketHeight;
                    try
                    {
                        bucketHeight = histogram[i] / (histogramMax / 260);
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("Error: Divide by zero.");
                        break;
                    }
                    formGraphics.FillRectangle(brush, new Rectangle((280 / histogram.Length) * i, 260 - bucketHeight, 280 / histogram.Length, bucketHeight));
                }
            }
        }

        //+https://blogs.msdn.microsoft.com/ericlippert/2012/02/21/generating-random-non-uniform-data-in-c/
        //This function was taken from above link about generating non-uniform data
        private static int[] CreateHistogram(double[] data, int buckets, double min, double max)
        {
            int[] results = new int[buckets];
            double multiplier = buckets / (max - min);
            foreach (double datum in data)
            {
                double index = (datum - min) * multiplier;
                if (0.0 <= index && index < buckets)
                    results[(int)index] += 1;
            }
            return results;
        }

        //Read input from a config file
        private static void PartOne(String pathToConfig)
        {
            int numCourses = -1;
            int numStudents = -1;
            int coursesPerStudent = -1;
            String distributionType = "DEFAULT";
            String configString = "DEFAULT";
            Distribution distribution;

            StreamReader sr = new StreamReader(pathToConfig);
            String line = sr.ReadLine();
            while (line != null)
            {
                try
                {
                    configString = line.Substring(0, line.IndexOf('='));
                    switch (configString)
                    {
                        case ("C"):
                            int.TryParse(line.Substring(configString.Length + 1), out numCourses);
                            break;
                        case ("S"):
                            int.TryParse(line.Substring(configString.Length + 1), out numStudents);
                            break;
                        case ("K"):
                            int.TryParse(line.Substring(configString.Length + 1), out coursesPerStudent);
                            break;
                        case ("DIST"):
                            distributionType = line.Substring(configString.Length + 1);
                            break;
                        default:
                            Console.WriteLine($"Unexpected line: '{line}'");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{configString}'");
                }
                
                line = sr.ReadLine();
            }
            Console.WriteLine(numCourses + ", " + numStudents + ", " + coursesPerStudent + ", " + distributionType);

            distribution = new Distribution(distributionType);
            CourseHandler courseHandler = new CourseHandler(numCourses, numStudents, coursesPerStudent, distribution);
            //The boolean arguments here are enforceUniqueCourses and outputToFile respectively
            //courseHandler.MethodOne(true, true);
            courseHandler.MethodTwo(true, true);

        }

        private static void PartTwo(String pPath, String ePath)
        {
            bool TIMING_MODE = false;
            bool VERBOSE = false;
            
            GraphHandler graphHandler = new GraphHandler(pPath, ePath, VERBOSE, TIMING_MODE);
            //graphHandler.SmallestLast();
            //graphHandler.WelshPowell();
            //graphHandler.RandomOrdering();
            graphHandler.BogoColoring();
            
        }
    }
}
