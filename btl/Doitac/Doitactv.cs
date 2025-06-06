﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace btl.Doitac
{
    public partial class Doitactv : Form
    {
        Doitac Doitac;
        public Doitactv(Doitac parent)
        {
            InitializeComponent();
            this.Doitac = parent;
        }

        public void SetData(string ma, string ten, DateTime bd, DateTime kt, string cp)
        {
            txtma.Text = ma;
            txtten.Text = ten;
            dateTimeBD.Value = bd;
            dateTimeKT.Value = kt;
            txtcp.Text = cp;
            txtma.Enabled = false;
            btntv.Text = "Cập nhật";
            header.Text = "Cập nhật đối tác";
        }

        private void Reload()
        {
            txtma.Text = "";
            txtten.Text = "";
            txtcp.Text = "";
            txtma.Enabled = true;
            btntv.Text = "Thêm";
            header.Text = "Thêm đối tác";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtma_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void btntv_Click(object sender, EventArgs e)
        {
            if (txtma.Text == "" || txtten.Text == "" || txtcp.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            string sql = "";
            string ma = txtma.Text;
            string ten = txtten.Text;
            DateTime bd = dateTimeBD.Value;
            DateTime kt = dateTimeKT.Value;
            string cp = txtcp.Text;
            if (btntv.Text == "Thêm")
            {
                if (!Thuvien.CheckExist("select count(*) from Doitac where Madoitac='" + ma + "' "))
                    {
                        sql = String.Format("insert into Doitac values('{0}', N'{1}', '{2}', '{3}', '{4}')", ma, ten, bd, kt, cp);
                    }
                    else
                    {
                        MessageBox.Show("Mã khuyến mại đã tồn tại");
                        return;
                    }
            }
            else
            {
                sql = String.Format("update DoiTac set Tenquangcao = N'{0}', Ngaybatdau = '{1}', Ngayketthuc = '{2}', Chiphi = '{3}' where Madoitac = '{4}'", ten, bd, kt, cp, ma);
            }
            Thuvien.ExecuteQuery(sql);
            Doitac.doitactb.loadtb();
            Doitac.SwitchToTab(0);
            MessageBox.Show("Thao tác Thành công!!", "Thông báo!");
            Reload();
        }

        private void txtcp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
