using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant
{
    public partial class FrmSplash : Form
    {
        public FrmSplash()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbSplash.Increment(2);
            if (pbSplash.Value==100)
            {
                this.Hide();
                FrmGiris fg = new FrmGiris();
                fg.Show();
                timer1.Stop();
            }
        }

        private void FrmSplash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
