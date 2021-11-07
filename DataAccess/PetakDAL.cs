using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SITUFishery.Models;

namespace SITUFishery.DataAccess
{
    public class PetakDAL
    {
        public static List<Petak> GetPetaks()
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            List<Petak> petaks = new();

            string query = "SELECT * FROM dbo.Petak";

            SqlCommand command = new(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                petaks.Add(new Petak
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    NoPetak = (string)reader["NoPetak"]
                });
            }

            return petaks;
        }

        public static Petak FindById(int id)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            Petak result = new();

            string query = "SELECT * FROM dbo.Petak WHERE id=@id";

            SqlCommand cmd = new(query, connection);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

            connection.Open();
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Id = Convert.ToInt32(reader["Id"]);
                result.NoPetak = (string)reader["NoPetak"];
            }

            return result;
        }

        public static bool Insert(Petak petak)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "INSERT INTO dbo.Petak (NoPetak) VALUES(@noPetak)";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@noPetak", System.Data.SqlDbType.VarChar, 10).Value = petak.NoPetak;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Update(Petak petak)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "UPDATE dbo.Petak SET NoPetak=@noPetak WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = petak.Id;
            command.Parameters.Add("@noPetak", System.Data.SqlDbType.VarChar, 10).Value = petak.NoPetak;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Delete(int id)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "DELETE FROM dbo.Petak WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }
    }
}
