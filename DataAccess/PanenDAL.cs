using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SITUFishery.Models;

namespace SITUFishery.DataAccess
{
    public class PanenDAL
    {
        public static List<Panen> GetPanens()
        {
            using SqlConnection connection = new SqlConnection(Helper.ConnectionVal("SITUFishery"));
            List<Panen> panens = new();

            string query = "SELECT " +
                "Panen.Id, NoPetak, BeratTotal, Tanggal " +
                "FROM dbo.Panen " +
                "INNER JOIN Petak ON Panen.PetakId = Petak.Id";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                panens.Add(new Panen
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Petak = new Petak
                    {
                        NoPetak = (string)reader["NoPetak"]
                    },
                    BeratTotal = Convert.ToDouble(reader["BeratTotal"]),
                    Tanggal = Convert.ToDateTime(reader["Tanggal"])
                });
            }

            return panens;
        }

        public static Panen FindById(int id)
        {
            using SqlConnection connection = new SqlConnection(Helper.ConnectionVal("SITUFishery"));

            string query = "SELECT * FROM dbo.Panen " +
                "INNER JOIN Petak ON Panen.PetakId = Petak.Id " +
                "WHERE Panen.id=@id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

            connection.Open();
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            Panen result = new();
            while (reader.Read())
            {
                result.Id = Convert.ToInt32(reader["Id"]);
                result.Petak = new Petak
                {
                    Id = Convert.ToInt32(reader["PetakId"]),
                    NoPetak = (string)reader["NoPetak"]
                };
                result.BeratTotal = Convert.ToDouble(reader["BeratTotal"]);
                result.Tanggal = Convert.ToDateTime(reader["Tanggal"].ToString());
            }

            return result;
        }

        public static bool Insert(Panen panen)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "INSERT INTO " +
                "dbo.Panen (PetakId, BeratTotal, Tanggal) " +
                "VALUES(@petakId, @beratTotal, @tanggal)";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@petakId", System.Data.SqlDbType.VarChar, 10).Value = panen.Petak.Id;
            command.Parameters.Add("@beratTotal", System.Data.SqlDbType.Int, 3).Value = panen.BeratTotal;
            command.Parameters.Add("@tanggal", System.Data.SqlDbType.Date, -1).Value = panen.Tanggal;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Update(Panen panen)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "UPDATE dbo.Panen " +
                "SET PetakId=@petakId, BeratTotal=@beratTotal, Tanggal=@tanggal " +
                "WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = panen.Id;
            command.Parameters.Add("@petakId", System.Data.SqlDbType.VarChar, 10).Value = panen.Petak.Id;
            command.Parameters.Add("@beratTotal", System.Data.SqlDbType.Int, 3).Value = panen.BeratTotal;
            command.Parameters.Add("@tanggal", System.Data.SqlDbType.Date, -1).Value = panen.Tanggal;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Delete(int id)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "DELETE FROM dbo.Panen WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }
    }
}
