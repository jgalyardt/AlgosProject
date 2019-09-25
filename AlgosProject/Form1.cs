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
        private System.Drawing.SolidBrush brush;
        private System.Drawing.Font font;
        private System.Drawing.Graphics formGraphics;
        private int[] histogram;
        private int histogramMax;
        private static Random random;

        public Form1()
        {
            random = new Random();
            brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            font = new System.Drawing.Font("Arial", 12);
            formGraphics = CreateGraphics();

            var cauchy = from x in UniformDistribution() select CauchyQuantile(x);
            histogram = CreateHistogram(cauchy.Take(100000), 50, -5.0, 5.0);
            histogramMax = histogram.Max();

            InitializeComponent();   
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int i = 0; i < histogram.Length; i++)
            {
                int bucketHeight = histogram[i] / (histogramMax / 260);
                formGraphics.FillRectangle(brush, new Rectangle((280/histogram.Length)*i, 260 - bucketHeight, 280 / histogram.Length, bucketHeight));
            }
        }

        //+https://blogs.msdn.microsoft.com/ericlippert/2012/02/21/generating-random-non-uniform-data-in-c/
        private static IEnumerable<double> UniformDistribution()
        {
            while (true) yield return random.NextDouble();
        }

        private static double UniformQuantile(double p)
        {
            return p;
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
