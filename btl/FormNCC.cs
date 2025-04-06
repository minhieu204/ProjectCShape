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
    public partial class FormNCC : Form
    {
        public FormNCC()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Thuvien.CustomDataGridView(dataGridView1);
        }

        private void resetText()
        {
            txtMancc.Text = "";
            txtTenncc.Text = "";
            txtDiachi.Text = "";
            txtSdt.Text = "";
            txtEmail.Text = "";
            txtSdt.Text = "";
        }

        private void setDisable()
        {
            txtMancc.Enabled = false;
            txtTenncc.Enabled = false;
            txtDiachi.Enabled = false;
            txtSdt.Enabled = false;
            txtEmail.Enabled = false;
        }

        private void setEnable()
        {
            txtMancc.Enabled = true;
            txtTenncc.Enabled = true;
            txtDiachi.Enabled = true;
            txtSdt.Enabled = true;
            txtEmail.Enabled = true;
        }

        private void loadNCC()
        {
            String sql = "select * from NhaCungCap";
            Thuvien.LoadData(sql, dataGridView1);
        }

        private void FormNCC_Load(object sender, EventArgs e)
        {
            loadNCC();
            setDisable();
            Thuvien.CustomDisabledButton(btnLuu);
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void FormNCC_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            setEnable();
            resetText();
            Thuvien.CustomEnabledButton(btnLuu);
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            String mancc = txtMancc.Text.Trim();
            String tenncc = txtTenncc.Text.Trim();
            String diachi = txtDiachi.Text.Trim();
            String email = txtEmail.Text.Trim();
            String sdt = txtSdt.Text.Trim();
            if (String.IsNullOrEmpty(mancc) || String.IsNullOrEmpty(tenncc) || String.IsNullOrEmpty(diachi) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Thuvien.CheckExist("select count(*) from nhacungcap where mancc = '" + mancc + "'"))
            {
                MessageBox.Show("Mã nhà cung cấp đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql = "insert into nhacungcap values('" + mancc + "', N'" + tenncc + "', N'" + diachi + "', '" + email + "', '" + sdt + "')";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Lưu nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadNCC();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnLuu);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMancc.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txtTenncc.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txtDiachi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                txtSdt.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                Thuvien.CustomEnabledButton(btnXoa);
                Thuvien.CustomEnabledButton(btnSua);
                Thuvien.CustomDisabledButton(btnLuu);
                setEnable();
                txtMancc.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            String mancc = txtMancc.Text.Trim();
            String tenncc = txtTenncc.Text.Trim();
            String diachi = txtDiachi.Text.Trim();
            String email = txtEmail.Text.Trim();
            String sdt = txtSdt.Text.Trim();
            if (String.IsNullOrEmpty(tenncc) || String.IsNullOrEmpty(diachi) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String sql = "update nhacungcap set tenncc = N'" + tenncc + "', diachi = N'" + diachi + "', email = '" + email + "', sdt = '" + sdt + "' where mancc = '" + mancc + "'";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Sửa nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadNCC();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            String mancc = txtMancc.Text.Trim();
            String sql = "delete from nhacungcap where mancc = '" + mancc + "'";
            Thuvien.ExecuteQuery(sql);
            MessageBox.Show("Xóa nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadNCC();
            resetText();
            setDisable();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            String search = txtSearch.Text;
            String sql = "select * from nhacungcap where tenncc like N'%" + search + "%'";
            Thuvien.LoadData(sql, dataGridView1);
        }
    }
}
