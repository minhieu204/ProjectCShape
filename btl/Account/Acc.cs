using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Account
{
    public partial class Acc : Form
    {
        public Form1 f;
        public Acctb acctb;
        public Acctv acctv;
        public Acc(Form1 f)
        {
            InitializeComponent();
            acctb = new Acctb(this);
            acctv = new Acctv(this);
            EmbedFormInTab(acctb, tabPage1);
            EmbedFormInTab(acctv, tabPage2);
            HideTabHeaders();
            SwitchToTab(0);
            this.f = f;
        }
        private void HideTabHeaders()
        {
            tabControlMain.Appearance = TabAppearance.FlatButtons;
            tabControlMain.ItemSize = new Size(0, 1);
            tabControlMain.SizeMode = TabSizeMode.Fixed;
        }

        // Nhúng form con vào TabPage
        private void EmbedFormInTab(Form childForm, TabPage tabPage)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            tabPage.Controls.Add(childForm);
            childForm.Show();
        }

        // Chuyển đổi giữa các TabPage
        public void SwitchToTab(int tabIndex)
        {
            if (tabIndex >= 0 && tabIndex < tabControlMain.TabCount)
            {
                tabControlMain.SelectedIndex = tabIndex;
            }
        }
        public void suacc(string ma, string ht, string gt, string pq, string un, string pw, string sdt, string email, int i)
        {
            acctv.setData(ma, ht, gt, pq, un, pw, sdt, email, i);
            SwitchToTab(1);
        }
        private void Acc_Load(object sender, EventArgs e)
        {

        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            acctb.loadtb();
            SwitchToTab(0);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SwitchToTab(1);
        }
        public void switchns(){
            f.switchns();
        }
    }
}
