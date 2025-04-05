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
            txtNguoinhap.Enabled = false;
        }

        private void FormSP_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMasp.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtTensp.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            cbbNCC.SelectedValue = Thuvien.GetValueFromCode("select * from nhacungcap", dataGridView1.Rows[i].Cells[2].Value.ToString(), "mancc", "tenncc");
            txtGianhap.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtGiaban.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtSoluong.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            txtNgaynhap.Value = DateTime.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
            txtDVT.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
            txtNguoinhap.Text = dataGridView1.Rows[i].Cells[8].Value.ToString();
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            resetText();
            Thuvien.CustomDisabledButton(btnXoa);
            Thuvien.CustomDisabledButton(btnSua);
            Thuvien.CustomEnabledButton(btnLuu);
        }
    }
}
