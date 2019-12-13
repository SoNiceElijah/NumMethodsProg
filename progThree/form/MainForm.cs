using core;
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
        DotForm info;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox3.Text);
            int m = Convert.ToInt32(textBox4.Text);

            double eps = Convert.ToDouble(textBox2.Text.Replace('.', ','));
            double max = Convert.ToInt32(textBox1.Text);


            List<Method.Func> param1 = new List<Method.Func>();

            param1.Add((x, y) => 1 - x * x - y * y);
            param1.Add((x, y) => 1 - x * x - y * y);
            param1.Add((x, y) => 1 - x * x - y * y);
            param1.Add((x, y) => 1 - x * x - y * y);


            double[] param2 = { -1, 1, -1, 1 };

            Method meth = new Method(
                (x,y) => -4,
                param1.ToArray(),
                param2,
                n,m
            );


            var result = meth.Calculate(eps, max, out double num, out double diff);

            Console.WriteLine(num + " " + diff);

            info = new DotForm(n - 1, m - 1, result);

            info.Show();

        }
    }
  
}
