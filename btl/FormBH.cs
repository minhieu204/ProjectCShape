using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl
{
    public partial class FormBH : Form
    {

        public FormBH()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Thuvien.CustomDataGridView(dgvSP);
            
        }

        private void loadSP()
        {
            String sql = "select masp, tensp, donvitinh, giaban, soluong " +
                         "from sanpham ";
            Thuvien.LoadData(sql, dgvSP);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            loadSP();
            txtMasp.Enabled = false;
            txtTensp.Enabled = false;
            txtDVT.Enabled = false;
            txtGiaban.Enabled = false;
            txtSLco.Enabled = false;
            txtSLban.Enabled = false;
            txtMahd.Enabled = false;
            txtNgaynhap.Enabled = false;
            txtNguoiban.Enabled = false;
        }

        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSLban.Enabled = true;
            int i = e.RowIndex;
            txtMasp.Text = dgvSP.Rows[i].Cells[0].Value.ToString();
            txtTensp.Text = dgvSP.Rows[i].Cells[1].Value.ToString();
            txtDVT.Text = dgvSP.Rows[i].Cells[2].Value.ToString();
            txtGiaban.Text = dgvSP.Rows[i].Cells[3].Value.ToString();
            txtSLco.Text = dgvSP.Rows[i].Cells[4].Value.ToString();   
        }

        private void txtSLban_TextChanged(object sender, EventArgs e)
        {

            if (txtSLban.Text.Trim() == "")
            {
                txtThanhtien.Text = "....................................................";
                return;
            }
            int slban = int.Parse(txtSLban.Text.Trim());
            int thanhtien = int.Parse(txtGiaban.Text.Trim()) * slban;
            txtThanhtien.Text = thanhtien.ToString();
        }

        private void txtSLban_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
