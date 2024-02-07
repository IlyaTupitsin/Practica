using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace практика_производственная
{
    public partial class selection : Form
    {
        public selection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Films f = new Films();
            f.ShowDialog();
       

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Serial f = new Serial();
            f.ShowDialog();
        }
    }
}
