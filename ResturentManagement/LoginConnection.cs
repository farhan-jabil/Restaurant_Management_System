using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ResturentManegment
{
    class LoginConnection
    {
        public SqlConnection con;

        public LoginConnection()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["cc"].ConnectionString);
        }

        public static string type;

    }
}
