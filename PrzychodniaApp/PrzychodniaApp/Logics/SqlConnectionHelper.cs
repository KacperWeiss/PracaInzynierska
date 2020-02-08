using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.Logics
{
    public class SqlConnectionHelper
    {
        SqlConnection sqlConnection;

        public SqlConnectionHelper(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        public bool IsConnection
        {
            get
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                return true;
            }
        }
    }
}
