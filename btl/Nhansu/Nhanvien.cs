using System;
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
    public partial class Nhanvien : Form
    {
        public Nhansu nhansu;
        public Nhanvien(Nhansu parent)
        {
            this.nhansu = parent;
            InitializeComponent();
            loadtb();
            Thuvien.CustomDataGridView(Datanv);
        }
        private void loadtb()
        {
            String sql = "select manhanvien,hoten,gioitinh,sdt,email from nhanvien";
            Thuvien.LoadData(sql, Datanv);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
