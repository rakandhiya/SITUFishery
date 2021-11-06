using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SITUFishery.Models;

namespace SITUFishery.DataAccess
{
    public class PakanDAL
    {
        public static List<Pakan> GetPakans()
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            List<Pakan> pakans = new();

            string query = "SELECT * FROM dbo.Pakan";

            SqlCommand command = new(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                pakans.Add(new Pakan
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Nama = (string)reader["Nama"],
                    Stok = Convert.ToInt32(reader["Stok"])
                });
            }

            return pakans;
        }

        public static Pakan FindById(int id)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            Pakan result = new();

            string query = "SELECT * FROM dbo.Pakan WHERE id=@id";

            SqlCommand cmd = new(query, connection);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

            connection.Open();
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Id = Convert.ToInt32(reader["Id"]);
                result.Nama = (string)reader["Nama"];
                result.Stok = Convert.ToInt32(reader["Stok"]);
            }

            return result;
        }

        public static bool Insert(Pakan pakan)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "INSERT INTO dbo.Pakan (Nama, Stok) VALUES(@nama, @stok)";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@nama", System.Data.SqlDbType.VarChar, 10).Value = pakan.Nama;
            command.Parameters.Add("@stok", System.Data.SqlDbType.Int, 3).Value = pakan.Stok;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Update(Pakan pakan)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "UPDATE dbo.Pakan SET Nama=@nama, Stok=@stok WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = pakan.Id;
            command.Parameters.Add("@nama", System.Data.SqlDbType.VarChar, 10).Value = pakan.Nama;
            command.Parameters.Add("@stok", System.Data.SqlDbType.Int, 3).Value = pakan.Stok;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Delete(int id)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "DELETE FROM dbo.Pakan WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }
    }
}
