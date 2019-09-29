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

        Dot[] dots {  set; get; }


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

            mainChart.ChartAreas[0].AxisY.Minimum = 0;
            mainChart.ChartAreas[0].AxisY.Maximum = 10;

            mainChart.Series.Clear();
            // Добавляем новый график
            var ser = mainChart.Series.Add("Diff");
            ser.ChartType = SeriesChartType.Line;

            chart1.Series.Clear();
            var sh = chart1.Series.Add("h");
            sh.ChartType = SeriesChartType.Line;

            ser.Points.AddXY(0, 0);
            ser.Points.AddXY(10, 10);

            this.dataGridView1.Columns.Add("i", "i");
            this.dataGridView1.Columns.Add("Xi", "Xi");
            this.dataGridView1.Columns.Add("Hi", "Hi");
            this.dataGridView1.Columns.Add("Vi", "Vi");
            this.dataGridView1.Columns.Add("V^i", "V^i");
            this.dataGridView1.Columns.Add("V^i-Vi", "V^i-Vi");
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            double l = Convert.ToDouble(lTextBox.Text.Replace('.',','));
            double r = Convert.ToDouble(rTextBox.Text.Replace('.', ','));
            double v = Convert.ToDouble(vTextBox.Text.Replace('.', ','));
            double i0 = Convert.ToDouble(iTextBox.Text.Replace('.', ','));
            double h = Convert.ToDouble(hTextBox.Text.Replace('.', ','));
            int n = Convert.ToInt32(nTextBox.Text.Replace('.', ','));

            Method m = new Method((x,y) =>  (- r*y/l) + (v/l), 0, i0, h);

            mainChart.Series["Diff"].Points.Clear();
            chart1.Series["h"].Points.Clear();
            this.dataGridView1.Rows.Clear();

            foreach (var i in Enumerable.Range(0,n))
            {
                double step = m.Step;
                Dot p = m.nextStep(out double contr);
                mainChart.Series["Diff"].Points.AddXY(p.X,p.Y);
                chart1.Series["h"].Points.AddXY(i,step);

                this.dataGridView1.Rows.Add(i + "",p.X, step, p.Y, contr, p.Y - contr);
            }
        }
    }
}
