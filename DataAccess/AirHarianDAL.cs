using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SITUFishery.Models;

namespace SITUFishery.DataAccess
{
    public class AirHarianDAL
    {
        public static List<AirHarian> GetAirHarians()
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            List<AirHarian> airHarians = new();

            string query = "SELECT " +
                "AirHarian.Id, NoPetak, Alga, PH, Obat, Kaporit, Tanggal " +
                "FROM dbo.AirHarian " +
                "INNER JOIN Petak ON AirHarian.PetakId = Petak.Id";

            SqlCommand command = new(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                airHarians.Add(new AirHarian
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Petak = new Petak
                    {
                        NoPetak = (string)reader["NoPetak"]
                    },
                    Alga = Convert.ToDouble(reader["Alga"]),
                    PH = Convert.ToDouble(reader["PH"]),
                    Obat = Convert.ToDouble(reader["Obat"]),
                    Kaporit = Convert.ToDouble(reader["Kaporit"]),
                    Tanggal = Convert.ToDateTime(reader["Tanggal"])
                });
            }

            return airHarians;
        }

        public static AirHarian FindById(int id)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            AirHarian result = new();

            string query = "SELECT * " +
                "FROM dbo.AirHarian " +
                "INNER JOIN Petak ON AirHarian.PetakId = Petak.Id WHERE dbo.AirHarian.id=@id";

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
                    NoPetak = (string)reader["NoPetak"]
                };
                result.Alga = Convert.ToDouble(reader["Alga"]);
                result.PH = Convert.ToDouble(reader["PH"]);
                result.Obat = Convert.ToDouble(reader["Obat"]);
                result.Kaporit = Convert.ToDouble(reader["Kaporit"]);
                result.Tanggal = Convert.ToDateTime(reader["Tanggal"].ToString());
            }

            return result;
        }

        public static bool Insert(AirHarian airHarian)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "INSERT INTO " +
                "dbo.AirHarian (PetakId, Alga, PH, Obat, Kaporit, Tanggal) " +
                "VALUES(@petakId, @alga, @ph, @obat, @kaporit, @tanggal)";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@petakId", System.Data.SqlDbType.VarChar, 10).Value = airHarian.Petak.Id;
            command.Parameters.Add("@alga", System.Data.SqlDbType.Int, 3).Value = airHarian.Alga;
            command.Parameters.Add("@ph", System.Data.SqlDbType.Int, 3).Value = airHarian.PH;
            command.Parameters.Add("@obat", System.Data.SqlDbType.Int, 3).Value = airHarian.Obat;
            command.Parameters.Add("@kaporit", System.Data.SqlDbType.Int, 3).Value = airHarian.Kaporit;
            command.Parameters.Add("@tanggal", System.Data.SqlDbType.Date, -1).Value = airHarian.Tanggal;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Update(AirHarian airHarian)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "UPDATE dbo.AirHarian " +
                "SET PetakId=@petakId, Alga=@alga, PH=@ph, Obat=@obat, Kaporit=@kaporit " +
                "WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = airHarian.Id;
            command.Parameters.Add("@petakId", System.Data.SqlDbType.VarChar, 10).Value = airHarian.Petak.Id;
            command.Parameters.Add("@alga", System.Data.SqlDbType.Int, 3).Value = airHarian.Alga;
            command.Parameters.Add("@ph", System.Data.SqlDbType.Int, 3).Value = airHarian.PH;
            command.Parameters.Add("@obat", System.Data.SqlDbType.Int, 3).Value = airHarian.Obat;
            command.Parameters.Add("@kaporit", System.Data.SqlDbType.Int, 3).Value = airHarian.Kaporit;
            command.Parameters.Add("@tanggal", System.Data.SqlDbType.Date, -1).Value = airHarian.Tanggal;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static bool Delete(int id)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "DELETE FROM dbo.AirHarian WHERE Id=@id";
            SqlCommand command = new(query, connection);

            command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

            connection.Open();
            command.Prepare();

            command.ExecuteNonQuery();

            return true;
        }

        public static double AveragePH()
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            string query = "SELECT AVG(PH) FROM dbo.AirHarian";
            SqlCommand command = new(query, connection);

            connection.Open();
            var result = command.ExecuteScalar();

            return result.Equals(DBNull.Value) ? 0 : (double)result;
        }
    }
}
