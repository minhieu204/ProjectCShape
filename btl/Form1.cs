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
    public partial class Form1 : Form
    {
        Dashboard dashboard;
        FormSP formSP;
        public Form1()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            mdiProp();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        bool sidebarExpand = true;
        private void mdiProp()
        {
            this.SetBevel(false);
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(232, 234, 237);
        }
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width <= 60)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                    panelDB.Width = sidebar.Width;
                    panelSP.Width = sidebar.Width;
                    panelLO.Width = sidebar.Width;
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 260)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                    panelDB.Width = sidebar.Width;
                    panelSP.Width = sidebar.Width;
                    panelLO.Width = sidebar.Width;
                }
            }
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dashboard == null)
            {
                dashboard = new Dashboard();
                dashboard.FormClosed += Dashboard_FormClosed;
                dashboard.MdiParent = this;
                dashboard.Dock = DockStyle.Fill;
                dashboard.Show();
            }
            else
            {
                dashboard.Activate();
            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            dashboard = null;
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
            if (formSP == null)
            {
                formSP = new FormSP();
                formSP.FormClosed += Sanpham_FormClosed;
                formSP.MdiParent = this;
                formSP.Dock = DockStyle.Fill;
                formSP.Show();
            }
            else
            {
                formSP.Activate();
            }
        }

        private void Sanpham_FormClosed(object sender, FormClosedEventArgs e)
        {
            formSP = null;
        }
    }
}
