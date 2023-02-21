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
    public partial class NhaCungCap : Form
    {
        public NhaCungCap()
        {
            InitializeComponent();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        //Load nhà cung cấp lên datagridview
        private void Load_GridViewNCC()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From NHACUNGCAP";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewNCC.DataSource = dt;
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }
        //Hàm disable các textbox
        private void disable()
        {
            txtMaNCC.Enabled = false;
            txtTenNCC.Enabled = false;
            txtEmail.Enabled = false;
            txtSDT.Enabled = false;
            txtDiaChi.Enabled = false;
        }
        //hàm enable cacs textbox
        private void enable()
        {
            txtTenNCC.Enabled = true;
            txtEmail.Enabled = true;
            txtSDT.Enabled = true;
            txtDiaChi.Enabled = true;
        }
        private void NhaCungCap_Load(object sender, EventArgs e)
        {
            //+Load datagridview nhà cung cấp và chỉ đọc
            Load_GridViewNCC();
            dataGridViewNCC.ReadOnly = true;
            dataGridViewNCC.AllowUserToAddRows = false;
        }
        //Hàm kiểm tra text rỗng
        private bool check_TextRong()
        {
            if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Hãy nhập tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return false;
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ Email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }
            else if (txtSDT.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (check_TextRong() == true)
                {
                    if (txtMaNCC.Text == "")
                    {
                        sqlconn.Open();
                        string sqlInsert = "INSERT INTO NHACUNGCAP(TenNCC, Email, SdtNCC, DiaChi) VALUES(N'" + txtTenNCC.Text + "', '" + txtEmail.Text + "', '" + txtSDT.Text + "', '"+txtSDT.Text+"')";
                        SqlDataAdapter sda = new SqlDataAdapter(sqlInsert, sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridViewNCC.DataSource = dt;
                        sqlconn.Close();
                        NhaCungCap_Load(sender, e);
                        MessageBox.Show("Thêm nhà cung cấp thành công");
                        disable();
                    }
                    if (txtMaNCC.Text != "")
                    {
                        string sqlUpdate = "UPDATE NHACUNGCAP SET TenNCC=N'" + txtTenNCC.Text + "' , Email='" + txtEmail.Text + "', SdtNCC='" + txtSDT.Text + "', DiaChi='"+txtDiaChi.Text+"' WHERE MaNCC= " + txtMaNCC.Text;
                        sqlconn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(sqlUpdate, sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridViewNCC.DataSource = dt;
                        sqlconn.Close();
                        NhaCungCap_Load(sender, e);
                        btnSua.Enabled = false;
                        btnXoa.Enabled = false;
                        disable();
                        MessageBox.Show("Sửa thông tin thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn chưa đủ quyền để truy cập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            enable();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    sqlconn.Open();
                    string sql = "Delete From NHACUNGCAP Where MaNCC='" + txtMaNCC.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    cmd.ExecuteNonQuery();
                    sqlconn.Close();
                    Load_GridViewNCC();
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                }
            }
            catch
            {
                MessageBox.Show("Không được xóa thông tin này!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtTenNCC.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            enable();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dataGridViewNCC_Click(object sender, EventArgs e)
        {
            try
            {
                disable();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtMaNCC.Text = dataGridViewNCC.CurrentRow.Cells[0].Value.ToString();
                txtTenNCC.Text = dataGridViewNCC.CurrentRow.Cells[1].Value.ToString();
                txtEmail.Text = dataGridViewNCC.CurrentRow.Cells[2].Value.ToString();
                txtSDT.Text = dataGridViewNCC.CurrentRow.Cells[3].Value.ToString();
                txtDiaChi.Text = dataGridViewNCC.CurrentRow.Cells[4].Value.ToString();
            }
            catch
            {
                MessageBox.Show("DataGridView Chưa có giá trị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From NhaCungCap Where TenNCC Like N'%" + txtTimKiem.Text + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridViewNCC.DataSource = ds.Tables[0];
                sqlconn.Close();
            }
            catch
            {
                MessageBox.Show("Tìm kiếm không thành công!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

    }
}
