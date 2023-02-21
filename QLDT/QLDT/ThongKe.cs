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
    public partial class ThongKe : Form
    {
        SqlConnection sqlconn = new SqlConnection(ConnectionSQL.conn());
        public ThongKe()
        {
            InitializeComponent();
        }
        private void Load_GridView()
        {
            try
            {
                sqlconn.Open();
                string sql = "SELECT H.MaHDB,NgayLapHDB,SUM(DonGiaNhap) as GN,SUM(S.DonGiaBan) AS GB,SUM(TongTien) AS DT, SUM(TongTien - S.DonGiaNhap*SoLuongBan) AS LAI,CONVERT(numeric(18,2),(100*SUM(TongTien - S.DonGiaNhap*SoLuongBan)/SUM(TongTien))) AS TILE FROM KHACHHANG K, HOADONBAN H, CT_HDBAN CT, SANPHAM S WHERE H.MaHDB=CT.MaHDB AND K.MaKH=H.MaKH AND S.MaSP=CT.MaSP GROUP BY H.MaHDB,NgayLapHDB";
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
        private void Load_TongSLSPDaBan()
        {
            sqlconn.Open();
            string sql = "SELECT SUM(SoLuongBan) as sl FROM CT_HDBAN";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            lbTongSL.Text = ds.Tables[0].Rows[0]["sl"].ToString();
            sqlconn.Close();
        }
        private void Load_TongDoanhThu()
        {
            sqlconn.Open();
            string sql = "SELECT SUM(TongTien) as dt FROM CT_HDBAN";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            lbTongDT.Text = ds.Tables[0].Rows[0]["dt"].ToString();
            sqlconn.Close();
        }
        private void Load_TongLoiNhuan()
        {
            sqlconn.Open();
            string sql = "SELECT SUM(TongTien - S.DonGiaNhap*SoLuongBan) AS ln FROM CT_HDBAN CT,SANPHAM S WHERE CT.MaSP=S.MaSP";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            LBTongLN.Text = ds.Tables[0].Rows[0]["ln"].ToString();
            sqlconn.Close();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            Load_GridView();
            LoadKhachHang_Combobox();
            Load_TongSLSPDaBan();
            Load_TongDoanhThu();
            Load_TongLoiNhuan();
            this.dataGridView1.AllowUserToAddRows = false;
        }

        private void btnTK_KH_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string sql = "SELECT H.MaHDB,NgayLapHDB,SUM(DonGiaNhap) as GN,SUM(S.DonGiaBan) AS GB,SUM(TongTien) AS DT, SUM(TongTien - S.DonGiaNhap*SoLuongBan) AS LAI,CONVERT(numeric(18,2),(100*SUM(TongTien - S.DonGiaNhap*SoLuongBan)/SUM(TongTien))) AS TILE FROM KHACHHANG K, HOADONBAN H, CT_HDBAN CT, SANPHAM S WHERE H.MaHDB=CT.MaHDB AND K.MaKH=H.MaKH AND S.MaSP=CT.MaSP AND K.MaKH = " + cbxKH.SelectedValue.ToString() + "  GROUP BY H.MaHDB,NgayLapHDB";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            sqlconn.Close();
        }

        private void btnTK_NB_Click(object sender, EventArgs e)
        {
            sqlconn.Open();
            string sql = "SELECT H.MaHDB,NgayLapHDB,SUM(DonGiaNhap) as GN,SUM(S.DonGiaBan) AS GB,SUM(TongTien) AS DT, SUM(TongTien - S.DonGiaNhap*SoLuongBan) AS LAI,CONVERT(numeric(18,2),(100*SUM(TongTien - S.DonGiaNhap*SoLuongBan)/SUM(TongTien))) AS TILE FROM KHACHHANG K, HOADONBAN H, CT_HDBAN CT, SANPHAM S WHERE H.MaHDB=CT.MaHDB AND K.MaKH=H.MaKH AND S.MaSP=CT.MaSP AND NgayLapHDB >= '" + dateBD.Text + "' AND NgayLapHDB <='" + dateKT.Text + "' GROUP BY H.MaHDB,NgayLapHDB";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlconn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            sqlconn.Close();           
        }

        
    }
}
