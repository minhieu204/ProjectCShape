using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Chinhanh
{
    public partial class Chinhanhtv : Form
    {
        Chinhanh Chinhanh;
        public Chinhanhtv(Chinhanh parent)
        {
            InitializeComponent();
            this.Chinhanh = parent;
        }

        public void SetData(string ma, string ten, string dt, string em, string dc)
        {
            txtma.Text = ma;
            txtten.Text = ten;
            txtsdt.Text = dt;
            txtemail.Text = em;
            txtdc.Text = dc;
            txtma.Enabled = false;
            btntv.Text = "Cập nhật";
            header.Text = "Cập nhật chi nhánh";
        }

        private void Reload()
        {
            txtma.Text = "";
            txtten.Text = "";
            txtsdt.Text = "";
            txtemail.Text = "";
            txtdc.Text = "";
            txtma.Enabled = true;
            btntv.Text = "Thêm";
            header.Text = "Thêm chi nhánh";
        }

        private void btnload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void btntv_Click(object sender, EventArgs e)
        {
            if (txtma.Text == "" || txtten.Text == "" || txtsdt.Text == "" || txtemail.Text == "" || txtdc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            string sql = "";
            string ma = txtma.Text;
            string ten = txtten.Text;
            string dt = txtsdt.Text;
            string em = txtemail.Text;
            string dc = txtdc.Text;

            if (btntv.Text == "Thêm")
            {
                if (!Thuvien.CheckExist("select count(*) from ChiNhanh where Machinhanh='" + ma + "' "))
                {
                    sql = String.Format("insert into Doitac values('{0}', N'{1}', '{2}', '{3}', '{4}')", ma, ten, dt, em, dc);
                }
                else
                {
                    MessageBox.Show("Chi nhánh đã tồn tại");
                    return;
                }
            }
            else
            {
                sql = String.Format("update ChiNhanh set Tenchinhanh = N'{0}', SDT = '{1}', Email = '{2}', Diachi = '{3}' where Machinhanh = '{4}'", ten, dt, em, dc, ma);
            }
            Thuvien.ExecuteQuery(sql);
            Chinhanh.chinhanhtb.loadtb();
            Chinhanh.SwitchToTab(0);
            MessageBox.Show("Thao tác Thành công!!", "Thông báo!");
            Reload();
        }
    }
}
