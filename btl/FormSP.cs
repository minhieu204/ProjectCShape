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
        public void CustomDataGridView(DataGridView dataGridView)
        {
            // Thiết lập cơ bản
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;

            // Thiết lập font
            dataGridView.Font = new Font("Segoe UI", 10);

            // Màu sắc
            dataGridView.BackgroundColor = Color.White;
            dataGridView.GridColor = Color.FromArgb(240, 240, 240);

            // Style cho header cột (KHÔNG thay đổi khi chọn)
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(44, 62, 80); // Giữ nguyên màu khi chọn
            dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White; // Giữ nguyên màu chữ khi chọn

            // Style cho hàng
            dataGridView.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(209, 238, 255);
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Style cho cell
            dataGridView.DefaultCellStyle.Padding = new Padding(5);
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

        }
        public FormSP()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            CustomDataGridView(dataGridView1);
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
            txtNguoinhap.Text = "";
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
        }

        private void FormSP_Resize(object sender, EventArgs e)
        {
            label2.Width = flowLayoutPanel1.Width;
        }

        private void btnThemmoi_Click(object sender, EventArgs e)
        {
            resetText();

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
    }
}
