using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QLDT
{
    class ConnectionServer
    {
        SqlConnection sqlconn;
        public ConnectionServer(string cstring)
        {
            sqlconn = new SqlConnection(cstring);
        }
        public bool IsConnection
        {
            get 
            {
                if (sqlconn.State == System.Data.ConnectionState.Closed)
                    sqlconn.Open();
                return true;
            }
        }
    }
}
