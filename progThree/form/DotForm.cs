using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nm
{
    public partial class DotForm : Form
    {
        public DotForm()
        {
            InitializeComponent();


            this.dataGridView1.Columns.Add("i", "i");
            this.dataGridView1.Columns.Add("Xi", "Xi");
            this.dataGridView1.Columns.Add("Hi", "Hi");
            this.dataGridView1.Columns.Add("Vi", "Vi");
            this.dataGridView1.Columns.Add("V^i", "V^i");
            this.dataGridView1.Columns.Add("V^i-Vi", "V^i-Vi");
        }



        public DotForm(int n, int m, double[] data)
        {
            InitializeComponent();

            for(int i = 0; i < m; ++i)
                this.dataGridView1.Columns.Add("i" + i, i+"");


            for (int i = 0; i < n; ++i)
            {
                dataGridView1.DataSource = data.Skip(i * m).Take(m).Select(u => u.ToString()).ToList();
;
            }

        }
    }
}
