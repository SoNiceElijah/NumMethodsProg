﻿using core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace nm
{
    public partial class MainForm : Form
    {
        int nums = 0;
        Dot[] dots {  set; get; }
        DotForm info;

        public MainForm()
        {
            InitializeComponent();
            var x = Enumerable.Range(0, 1001).Select(i => i / 10.0).ToArray();
            dots = x.Select(v => new Dot() { Y = Math.Abs(v) < 1e-10 ? 1 : Math.Sin(v) / v, X = v }).ToArray();

            this.Shown += MainForm_Shown;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            mainChart.ChartAreas[0].AxisX.Minimum = 0;
            mainChart.ChartAreas[0].AxisX.Maximum = 10;

            mainChart.ChartAreas[0].AxisX.Title = "t (время)";

            mainChart.ChartAreas[0].AxisY.Minimum = 0;
            mainChart.ChartAreas[0].AxisY.Maximum = 10;

            mainChart.ChartAreas[0].AxisY.Title = "I (ток)";

            mainChart.Legends.Clear();
            mainChart.Series.Clear();
            // Добавляем новый график
            var ser = mainChart.Series.Add("Diff");
            ser.ChartType = SeriesChartType.Line;
            ser.Color = Color.FromArgb(255, Color.Red);
            ser.BorderWidth = 1;

            var real = mainChart.Series.Add("Real");
            real.ChartType = SeriesChartType.Line;
            real.Color = Color.FromArgb(20,Color.Green);
            real.BorderWidth = 3;

            chart1.Series.Clear();
            chart1.Legends.Clear();

            var sh = chart1.Series.Add("h");
            sh.ChartType = SeriesChartType.Line;

            ser.Points.AddXY(0, 0);
            ser.Points.AddXY(10, 10);

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = "Шаги";

        }

        private void runButton_Click(object sender, EventArgs e)
        {
            double l = Convert.ToDouble(lTextBox.Text.Replace('.',','));
            double r = Convert.ToDouble(rTextBox.Text.Replace('.', ','));
            double v = Convert.ToDouble(vTextBox.Text.Replace('.', ','));
            double i0 = Convert.ToDouble(iTextBox.Text.Replace('.', ','));
            double h = Convert.ToDouble(hTextBox.Text.Replace('.', ','));
            double eps = Convert.ToDouble(epsTextBox.Text.Replace('.', ','));
            int n = Convert.ToInt32(nTextBox.Text.Replace('.', ','));

            Method m = new Method((x,y) =>  (- r*y/l) + (v/l), 0, i0, h, eps);


            mainChart.Series["Diff"].Points.Clear();
            mainChart.Series["Real"].Points.Clear();
            chart1.Series["h"].Points.Clear();

            ++nums;
            info = new DotForm();
            info.label1.Text = "Запуск номер " + nums;
            info.param.Text = $"L: {l}, R: {r}, V: {v}, I(0): {i0}, H: {h}, N: {n}";

            foreach (var i in Enumerable.Range(0,n))
            {
                double step = m.Step;
                Dot p = m.nextStep(out double contr);
                if (Math.Abs(p.Y) < 1e-8)
                    p.Y = 0;
                if (Math.Abs(p.Y) > 10e+20)
                    break;
                if (Math.Abs(p.X) < 1e-8)
                    p.X = 0;
                if (Math.Abs(p.X) > 10e+20)
                    break;
                mainChart.Series["Diff"].Points.AddXY(p.X,p.Y);
                chart1.Series["h"].Points.AddXY(i,step);

                Console.WriteLine(p.X + " " +p.Y);

                info.dataGridView1.Rows.Add(i + "",p.X, step, p.Y, contr, p.Y - contr);
                info.Show();
            }

           // foreach (var x in Enumerable.Range(0, 1000).Select(u => (double)u / 100))
           // {
           //     var y = (v / r) * (1 - Math.Exp((-r / l) * x)) + i0 * Math.Exp((-r / l) * x);
           //     mainChart.Series["Real"].Points.AddXY(x, y);
           // }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double l = Convert.ToDouble(lTextBox.Text.Replace('.', ','));
            double r = Convert.ToDouble(rTextBox.Text.Replace('.', ','));
            double v = Convert.ToDouble(vTextBox.Text.Replace('.', ','));
            double i0 = Convert.ToDouble(iTextBox.Text.Replace('.', ','));
            double h = Convert.ToDouble(hTextBox.Text.Replace('.', ','));
            int n = Convert.ToInt32(nTextBox.Text.Replace('.', ','));

            mainChart.Series["Real"].Points.Clear();

            foreach (var x in Enumerable.Range(0, 1000).Select(u => (double)u / 100))
            {
               var y = (v / r) * (1 - Math.Exp((-r / l) * x)) + i0 * Math.Exp((-r / l) * x);
               mainChart.Series["Real"].Points.AddXY(x, y);
            }
        }
    }
  
}
