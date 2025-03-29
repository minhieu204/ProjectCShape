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
    public partial class FormSP : Form
    {
        public FormSP()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void DrawBorder(TextBox textBox, Color color)
        {
            using (Graphics g = textBox.CreateGraphics())
            {
                int borderWidth = 2;
                ControlPaint.DrawBorder(g, textBox.ClientRectangle, color, borderWidth, ButtonBorderStyle.Solid,
                                                                      color, borderWidth, ButtonBorderStyle.Solid,
                                                                      color, borderWidth, ButtonBorderStyle.Solid,
                                                                      color, borderWidth, ButtonBorderStyle.Solid);
            }
        }

        private void FormSP_Load(object sender, EventArgs e)
        {
        }

        private void FormSP_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }
    }
}
