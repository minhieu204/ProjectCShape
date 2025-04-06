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
    public partial class FormDH : Form
    {
        public FormDH()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Thuvien.CustomDataGridView(dgvCTDH);
            Thuvien.CustomDataGridView(dgvDH);
        }

        private void resetText()
        {
            txtMadh.Text = "";
            txtNgaynhap.Text = "";
            txtTongtien.Text = "";
            txtNguoiban.Text = "";
        }

        private void loadCTDH()
        {
            String sql = "select madon, tensp, chitietdonhang.soluong, chitietdonhang.giaban, thanhtien " +
                             "from chitietdonhang, sanpham " +
                             "where chitietdonhang.masp = sanpham.masp and madon='" + txtMadh.Text.Trim() + "'";
            Thuvien.LoadData(sql, dgvCTDH);
        }

        private void loadDH()
        {
            String sql = "select madon, ngayban, tongtien, quanly.hoten " +
                         "from donhang, quanly " +
                         "where donhang.manhanvien = quanly.maquanly " +
                         "union all " +
                         "select madon, ngayban, tongtien, nhanvien.hoten " +
                         "from donhang, nhanvien " +
                         "where donhang.manhanvien = nhanvien.manhanvien ";
            Thuvien.LoadData(sql, dgvDH);
        }

        private void FormDH_Load(object sender, EventArgs e)
        {
            txtMadh.Enabled = false;
            txtNgaynhap.Enabled = false;
            txtTongtien.Enabled = false;
            txtNguoiban.Enabled = false;
            loadDH();
            Thuvien.CustomDisabledButton(btnXoa);
        }

        private void FormDH_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }

        private void dgvDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMadh.Text = dgvDH.Rows[i].Cells[0].Value.ToString();
                txtNgaynhap.Text = dgvDH.Rows[i].Cells[1].Value.ToString();
                txtTongtien.Text = dgvDH.Rows[i].Cells[2].Value.ToString();
                txtNguoiban.Text = dgvDH.Rows[i].Cells[3].Value.ToString();
                loadCTDH();
                Thuvien.CustomEnabledButton(btnXoa);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String madon = txtMadh.Text.Trim();
            String sql = "delete from chitietdonhang where madon='" + madon + "'";
            Thuvien.ExecuteQuery(sql);
            sql = "delete from donhang where madon='" + madon + "'";
            Thuvien.ExecuteQuery(sql);
            loadDH();
            resetText();
            Thuvien.CustomDisabledButton(btnXoa);
            loadCTDH();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String search = txtSearch.Text;
            String sql = "select madon, ngayban, tongtien, quanly.hoten " +
                         "from donhang, quanly " +
                         "where donhang.manhanvien = quanly.maquanly and madon like '%" + search + "%' " +
                         "union all " +
                         "select madon, ngayban, tongtien, nhanvien.hoten " +
                         "from donhang, nhanvien " +
                         "where donhang.manhanvien = nhanvien.manhanvien and madon like '%" + search + "%'";
            Thuvien.LoadData(sql, dgvDH);

        }
    }
}
