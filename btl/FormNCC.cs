using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl
{
    public partial class FormNCC : Form
    {
        public FormNCC()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

        }

        private void FormNCC_Load(object sender, EventArgs e)
        {

        }

        private void FormNCC_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }
    }
}
