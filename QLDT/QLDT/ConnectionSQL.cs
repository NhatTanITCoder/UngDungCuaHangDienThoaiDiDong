using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace QLDT
{
    class ConnectionSQL
    {
        //public static string conn = @"data source=NHATTAN\SQLEXPRESS;initial catalog=DB_MobileStore_v2;uid=sa; pwd=sa2012";
        private SqlConnection connect;
        private DataTable dt;
        private SqlCommand cmd;
        

        public static string conn()
        {

            String name_May = KetNoiServer._tenMay;
            String name_CSDL = KetNoiServer._tenCSDL;
            String user_ID = KetNoiServer._userID;
            String pass_W = KetNoiServer._passW;

            string sql = string.Format("data source={0};initial catalog={1};uid={2};pwd={3};", name_May, name_CSDL, user_ID, pass_W);
            return sql;
        }
        public DataTable SelectData(string sql, List<CustomParameter> lstPara)
        {
            try
            {
                connect.Open();
                cmd = new SqlCommand(sql, connect);//nội dung sql đc truyền vào
                cmd.CommandType = CommandType.StoredProcedure;//set command type cho cmd
                foreach (var para in lstPara)//gán các tham số cho cmd
                {
                    cmd.Parameters.AddWithValue(para.key, para.value);
                }
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
                return null;
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
