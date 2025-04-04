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
    public partial class Loadfrm : Form
    {
        public Loadfrm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Thuvien.Test()) {
                Thuvien.CheckLogin();
                panel2.Width += 3;
                if(panel2.Width >= 180) {
                    label3.Text = "Kết nối thành công! Đang tải dữ liệu...";
                }
                if (panel2.Width >= 360)
                {

                timer1.Stop();
                    if (Datauser.IsSuccess)
                    {
                        if (Datauser.Role == "nhanvien") 
                        {
                            Thuvien.LogLogin(Datauser.ID);
                        }
                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide();
                    }
                    else 
                    {
                        Login login = new Login();
                        login.Show();
                        this.Hide(); 
                    }
                
                }
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Kết nối không thành công! Vui lòng kiểm tra lại!");
                Application.Exit();
            }
        }

        private void Loadfrm_Load(object sender, EventArgs e)
        {

        }
    }
}
