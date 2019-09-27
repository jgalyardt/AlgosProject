﻿using System;
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

            var distribution = from x in UniformDistribution() select SkewedQuantile(x);
            histogram = CreateHistogram(distribution.Take(100000), 50, 0.0, 1.0);
            histogramMax = histogram.Max();

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
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
                formGraphics.FillRectangle(brush, new Rectangle((280/histogram.Length)*i, 260 - bucketHeight, 280 / histogram.Length, bucketHeight));
            }
        }

        //+https://blogs.msdn.microsoft.com/ericlippert/2012/02/21/generating-random-non-uniform-data-in-c/
        private static IEnumerable<double> UniformDistribution()
        {
            while (true) yield return random.NextDouble();
        }

        private static double UniformQuantile(double p) //Range: 0.0 to 1.0
        {
            return p;
        }

        private static double SkewedQuantile(double p) //Range: 1.0 to 2.0 --> normalize
        {
            return Math.Abs(1.0 - (((1 + Math.Sqrt(1-p)) - 1.0) / (2.0 - 1.0)));
        }

        private static double TieredQuantile(double p) //Range: 0.0 to 1.0
        {
            //This is effectively a piecewise function
            if (p <= 0.4) return random.NextDouble() * (0.25);
            if (p <= 0.7) return random.NextDouble() * (0.50 - 0.25) + 0.25;
            if (p <= 0.9) return random.NextDouble() * (0.75 - 0.50) + 0.50;
            return random.NextDouble() * (1.0 - 0.75) + 0.75;

            //if (p <= 0.4) return (0.25) * (p / 0.25);
            //if (p <= 0.7) return (0.5 - 0.25) * ((p - 0.25) / (0.5 - 0.25)) + 0.25;
            //if (p <= 0.9) return (0.75 - 0.5) * ((p - 0.5) / (0.75 - 0.5)) + 0.5;
            //return (1.0 - 0.75) * ((p - 0.75) / (1.0 - 0.75)) + 0.75;
        }

        private static double CauchyQuantile(double p) //Range: -5.0 to 5.0 --> normalize
        {
            return ((Math.Tan(Math.PI * (p - 0.5))) - -5.0) / (5.0 - -5.0);
        }

        //This function was taken from above link about generating non-uniform data
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
