using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace btl.Nhansu
{
    public partial class Tacvu : Form
    {
        public Nhansu nhanSu;
        int month, year;
        float hlv=0;
        String ma;
        float lcb = 0;
        float thuong = 0;
        float luongtt1 = 0;
        float nhanduoc1 = 0;

        public Tacvu(Nhansu parent)
        {
            this.nhanSu = parent;
            InitializeComponent();
            luongtt.Text = "0";
            nhanduoc.Text = "0";
            lbluong.Text = "0";
            dateTimePicker1.Value = DateTime.Now;
            tinhtongh();

        }

        private void tinhtongh() {
             month = dateTimePicker1.Value.Month;
             year = dateTimePicker1.Value.Year;
            String sql = "select sum(giolamviec) as tonggio from lslamviec where manhanvien='"+ma+"' and MONTH(ngay)="+month+" and YEAR(ngay)="+year+"";
            DataTable dt = new DataTable();
            Thuvien.LoadExcel(sql, dt);
            if (dt.Rows.Count > 0 && dt.Rows[0]["tonggio"] != DBNull.Value)
            {
                lbluong.Text = dt.Rows[0]["tonggio"].ToString();
            }
            else
            {
                lbluong.Text = "0";
            }


        }
       
        public void SetData(String ht, String ma)
        {
            label3.Text = ht;
            this.ma = ma;
            luongtt.Text = "0";
            nhanduoc.Text = "0";
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Text = "";
            textBox2.Text = "";
            tinhtongh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            tinhtongh();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                lcb = 0;
            }
            else
            {
                lcb = float.Parse(textBox1.Text);
            }
            if (textBox2.Text == "")
            {
                thuong = 0;
            }
            else
            {
                thuong = float.Parse(textBox2.Text);
            }
            luongtt1 = lcb * float.Parse(lbluong.Text);
            nhanduoc1 = luongtt1 + thuong;
            luongtt.Text = luongtt1.ToString("N2")+" đ";
            nhanduoc.Text = nhanduoc1.ToString("N2")+" đ";

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string sql = $"INSERT INTO luong (manhanvien, tonggio, luongtheogio, thuong) " +
             $"VALUES ('{ma}', {float.Parse(lbluong.Text):0.00}, {lcb:0.00}, {thuong:0.00})";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Thêm bảng lương Thành công!!", "Thông báo!");
            nhanSu.luong.Loadtb();
            nhanSu.SwitchToTab(1);
        }
    }
}
