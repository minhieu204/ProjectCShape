using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace btl.ThongKe
{
    public partial class ThongKeQL : Form
    {
        public ThongKe thongKe;
        public ThongKeQL(ThongKe parent)
        {
            InitializeComponent();
            Thuvien.CustomDataGridView(dataGridView1);
            this.thongKe = parent;
            comboBox1.SelectedItem = "Hàng Bán Chạy";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int check = comboBox1.SelectedIndex;

            if (check == 0)
            {
                string query = @"SELECT sp.masp AS MaHang, sp.tensp AS TenHang, ncc.mancc AS MaNCC, sp.giaban AS GiaBan, 
                    SUM(ctdh.soluong) AS TongSoluongBan, sp.soluong AS SoluongTon 
                    FROM chitietdonhang ctdh 
                    JOIN sanpham sp ON ctdh.masp = sp.masp 
                    JOIN nhacungcap ncc ON sp.mancc = ncc.mancc 
                    GROUP BY sp.masp, sp.tensp, ncc.mancc, sp.giaban, sp.soluong ORDER BY sp.soluong DESC";

                // Sử dụng phương thức LoadData từ Thuvien
                Thuvien.LoadData(query, dataGridView1);
            }
            else if (check == 1)
            {
                string query = "SELECT s.masp, s.tensp, s.mancc, s.giaban, s.soluong FROM sanpham AS s WHERE s.soluong > 100 ORDER BY s.soluong DESC, s.tensp ASC;";

                Thuvien.LoadData(query, dataGridView1);
            }
            else if (check == 2)
            {
                string query = "SELECT s.masp, s.tensp, s.mancc, s.giaban, s.soluong FROM sanpham AS s WHERE s.soluong < 50 ORDER BY s.soluong ASC, s.tensp ASC;";

                Thuvien.LoadData(query, dataGridView1);
            }
        
        }

    }
}
