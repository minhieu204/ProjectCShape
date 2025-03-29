using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace btl
{
    internal class Thuvien
    {
        private static readonly string connectionString = "Data Source=.;Initial Catalog=QLSIEUTHI;User ID=sa;Password=1306;";

        public static bool Test()
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    return con.State == ConnectionState.Open;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
        public static void ExecuteQuery(string sql)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static void LoadData(string sql, DataGridView dgv)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
        }

        public static void LoadComboBox(string sql, ComboBox cb, string valueField, string displayField)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row[valueField] = DBNull.Value;
                row[displayField] = "---Chọn---";
                dt.Rows.InsertAt(row, 0);

                cb.DataSource = dt;
                cb.DisplayMember = displayField;
                cb.ValueMember = valueField;
            }
        }

        public static bool CheckExist(string sql)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result > 0;
            }
        }
        public static void LoadExcel(string sql, DataTable dt)
        {
            using (SqlConnection con = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);
            }
        }

    }
}
