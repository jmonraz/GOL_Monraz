using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL_Monraz
{
    public partial class SeedDialog : Form
    {
        public SeedDialog()
        {
            InitializeComponent();
        }

        public int seedNumber
        {
            get { return (int)numericUpDownSeed.Value; }
            set { numericUpDownSeed.Value = value; }
        }

        private void randomLabel_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            
            numericUpDownSeed.Value = rand.Next(int.MinValue, int.MaxValue);
        }
    }
}
