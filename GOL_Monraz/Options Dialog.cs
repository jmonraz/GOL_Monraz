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
    public partial class Options_Dialog : Form
    {
        public Options_Dialog()
        {
            InitializeComponent();
            
        }

        public int Miliseconds
        {
            get { return (int)numericUpDownMiliseconds.Value; }
            set { numericUpDownMiliseconds.Value = value; }
        }

        public int CellWidht
        {
            get { return (int)numericUpDownWidthCells.Value; }
            set { numericUpDownWidthCells.Value = value; }
        }

        public int CellHeight
        {
            get { return (int)numericUpDownHeightCells.Value; }
            set { numericUpDownHeightCells.Value = value; }
        }

        
    }

    
}
