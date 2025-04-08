using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using btl.Doitac;

namespace btl.Khuyenmai
{
    public partial class KhuyenMai : Form
    {
        public KhuyenMaiQL khuyenMaiQL;
        public KhuyenMaiTV khuyenMaiTV;
        public KhuyenMai()
        {
            InitializeComponent();
            khuyenMaiTV = new KhuyenMaiTV(this);
            khuyenMaiQL = new KhuyenMaiQL(this);
            EmbedFormInTab(khuyenMaiQL, tabPage1);
            EmbedFormInTab(khuyenMaiTV, tabPage2);
            HideTabHeaders();
            SwitchToTab(0);
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            khuyenMaiQL.loadtb();
            SwitchToTab(0);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SwitchToTab(1);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
