using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SITUFishery.Models;

namespace SITUFishery.DataAccess
{
    public class PakanHarianDAL
    {
        public static List<PakanHarian> GetPakanHarians()
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            List<PakanHarian> pakanHarians = new();

            string query = "SELECT " +
                    "PakanHarian.Id, Petak.NoPetak, Pakan.Nama, Quantity, PakanHarian.Tanggal " +
                    "FROM dbo.PakanHarian " +
                    "INNER JOIN Petak ON PakanHarian.PetakId = Petak.Id " +
                    "INNER JOIN Pakan ON PakanHarian.PakanId = Pakan.Id";

            SqlCommand command = new(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                pakanHarians.Add(new PakanHarian
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Petak = new Petak
                    {
                        NoPetak = (string) reader["NoPetak"]
                    },
                    Pakan = new Pakan
                    {
                        Nama = (string) reader["Nama"]
                    },
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Tanggal = Convert.ToDateTime(reader["Tanggal"])
                });
            }

            return pakanHarians;
        }

        public static PakanHarian FindById(int id)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            PakanHarian result = new();

            string query = "SELECT * " +
                    "FROM dbo.PakanHarian " +
                    "INNER JOIN Petak ON PakanHarian.PetakId = Petak.Id " +
                    "INNER JOIN Pakan ON PakanHarian.PakanId = Pakan.Id " +
                    "WHERE PakanHarian.Id=@id";

            SqlCommand cmd = new(query, connection);
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

            connection.Open();
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Id = Convert.ToInt32(reader["Id"]);
                result.Petak = new Petak
                {
                    Id = Convert.ToInt32(reader["PetakId"]),
                    NoPetak = (string) reader["NoPetak"]
                };
                result.Pakan = new Pakan
                {
                    Id = Convert.ToInt32(reader["PakanId"]),
                    Nama = (string)reader["Nama"]
                };
                result.Quantity = Convert.ToInt32(reader["Quantity"]);
                result.Tanggal = Convert.ToDateTime(reader["Tanggal"]);
            }

            return result;
        }

        public static bool Insert(PakanHarian pakanHarian)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "INSERT INTO dbo.PakanHarian (PetakId, PakanId, Quantity, Tanggal) " +
                "VALUES(@petakId, @pakanId, @quantity, @tanggal)";

            SqlCommand command = new(query, connection);

            command.Parameters.Add("@petakId", System.Data.SqlDbType.VarChar, 10).Value = pakanHarian.Petak.Id;
            command.Parameters.Add("@pakanId", System.Data.SqlDbType.Int, 3).Value = pakanHarian.Pakan.Id;
            command.Parameters.Add("@quantity", System.Data.SqlDbType.Int, 3).Value = pakanHarian.Quantity;
            command.Parameters.Add("@tanggal", System.Data.SqlDbType.Date, -1).Value = pakanHarian.Tanggal;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Update(PakanHarian pakanHarian)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "UPDATE dbo.PakanHarian " +
                "SET PetakId=@petakId, PakanId=@pakanId, Quantity=@quantity, Tanggal=@tanggal " +
                "WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = pakanHarian.Id;
            command.Parameters.Add("@petakId", System.Data.SqlDbType.VarChar, 10).Value = pakanHarian.Petak.Id;
            command.Parameters.Add("@pakanId", System.Data.SqlDbType.Int, 3).Value = pakanHarian.Pakan.Id;
            command.Parameters.Add("@quantity", System.Data.SqlDbType.Int, 3).Value = pakanHarian.Quantity;
            command.Parameters.Add("@tanggal", System.Data.SqlDbType.Date, -1).Value = pakanHarian.Tanggal;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Delete(int id)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "DELETE FROM dbo.PakanHarian WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }
    }
}
