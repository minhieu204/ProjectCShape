﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Nhansu
{
    public partial class Nhansu : Form
    {
        public Form1 f;
        public Tacvu tacvu;
        public Nhanvien nhanvien;
        public Luong luong;
        public Nhansu(Form1 parent)
        {
            this.f = parent;
            tacvu = new Tacvu(this);
            nhanvien = new Nhanvien(this);
            luong = new Luong(this);
            InitializeComponent();
            HideTabHeaders();
            EmbedFormInTab(nhanvien, tabPage1);
            EmbedFormInTab(tacvu, tabPage3);
            EmbedFormInTab(luong, tabPage2);
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
        public void toacc()
        {
            f.switchtabacc();
        }
        public void suanv(string ma, string ht, string gt, string pq, string un, string pw, string sdt, string email)
        {
            f.suaacc(ma, ht, gt, pq, un, pw, sdt, email);
        }
        public void sw()
        {
            nhanvien.loadtb();        
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SwitchToTab(0);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SwitchToTab(1);
        }
    }
}
