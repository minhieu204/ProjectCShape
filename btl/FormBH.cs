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
            Thuvien.CustomDataGridView(dgvHD);
        }

        private void loadTongtien()
        {
            String sql = "select sum(thanhtien) from giohang";
            txtTongtien.Text = Thuvien.GetSingleValue(sql).ToString();
            if (txtTongtien.Text.Trim() == "")
            {
                txtTongtien.Text = "....................................................";
            }
        }
        private void resetText()
        {
            txtMasp.Text = "";
            txtTensp.Text = "";
            txtDVT.Text = "";
            txtGiaban.Text = "";
            txtSLco.Text = "";
            txtSLban.Text = "";
            txtThanhtien.Text = "....................................................";
        }

        private void loadSP()
        {
            String sql = "select masp, tensp, donvitinh, giaban, soluong " +
                         "from sanpham ";
            Thuvien.LoadData(sql, dgvSP);
        }

        private void loadGiohang()
        {
            String sql = "select * from giohang";
            Thuvien.LoadData(sql, dgvHD);
        }

        private void insertGiohang()
        {
            String masp = txtMasp.Text.Trim();
            String tensp = txtTensp.Text.Trim();
            String dvt = txtDVT.Text.Trim();
            String giaban = txtGiaban.Text.Trim();
            String slban = txtSLban.Text.Trim();
            String thanhtien = txtThanhtien.Text.Trim();
            String check = "select count(*) from giohang where masp = '" + masp + "'";
            if (!Thuvien.CheckExist(check))
            {
                String sql = "insert into giohang values('"+ masp +"', N'"+ tensp +"', '"+ giaban +"', '"+ dvt +"', '"+ slban +"', '"+ thanhtien +"')";
                Thuvien.ExecuteQuery(sql);
            }
            else
            {
                String sql = "update giohang set thanhtien=thanhtien+'" + thanhtien + "', soluongnhap=soluongnhap+'" + slban + "' where masp='" + masp + "'";
                Thuvien.ExecuteQuery(sql);
            }
        }

        private void loadMadon()
        {
            String sql = "select top 1 madon from donhang order by madon desc";
            int mahd = int.Parse(Thuvien.GetSingleValue(sql).ToString()) + 1;
            txtMahd.Text = mahd.ToString();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            loadSP();
            loadMadon();
            loadGiohang();
            //txtMasp.Enabled = false;
            txtMasp.ReadOnly = true;
            txtMasp.TabStop = false;
            txtMasp.BackColor = Color.White;
            txtMasp.ForeColor = Color.Black;
            txtTensp.Enabled = false;
            txtDVT.Enabled = false;
            txtGiaban.Enabled = false;
            txtSLco.Enabled = false;
            txtSLban.Enabled = false;
            txtMahd.Enabled = false;
            txtNgaynhap.Enabled = false;
            txtNguoiban.Enabled = false;
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnThanhtoan);
            Thuvien.CustomDisabledButton(btnThemmoi);
            Thuvien.CustomDisabledButton(btnNhaplai);
        }

        String maTemp = "";
        private void dgvSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSLban.Enabled = true;
            Thuvien.CustomEnabledTButton(btnThemmoi);
            Thuvien.CustomEnabledTButton(btnNhaplai);
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

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            insertGiohang();
            loadGiohang();  
            resetText();
            Thuvien.CustomDisabledButton(btnThemmoi);
            Thuvien.CustomDisabledButton(btnNhaplai);
            txtSLban.Enabled = false;
            dgvSP.ClearSelection();
            dgvHD.ClearSelection();
            loadTongtien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String sql = "delete from giohang where masp='" + maTemp + "'";
            Thuvien.ExecuteQuery(sql);
            loadGiohang();
            Thuvien.CustomDisabledButton(btnXoa);
            dgvSP.ClearSelection();
            dgvHD.ClearSelection();
            loadTongtien();
        }

        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Thuvien.CustomEnabledTButton(btnXoa);
            int i = e.RowIndex;
            maTemp = dgvHD.Rows[i].Cells[0].Value.ToString();
        }

        private void btnNhaplai_Click(object sender, EventArgs e)
        {
            txtSLban.Text = "";
        }
    }
}
