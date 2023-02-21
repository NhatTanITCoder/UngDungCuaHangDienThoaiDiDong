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
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        //Load khách hàng lên datagridview
        private void Load_GridViewKH()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From KhachHang";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridViewKH.DataSource = dt;
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Bạn chưa đủ quyền để truy cập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }
        //Hàm disable các textbox
        private void disable()
        {
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
        }
        //hàm enable cacs textbox
        private void enable()
        {
            txtTenKH.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
        }
        //Hàm load form KHACHHANG
        private void KhachHang_Load(object sender, EventArgs e)
        {
            //+Load datagridview Khách hàng và chỉ đọc
            Load_GridViewKH();
            dataGridViewKH.ReadOnly = true;
            dataGridViewKH.AllowUserToAddRows = false;
        }
        //Hàm kiểm tra text rỗng
        private bool check_TextRong()
        {
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Hãy nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKH.Focus();
                return false;
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }
            else if (txtSDT.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            return true;
        }
        //Lưu thông tin Khách Hàng
        private void btnLuu_Click(object sender, EventArgs e)
        {           
            try
            {
                if (check_TextRong() == true)
                {
                    if (txtMaKH.Text == "")
                    {
                        sqlconn.Open();
                        string sqlInsert = "INSERT INTO KHACHHANG(TenKH, DiaChi, SdtKH) VALUES(N'" + txtTenKH.Text + "', N'" + txtDiaChi.Text + "', '" + txtSDT.Text + "')";
                        SqlDataAdapter sda = new SqlDataAdapter(sqlInsert, sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridViewKH.DataSource = dt;
                        sqlconn.Close();
                        KhachHang_Load(sender, e);
                        MessageBox.Show("Thêm khách hàng thành công");
                        disable();
                    }
                    if (txtMaKH.Text != "")
                    {
                        string sqlUpdate = "UPDATE KHACHHANG SET TenKH=N'" + txtTenKH.Text + "' , DiaChi=N'" + txtDiaChi.Text + "' , SdtKH='" + txtSDT.Text + "' WHERE MaKH= " + txtMaKH.Text;
                        sqlconn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(sqlUpdate, sqlconn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridViewKH.DataSource = dt;
                        sqlconn.Close();
                        KhachHang_Load(sender, e);
                        btnSua.Enabled = false;
                        btnXoa.Enabled = false;
                        disable();
                        MessageBox.Show("Sửa thông tin thành công");
                    }
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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
                    string sql = "Delete From KHACHHANG Where MaKH='" + txtMaKH.Text + "'";
                    SqlCommand cmd = new SqlCommand(sql, sqlconn);
                    cmd.ExecuteNonQuery();
                    sqlconn.Close();
                    Load_GridViewKH();
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
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
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

        private void dataGridViewKH_Click(object sender, EventArgs e)
        {
            disable();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaKH.Text = dataGridViewKH.CurrentRow.Cells[0].Value.ToString();
            txtTenKH.Text = dataGridViewKH.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dataGridViewKH.CurrentRow.Cells[2].Value.ToString();
            txtSDT.Text = dataGridViewKH.CurrentRow.Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From KhachHang Where TenKH Like N'%" + txtTK.Text + "%'";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridViewKH.DataSource = ds.Tables[0];
                sqlconn.Close();
            }
            catch
            {
                MessageBox.Show("Tìm kiếm không thành công!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

    }
}
