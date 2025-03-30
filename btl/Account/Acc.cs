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
        Acctb acctb;
        Acctv acctv;
        public Acc()
        {
            InitializeComponent();
            ResizeTab();
            acctb = new Acctb();
            acctv = new Acctv();
            acctb.Dock = DockStyle.Fill;
            acctv.Dock = DockStyle.Fill;
            acctb.TopLevel = false;
            acctv.TopLevel = false;
            tabPage1.Controls.Add(acctb);
            tabPage2.Controls.Add(acctv);
            acctb.Show();
            acctv.Show();
            tabControlMain.DrawItem += new DrawItemEventHandler(TabControlMain_DrawItem);
        }
        private void ResizeTab()
        {
            if (tabControlMain.TabCount > 0)
            {
                int tabWidth = tabControlMain.Width / 2; // Chia đều chiều rộng
                tabControlMain.ItemSize = new Size(tabWidth, tabControlMain.ItemSize.Height);
            }
        }
        private void TabControlMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabControl tabControl = (TabControl)sender;
            TabPage tabPage = tabControl.TabPages[e.Index];

            Rectangle tabRect = tabControl.GetTabRect(e.Index);
            Font font = new Font("Segoe UI", 12, FontStyle.Bold);

            // Kiểm tra tab có đang được chọn không
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Màu nền của tab
            Color backColor = isSelected ? Color.FromArgb(234, 132, 50) : Color.LightGray;
            Color textColor = isSelected ? Color.White : Color.Black;

            // Vẽ nền
            using (SolidBrush brush = new SolidBrush(backColor))
            {
                g.FillRectangle(brush, tabRect);
            }

            // Vẽ chữ
            TextRenderer.DrawText(g, tabPage.Text, font, tabRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        private void Acc_Load(object sender, EventArgs e)
        {
           
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
