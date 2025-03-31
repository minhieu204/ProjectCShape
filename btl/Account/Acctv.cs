using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.Account
{
    public partial class Acctv : Form
    {
        public Acctv()
        {
            InitializeComponent();
            comboBox1.SelectedItem = "---Chọn---";
            Thuvien.LoadComboBox("select * from phanquyen", cbphanquyen , "maphanquyen", "tenphanquyen");
        }
        public void setData(string ma, string ht, string gt, string pq, string un, string pw, string sdt, string email)
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
        }

        private void Acctv_Load(object sender, EventArgs e)
        {

        }

        private void btnload_Click(object sender, EventArgs e)
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
        }
    }
}
