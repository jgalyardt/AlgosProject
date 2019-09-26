using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgosProject
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            var cauchy = from x in UniformDistribution() select CauchyQuantile(x);
            int[] histogram = CreateHistogram(cauchy.Take(100000), 50, -5.0, 5.0);
        }


        private static Random random = new Random();
        private static IEnumerable<double> UniformDistribution()
        {
            while (true) yield return random.NextDouble();
        }

        private static double CauchyQuantile(double p)
        {
            return Math.Tan(Math.PI * (p - 0.5));
        }

        private static int[] CreateHistogram(IEnumerable<double> data, int buckets, double min, double max)
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

    }
}
