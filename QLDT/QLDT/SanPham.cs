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
using System.IO;

namespace QLDT
{
    public partial class SanPham : Form
    {
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        public SanPham()
        {
            InitializeComponent();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            disable();
        }

        private void disable()
        {
            txtMaSanPham.Enabled = false;
            txtTenSanPham.Enabled = false;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            txtSoLuongTon.Enabled = false;
            txtMoTa.Enabled = false;
            cbxThuongHieu.Enabled = false;
        }
        private void enable()
        {
            txtTenSanPham.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            txtSoLuongTon.Enabled = true;
            txtMoTa.Enabled = true;
            cbxThuongHieu.Enabled = true;
        }
        //load tên thương hiệu lên combobox và value là mã thương hiệu
        private void LoadThuongHieu_Combobox()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From ThuongHieu";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "ThuongHieu");

                cbxThuongHieu.DataSource = ds.Tables[0];
                cbxThuongHieu.DisplayMember = "TenThuongHieu";
                cbxThuongHieu.ValueMember = "MaThuongHieu";
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }

        private void SanPham_Load(object sender, EventArgs e)
        {
            //+Load combobox mã thương hiệu: có chứa tên thương hiệu
            LoadThuongHieu_Combobox();
            //+Load datagridview sản phẩm và chỉ đọc
            Load_GridView();
            dataGridViewSP.ReadOnly = true;
            dataGridViewSP.AllowUserToAddRows = false;
        }

        private void Load_GridView()
        {
            try{
                sqlconn.Open();
                string sql = "Select * From SANPHAM";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dt.Columns.Add("HinhAnh", Type.GetType("System.Byte[]"));
                foreach (DataRow row in dt.Rows)
                {
                    row["HinhAnh"] = File.ReadAllBytes(Application.StartupPath + @"\SanPham\" + Path.GetFileName(row["TenAnh"].ToString()));
                }
                dataGridViewSP.DataSource = dt;
                sqlconn.Close();     
            }catch (Exception ex)
            {
                MessageBox.Show("Bạn chưa đủ quyền để truy cập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void check_TextRong()
        {
            if(txtTenSanPham.Text =="")
            {
                MessageBox.Show("Hãy nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSanPham.Focus();
                return;
            }else if (txtDonGiaNhap.Text == "")
            {
                MessageBox.Show("Hãy nhập đơn giá nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaNhap.Focus();
                return;
            }
            else if (txtDonGiaBan.Text == "")
            {
                MessageBox.Show("Hãy nhập đơn giá bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGiaBan.Focus();
                return;
            }
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            check_TextRong();
            try
            {
                if (txtMaSanPham.Text == "")
                {
                    sqlconn.Open();
                    string sqlInsert = "INSERT INTO SANPHAM(TENSP,DONGIANHAP,DONGIABAN,SOLUONGTON,MOTA,MATHUONGHIEU,TENANH) VALUES(N'" + txtTenSanPham.Text + "', " + txtDonGiaNhap.Text + ", " + txtDonGiaBan.Text + ", " + txtSoLuongTon.Text + ", N'" + txtMoTa.Text + "', " + cbxThuongHieu.SelectedValue.ToString() + ", '" + Path.GetFileName(pictureBox1.ImageLocation) + "')";
                    SqlDataAdapter sda = new SqlDataAdapter(sqlInsert, sqlconn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridViewSP.DataSource = dt;
                    sqlconn.Close();
                    SanPham_Load(sender, e);
                    MessageBox.Show("Thêm sản phẩm thành công");
                    disable();
                }
                if (txtMaSanPham.Text != "")
                {
                    string sqlUpdate = "UPDATE SANPHAM SET TENSP=N'" + txtTenSanPham.Text + "' , DONGIANHAP=" + txtDonGiaNhap.Text + " ,DONGIABAN=" + txtDonGiaBan.Text + ", SOLUONGTON=" + txtSoLuongTon.Text + ",MOTA=N'" + txtMoTa.Text + "',MATHUONGHIEU=" + cbxThuongHieu.SelectedValue.ToString() + ", TenAnh = '" + txtTenAnh.Text + "' WHERE MASP=" + txtMaSanPham.Text;
                    sqlconn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlUpdate, sqlconn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridViewSP.DataSource = dt;
                    sqlconn.Close();
                    SanPham_Load(sender, e);
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    disable();
                    MessageBox.Show("Sửa sản phẩm thành công");                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        //Hàm cho người dùng có thể thao tác lên các dòng textbox...
        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTenSanPham.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            txtSoLuongTon.Enabled = true;
            cbxThuongHieu.Enabled = true;
            txtMoTa.Enabled = true;
        }

        //Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                this.Close();
            }
        }

        //Xóa sản phẩm
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                sqlconn.Open();
                string sql = "Delete From SANPHAM Where MASP='" + txtMaSanPham.Text + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                cmd.ExecuteNonQuery();
                sqlconn.Close();
                Load_GridView();
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtMaSanPham.Text = "";
            txtTenSanPham.Text = "";
            txtDonGiaNhap.Text = "";
            txtDonGiaBan.Text = "";
            txtSoLuongTon.Text = "";
            cbxThuongHieu.Text = "";
            txtMoTa.Text = "";

            txtTenSanPham.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            txtSoLuongTon.Enabled = true;
            cbxThuongHieu.Enabled = true;
            txtMoTa.Enabled = true;
        }

        private void dataGridViewSP_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaSanPham.Text = dataGridViewSP.CurrentRow.Cells[0].Value.ToString();
            txtTenSanPham.Text = dataGridViewSP.CurrentRow.Cells[1].Value.ToString();
            txtDonGiaNhap.Text = dataGridViewSP.CurrentRow.Cells[2].Value.ToString();
            txtDonGiaBan.Text = dataGridViewSP.CurrentRow.Cells[3].Value.ToString();
            txtSoLuongTon.Text = dataGridViewSP.CurrentRow.Cells[4].Value.ToString();
            txtMoTa.Text = dataGridViewSP.CurrentRow.Cells[5].Value.ToString();
            cbxThuongHieu.Text = dataGridViewSP.CurrentRow.Cells[6].Value.ToString();
            byte[] img = (byte[])File.ReadAllBytes(Application.StartupPath + @"\SanPham\" + dataGridViewSP.CurrentRow.Cells[7].Value.ToString());
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
            richTextBox1.Text = Application.StartupPath + @"\SanPham\" + Path.GetFileName(dataGridViewSP.CurrentRow.Cells[7].Value.ToString());
            txtTenAnh.Text = dataGridViewSP.CurrentRow.Cells[7].Value.ToString();
        }

        private void btnLinkImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.jpg;*.jpeg;*.gif;) | *.jpg;*.jpeg;*.gif;";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
                pictureBox1.ImageLocation = ofd.FileName;
                richTextBox1.Text = ofd.FileName;
                txtTenAnh.Text = Path.GetFileName(pictureBox1.ImageLocation);
            }
        }

    }
}
