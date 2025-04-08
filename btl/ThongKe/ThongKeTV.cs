using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl.ThongKe
{
    public partial class ThongKeTV : Form
    {
        public ThongKe thongKe;
        public ThongKeTV(ThongKe parent)
        {
            InitializeComponent();
            this.thongKe = parent;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string ngayBD = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string ngayKT = dateTimePicker2.Value.ToString("yyyy-MM-dd");

            string sql = $@"
        SELECT 
            '{ngayBD}' AS NgayBatDau,
            '{ngayKT}' AS NgayKetThuc,

            -- Tổng lương nhân viên
            ISNULL((SELECT SUM(tongluong) 
                    FROM luong 
                    WHERE ngaynhan BETWEEN '{ngayBD}' AND '{ngayKT}'), 0) AS LuongNhanVien,

            -- Tổng phí quảng cáo
            ISNULL((SELECT SUM(Chiphi) 
                    FROM DoiTac 
                    WHERE Ngaybatdau >= '{ngayBD}' AND Ngayketthuc <= '{ngayKT}'), 0) AS PhiQuangCao,

            -- Chi phí điện, nước, sửa chữa
            ISNULL((SELECT SUM(TienDien + TienNuoc + PhiSuaChua)
                    FROM ChiPhi
                    WHERE FORMAT(ThangNam, 'yyyy-MM') 
                          BETWEEN FORMAT(CONVERT(date, '{ngayBD}'), 'yyyy-MM') 
                          AND FORMAT(CONVERT(date, '{ngayKT}'), 'yyyy-MM')), 0) AS ChiPhi,

            -- Tổng tiền nhập hàng
            ISNULL((SELECT SUM(gianhap * soluong) 
                    FROM sanpham 
                    WHERE ngaynhap BETWEEN '{ngayBD}' AND '{ngayKT}'), 0) AS TienNhapHang,

            -- Tổng tiền bán hàng
            ISNULL((SELECT SUM(tongtien) 
                    FROM donhang 
                    WHERE ngayban BETWEEN '{ngayBD}' AND '{ngayKT}'), 0) AS TienBanHang
    ";

            // Load dữ liệu vào DataGridView
            Thuvien.LoadData(sql, dataGridView1);

            // Đọc từ dòng đầu tiên (chỉ có 1 dòng)
            if (dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.Rows[0];

                double tongLuong = Convert.ToDouble(row.Cells["LuongNhanVien"].Value);
                double tongQC = Convert.ToDouble(row.Cells["PhiQuangCao"].Value);
                double tongChiPhi = Convert.ToDouble(row.Cells["ChiPhi"].Value);
                double tongNhap = Convert.ToDouble(row.Cells["TienNhapHang"].Value);
                double tongBan = Convert.ToDouble(row.Cells["TienBanHang"].Value);

                MessageBox.Show($"📊 **Tổng kết từ {ngayBD} đến {ngayKT}**:\n\n" +
                                $"💼 Lương NV      : {tongLuong:n0} ₫\n" +
                                $"📢 Quảng cáo     : {tongQC:n0} ₫\n" +
                                $"🧾 Chi phí khác  : {tongChiPhi:n0} ₫\n" +
                                $"📦 Tiền nhập     : {tongNhap:n0} ₫\n" +
                                $"💰 Tiền bán      : {tongBan:n0} ₫",
                                "📈 Kết quả thống kê",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có dữ liệu trong khoảng thời gian đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }
    }
}
