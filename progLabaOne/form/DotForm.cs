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

        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();

            Clipboard.SetText(dataObj.GetText());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
        }
    }
}
