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
    public partial class BanHang : Form
    {
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        public BanHang()
        {
            InitializeComponent();
            txtMaHD.Enabled = false;
            Disable_CT_HDBan();
            btnSua.Enabled = false;
            btnXoaSP.Enabled = false;
            txtTongTien.Enabled = false;
        }
        private void Enable_HDBan()
        {
            cbxTenNV.Enabled = true;
            cbxKH.Enabled = true;
            dateNgayLapHD.Enabled = true;
            txtGhiChu.Enabled = true;
            btnThemKH.Enabled = true;
            btnThemHD.Enabled = true;
        }
        private void disable_HDBan()
        {
            cbxTenNV.Enabled = false;
            cbxKH.Enabled = false;
            dateNgayLapHD.Enabled = false;
            txtGhiChu.Enabled = false;
            btnThemKH.Enabled = false;
            btnThemHD.Enabled = false;
        }
        private void Enable_CT_HDBan()
        {
            cbxSP.Enabled = true;
            txtSL.Enabled = true;
            txtMoTa.Enabled = true;
            btnLuu.Enabled = true;
        }
        private void Disable_CT_HDBan()
        {
            cbxSP.Enabled = false;
            txtSLTon.Enabled = false;
            txtSL.Enabled = false;
            txtMoTa.Enabled = false;
            txtThanhTien.Enabled = false;
            txtDonGia.Enabled = false;
            btnLuu.Enabled = false;
        }
        private void btnThemKH_Click_1(object sender, EventArgs e)
        {
            openChildForm(new KhachHang());
        }
        private void openChildForm(Form childForm)
        {
            childForm.Show();
        }
        private void LoadNhanVien_Combobox()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From NhanVien";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "NhanVien");

                cbxTenNV.DataSource = ds.Tables[0];
                cbxTenNV.DisplayMember = "TenNV";
                cbxTenNV.ValueMember = "MaNV";
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }
        private void LoadKhachHang_Combobox()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From KhachHang";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "KhachHang");

                cbxKH.DataSource = ds.Tables[0];
                cbxKH.DisplayMember = "TenKH";
                cbxKH.ValueMember = "MaKH";
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }
        private void LoadSanPham_Combobox()
        {
            try
            {
                sqlconn.Open();
                string sql = "Select * From SanPham";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "SanPham");

                cbxSP.DataSource = ds.Tables[0];
                cbxSP.DisplayMember = "TenSP";
                cbxSP.ValueMember = "MaSP";
                sqlconn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi: " + e.StackTrace);
            }
        }
        private void Load_GridView()
        {
            try
            {
                sqlconn.Open();
                string sql = "SELECT CT.MAHDB,TENSP,SOLUONGBAN,CT.DONGIABAN,TONGTIEN FROM CT_HDBAN CT,SANPHAM S WHERE S.MASP=CT.MASP AND MAHDB>=ALL(SELECT TOP 1 MAHDB FROM CT_HDBAN ORDER BY MAHDB DESC )";
                SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                sqlconn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load bị lỗi: " + ex.Message);
            }
        }
        private void btnThemHD_Click_1(object sender, EventArgs e)
        {
            sqlconn.Open();
            string sqlInsert = "INSERT INTO HOADONBAN(MANV,MAKH,NGAYLAPHDB,GHICHU) VALUES(" + cbxTenNV.SelectedValue.ToString() + ", " + cbxKH.SelectedValue.ToString() + ", '" + dateNgayLapHD.Text + "', N'" + txtGhiChu.Text + "')";
            SqlDataAdapter sda = new SqlDataAdapter(sqlInsert, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //dataGridViewSP.DataSource = dt;
            sqlconn.Close();
            MessageBox.Show("Thêm sản phẩm thành công");
            disable_HDBan();
            Enable_CT_HDBan();
        }
        public string GetMaHD()
        {
            string sql = "SELECT TOP 1 MAX(MAHDB) AS MA FROM HOADONBAN";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["MA"].ToString();
            }
            else return "";
        }

        private void BanHang_Load(object sender, EventArgs e)
        {
            LoadNhanVien_Combobox();
            LoadKhachHang_Combobox();
            LoadSanPham_Combobox();
        }
        public string Load_TongTien()
        {
            txtMaHD.Text = GetMaHD();
            string sql = "SELECT THANHTIENHDB TT FROM HOADONBAN WHERE MAHDB=" + txtMaHD.Text + "";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["TT"].ToString();
            }
            else return "0";
        }


        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                txtMaHD.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cbxSP.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtSL.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtDonGia.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtThanhTien.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch
            {
                MessageBox.Show("DataGridView Chưa có giá trị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnHDMoi_Click_1(object sender, EventArgs e)
        {
            txtGhiChu.Text = "";
            txtDonGia.Text = "";
            txtSL.Text = "";
            txtSLTon.Text = "";
            txtMoTa.Text = "";
            txtThanhTien.Text = "";
            Enable_HDBan();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtSL.Enabled = true;
            btnSua.Enabled = true;
            btnXoaSP.Enabled = true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            txtMaHD.Text = GetMaHD();
            sqlconn.Open();
            string sqlInsert = "INSERT INTO CT_HDBAN(MAHDB,MASP,SOLUONGBAN,DONGIABAN,TONGTIEN) VALUES(" + txtMaHD.Text + ", " + cbxSP.SelectedValue.ToString() + ", " + txtSL.Text + "," + txtDonGia.Text + "," + txtThanhTien.Text + ")";
            SqlDataAdapter sda = new SqlDataAdapter(sqlInsert, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
            MessageBox.Show("Thêm sản phẩm thành công");
            Load_GridView();
            txtTongTien.Text = Load_TongTien();
            Disable_CT_HDBan();
        }
        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn xóa", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                sqlconn.Open();
                string sql = "Delete From CT_HDBAN Where MAHDB=" + txtMaHD.Text + " AND MASP=" + cbxSP.SelectedValue.ToString() + "";
                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                cmd.ExecuteNonQuery();
                sqlconn.Close();
                Load_GridView();
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Disable_CT_HDBan();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sqlUpdate = "UPDATE CT_HDBAN SET SOLUONGBAN=" + txtSL.Text + ",TONGTIEN=" + txtThanhTien.Text + "  WHERE MAHDB=" + txtMaHD.Text + " AND MASP=" + cbxSP.SelectedValue.ToString() + "";
            sqlconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlUpdate, sqlconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlconn.Close();
            MessageBox.Show("Sửa sản phẩm thành công");
            Load_GridView();
            Disable_CT_HDBan();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult thoat = MessageBox.Show("Bạn chắc chắn muốn thoát!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (thoat == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cbxSP_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sql = "Select DonGiaBan,SoLuongTon From SanPham where MaSP=" + cbxSP.SelectedValue.ToString() + "";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);

            //sqlconn.Open();
            DataSet ds = new DataSet();
            sda.Fill(ds, "SanPham");

            txtDonGia.Text = ds.Tables[0].Rows[0]["DonGiaBan"].ToString();
            txtSLTon.Text = ds.Tables[0].Rows[0]["SoLuongTon"].ToString();
            sqlconn.Close();
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            float sl = 0;
            float dongia = 0;
            if (txtSL.Text != "")
            {
                sl = float.Parse(txtSL.Text);
            }
            if (txtDonGia.Text != "")
            {
                dongia = float.Parse(txtDonGia.Text);
            }
            txtThanhTien.Text = (sl * dongia).ToString();
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            Enable_CT_HDBan();
            disable_HDBan();
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            openChildForm(new wInHoaDonBanHang());
        }
    }
}
