using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration; 

namespace KDUInfoApp
{
    class ConnectionManager
    {
        public static SqlConnection newCon;
        public static string conStr=ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        public static SqlConnection getConnection()
        { 
                newCon = new SqlConnection(conStr); 
                return newCon;
            
            
        }

    }
}
