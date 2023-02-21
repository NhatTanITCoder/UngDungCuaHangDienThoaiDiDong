using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLDT
{
    public partial class HeThong : Form
    {
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        public HeThong()
        {
            InitializeComponent();
        }
        //private void Load_Quyen_GridView()
        //{
        //    try
        //    {
        //        sqlconn.Open();
        //        string sql = "exec sp_helpuser";
        //        SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
        //        DataTable dt = new DataTable();
        //        sda.Fill(dt);
        //        dataGridView3.DataSource = dt;
        //        sqlconn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Load bị lỗi: " + ex.Message);
        //    }
        //}
        //private void LoadNguoiDung_GridView()
        //{
        //    try
        //    {
        //        sqlconn.Open();
        //        string sql = "exec sp_helprolemember";
        //        SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
        //        DataTable dt = new DataTable();
        //        sda.Fill(dt);
        //        dataGridView2.DataSource = dt;
        //        sqlconn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Load bị lỗi: " + ex.Message);
        //    }
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text != textBox3.Text)
                {
                    MessageBox.Show("Mật khẩu không trùng khớp!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn đổi mật khẩu", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        sqlconn.Open();
                        string sql = "ALTER LOGIN " + textBox1.Text + " WITH PASSWORD = '" + textBox4.Text + "'";
                        SqlCommand cmd = new SqlCommand(sql, sqlconn);
                        cmd.ExecuteNonQuery();
                        sqlconn.Close();
                        MessageBox.Show("Đổi thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Đổi mật khẩu thất bại!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn cấp quyền", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    sqlconn.Open();
                    string sql = "GRANT " + cbxQuyen1.SelectedItem.ToString() + " ON  " + cbxBang1.SelectedItem.ToString() + " TO " + txtTenNguoiDung.Text + "";
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    cmd.ExecuteNonQuery();
                    sqlconn.Close();
                    MessageBox.Show("cấp quyền thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("cấp quyền thất bại!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn hủy quyền", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    sqlconn.Open();
                    string sql = "REVOKE " + cbxQuyen1.SelectedItem.ToString() + " ON  " + cbxBang1.SelectedItem.ToString() + " FROM " + txtTenNguoiDung.Text + "";
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    cmd.ExecuteNonQuery();
                    sqlconn.Close();
                    MessageBox.Show("Hủy quyền thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Hủy quyền thất bại!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn cấp quyền", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    sqlconn.Open();
                    string sql = "GRANT " + cbxPhanQuyen.SelectedItem.ToString() + " ON  " + cbxBang2.SelectedItem.ToString() + " TO " + txtNhomND.Text + "";
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    cmd.ExecuteNonQuery();
                    sqlconn.Close();
                    MessageBox.Show("Cấp quyền thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("cấp quyền thất bại!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn hủy quyền", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    sqlconn.Open();
                    string sql = "REVOKE " + cbxPhanQuyen.SelectedItem.ToString() + " ON  " + cbxBang2.SelectedItem.ToString() + " FROM " + txtNhomND.Text + "";
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    cmd.ExecuteNonQuery();
                    sqlconn.Close();
                    MessageBox.Show("Hủy quyền thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Hủy quyền thất bại!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void HeThong_Load(object sender, EventArgs e)
        {
        }

    }
}
