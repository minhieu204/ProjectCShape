using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl
{
    public partial class FormSP : Form
    {

        public FormSP()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Thuvien.CustomDataGridView(dataGridView1);
        }

        private void resetText()
        {
            txtMasp.Text = "";
            txtTensp.Text = "";
            cbbNCC.SelectedIndex = 0;
            txtGianhap.Text = "";
            txtGiaban.Text = "";
            txtSoluong.Text = "";
            txtNgaynhap.Value = DateTime.Now;
            txtDVT.Text = "";
            txtNguoinhap.Text = Datauser.HoTen;
        }

        private void setDisable()
        {
            txtMasp.Enabled = false;
            txtTensp.Enabled = false;
            cbbNCC.Enabled = false;
            txtGianhap.Enabled = false;
            txtGiaban.Enabled = false;
            txtSoluong.Enabled = false;
            txtNgaynhap.Enabled = false;
            txtDVT.Enabled = false;
            txtNguoinhap.Enabled = false;
        }

        private void setEnable()
        {
            txtMasp.Enabled = true;
            txtTensp.Enabled = true;
            cbbNCC.Enabled = true;
            txtGianhap.Enabled = true;
            txtGiaban.Enabled = true;
            txtSoluong.Enabled = true;
            txtNgaynhap.Enabled = true;
            txtDVT.Enabled = true;
        }

        private void loadSP()
        {
            String sql = "select masp, tensp, tenncc, gianhap, giaban, soluong, ngaynhap, donvitinh, hoten " +
                         "from sanpham, nhacungcap, quanly " +
                         "where sanpham.mancc = nhacungcap.mancc and sanpham.maquanly = quanly.maquanly";
            Thuvien.LoadData(sql, dataGridView1);
            dataGridView1.ClearSelection();
        }

        private void loadCbbNCC()
        {
            String sql = "select * from nhacungcap";
            Thuvien.LoadComboBox(sql, cbbNCC, "mancc", "tenncc");
        }

        private void FormSP_Load(object sender, EventArgs e)
        {
            loadSP();
            loadCbbNCC();
            txtNguoinhap.Text = Datauser.HoTen;
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
            Thuvien.CustomDisabledButton(btnLuu);
        }

        private void FormSP_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i > 0)
            {
                txtMasp.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txtTensp.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                cbbNCC.SelectedValue = Thuvien.GetValueFromCode("select * from nhacungcap", dataGridView1.Rows[i].Cells[2].Value.ToString(), "mancc", "tenncc");
                txtGianhap.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                txtGiaban.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                txtSoluong.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                txtNgaynhap.Value = DateTime.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                txtDVT.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                txtNguoinhap.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
                Thuvien.CustomEnabledButton(btnXoa);
                Thuvien.CustomEnabledButton(btnSua);
                Thuvien.CustomDisabledButton(btnLuu);
                setEnable();
                txtMasp.Enabled = false;
            }
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            resetText();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
            Thuvien.CustomEnabledButton(btnLuu);
            setEnable();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            String masp = txtMasp.Text.Trim();
            String tensp = txtTensp.Text.Trim();
            String mancc = cbbNCC.SelectedValue.ToString();
            String gianhap = txtGianhap.Text.Trim();
            String giaban = txtGiaban.Text.Trim();
            String soluong = txtSoluong.Text.Trim();
            String ngaynhap = txtNgaynhap.Value.ToString();
            String donvitinh = txtDVT.Text.Trim();
            String maquanly = Datauser.ID;
            if (masp == "" || tensp == "" || mancc == "" || gianhap == "" || giaban == "" || soluong == "" || donvitinh == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Thuvien.CheckExist("select count(*) from sanpham where masp = '" + masp + "'"))
            {
                MessageBox.Show("Mã sản phẩm không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql = "insert into sanpham values('" + masp + "', N'" + tensp + "', '" + mancc + "', '" + gianhap + "', '" + giaban + "', '" + soluong + "', '" + ngaynhap + "', N'" + donvitinh + "', '" + maquanly + "')";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Lưu sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadSP();
            resetText();
            setDisable();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String masp = txtMasp.Text.Trim();
            String tensp = txtTensp.Text.Trim();
            String mancc = cbbNCC.SelectedValue.ToString();
            String gianhap = txtGianhap.Text.Trim();
            String giaban = txtGiaban.Text.Trim();
            String soluong = txtSoluong.Text.Trim();
            String ngaynhap = txtNgaynhap.Value.ToString();
            String donvitinh = txtDVT.Text.Trim();
            if (tensp == "" || mancc == "" || gianhap == "" || giaban == "" || soluong == "" || donvitinh == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql = "update sanpham set tensp = N'" + tensp + "', mancc = '" + mancc + "', gianhap = '" + gianhap + "', giaban = '" + giaban + "', soluong = '" + soluong + "', ngaynhap = '" + ngaynhap + "', donvitinh = N'" + donvitinh + "' where masp = '" + masp + "'";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Sửa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadSP();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String masp = txtMasp.Text.Trim();
            String sql = "delete from sanpham where masp = '" + masp + "'";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Xóa sản phẩm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadSP();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String search = txtSearch.Text;
            String sql = "select masp, tensp, tenncc, gianhap, giaban, soluong, ngaynhap, donvitinh, hoten " +
                         "from sanpham, nhacungcap, quanly " +
                         "where sanpham.mancc = nhacungcap.mancc and sanpham.maquanly = quanly.maquanly "+
                         "and tensp like N'%"+ search +"%'";
            Thuvien.LoadData(sql, dataGridView1);
            dataGridView1.ClearSelection();
        }
    }
}
