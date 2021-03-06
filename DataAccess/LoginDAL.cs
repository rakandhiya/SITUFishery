using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITUFishery.DataAccess
{
    public class LoginDAL
    {
        public static string Login(string username, string password)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "SELECT * FROM dbo.Users WHERE Username=@username AND Password=@password";

            SqlCommand command = new(query, connection);
            command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 20).Value = username;
            command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 20).Value = password;

            connection.Open();
            command.Prepare();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                return username;
            }

            return "";
        }
    }
}
