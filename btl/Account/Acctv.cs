using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Account
{
    public partial class Acctv : Form
    {
        Acc Acc;
        public Acctv(Acc parent)
        {
            InitializeComponent();
            this.Acc = parent;
            comboBox1.SelectedItem = "---Chọn---";
            Thuvien.LoadComboBox("select * from phanquyen", cbphanquyen, "maphanquyen", "tenphanquyen");
        }
        int i;
        public void setData(string ma, string ht, string gt, string pq, string un, string pw, string sdt, string email , int i)
        {
            txtma.Text = ma;
            txthoten.Text = ht;
            comboBox1.SelectedItem = gt;
            cbphanquyen.SelectedValue = pq;
            txtuser.Text = un;
            txtpass.Text = pw;
            txtsdt.Text = sdt;
            txtemail.Text = email;
            txtma.Enabled = false;
            btntv.Text = "Cập nhật";
            header.Text = "Cập nhật tài khoản";
            cbphanquyen.Enabled = false;
            this.i = i;
        }
        private void Reload()
        {
            txtma.Text = "";
            txthoten.Text = "";
            comboBox1.SelectedItem = "---Chọn---";
            cbphanquyen.SelectedIndex = 0;
            txtuser.Text = "";
            txtma.Enabled = true;
            txtpass.Text = "";
            txtsdt.Text = "";
            txtemail.Text = "";
            btntv.Text = "Thêm";
            txtuser.Text = "";
            header.Text = "Thêm tài khoản";
            cbphanquyen.Enabled = true;
        }

        private void Acctv_Load(object sender, EventArgs e)
        {

        }

        private void btnload_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void btntv_Click(object sender, EventArgs e)
        {
            if (txtma.Text == "" || txthoten.Text == "" || comboBox1.SelectedItem.ToString() == "---Chọn---" || cbphanquyen.SelectedIndex == 0 || txtuser.Text == "" || txtpass.Text == "" || txtsdt.Text == "" || txtemail.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }
            String sql = "";
            String ma = txtma.Text;
            String ht = txthoten.Text;
            String gt = comboBox1.SelectedItem.ToString();
            String pq = cbphanquyen.SelectedValue.ToString();
            String un = txtuser.Text;
            String pw = txtpass.Text;
            String sdt = txtsdt.Text;
            String email = txtemail.Text;
            if (btntv.Text == "Thêm")
            {
                if (!Thuvien.CheckExist("SELECT COUNT(*) FROM (SELECT username FROM quanly WHERE username = '" + un + "' UNION ALL SELECT username FROM nhanvien WHERE username = '" + un + "') as temp"))
                {
                    if (pq == "ql")
                    {
                        if (!Thuvien.CheckExist("select count(*) from quanly where maquanly='" + ma + "' "))
                        {
                            sql = String.Format("insert into quanly values('{0}', '{1}', '{2}', '{3}', N'{4}', N'{5}', '{6}', '{7}')", ma, un, pw, pq, ht, gt, sdt, email);
                        }
                        else
                        {
                            MessageBox.Show("Mã tài khoản đã tồn tại");
                            return;
                        }
                    }
                    else
                    {
                        if (!Thuvien.CheckExist("select count(*) from nhanvien where manhanvien='" + ma + "' "))
                        {
                            sql = String.Format("insert into nhanvien values('{0}', '{1}', '{2}', '{3}', N'{4}', N'{5}', '{6}', '{7}')", ma, un, pw, pq, ht, gt, sdt, email);
                        }
                        else
                        {
                            MessageBox.Show("Mã tài khoản đã tồn tại");
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại");
                    return;
                }
            }
            else
            {
                if (pq == "ql")
                {
                    sql = String.Format("update quanly set hoten = N'{0}', gioitinh = N'{1}', maphanquyen = '{2}', username = '{3}', pass = '{4}', sdt = '{5}', email = '{6}' where maquanly = '{7}'", ht, gt, pq, un, pw, sdt, email, ma);
                }
                else
                {
                    sql = String.Format("update nhanvien set hoten = N'{0}', gioitinh = N'{1}', maphanquyen = '{2}', username = '{3}', pass = '{4}', sdt = '{5}', email = '{6}' where manhanvien = '{7}'", ht, gt, pq, un, pw, sdt, email, ma);
                }
            }
            Thuvien.ExecuteQuery(sql);
            if (i != 1) 
            {
            Acc.acctb.loadtb();
            Acc.SwitchToTab(0);
            
            }
            else { Acc.switchns(); }
           MessageBox.Show("Thao tác Thành công!!", "Thông báo!");
            Reload(); 
        }

        private void txtsdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtsdt_Leave(object sender, EventArgs e)
        {
            string phoneNumber = txtsdt.Text.Trim();

            // Biểu thức chính quy cho số điện thoại Việt Nam
            string pattern = @"^0[3|5|7|8|9][0-9]{8}$";

            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập lại.");
                txtsdt.Text = "";
            }
        }

        private void txtemail_Leave(object sender, EventArgs e)
        {
            string email = txtemail.Text.Trim();

            // Biểu thức chính quy kiểm tra email hợp lệ
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(email, pattern))
            {
                MessageBox.Show("Email không hợp lệ. Vui lòng nhập lại.");
                txtemail.Text = "";
            }
        }

        private void header_Click(object sender, EventArgs e)
        {

        }
    }
}
