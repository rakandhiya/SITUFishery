using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SITUFishery.Models;

namespace SITUFishery.DataAccess
{
    public class TebarDAL
    {
        public List<Tebar> GetTebars()
        {
            using (SqlConnection connection = new SqlConnection(Helper.ConnectionVal("SITUFishery")))
            {
                List<Tebar> tebars = new();

                string query = "SELECT " +
                    "Tebar.Id, NoPetak, JumlahKantong, BenihPerKantong, BeratKantong, Tanggal " +
                    "FROM dbo.Tebar " +
                    "INNER JOIN Petak ON Tebar.PetakId = Petak.Id";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    tebars.Add(new Tebar
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Petak = new Petak
                        {
                            NoPetak = (string)reader["NoPetak"]
                        },
                        JumlahKantong = Convert.ToInt32(reader["JumlahKantong"]),
                        BeratKantong = Convert.ToInt32(reader["BeratKantong"]),
                        BenihPerKantong = Convert.ToInt32(reader["BenihPerKantong"]),
                        Tanggal = Convert.ToDateTime(reader["Tanggal"])
                    });
                }

                return tebars;
            }
        }

        public Tebar FindById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Helper.ConnectionVal("SITUFishery")))
            {
                Tebar result = new();

                string query = "SELECT * FROM dbo.Tebar INNER JOIN Petak ON Tebar.PetakId = Petak.Id WHERE dbo.Tebar.id=@id";

                SqlCommand cmd = new SqlCommand(query, connection);
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
                    result.JumlahKantong = Convert.ToInt32(reader["JumlahKantong"]);
                    result.BenihPerKantong = Convert.ToInt32(reader["BenihPerKantong"]);
                    result.BeratKantong = Convert.ToInt32(reader["BeratKantong"]);
                    result.Tanggal = Convert.ToDateTime(reader["Tanggal"].ToString());
                }

                return result;
            }
        }

        public bool Insert(Tebar tebar)
        {
            using (SqlConnection connection = new SqlConnection(Helper.ConnectionVal("SITUFishery")))
            {
                string query = "INSERT INTO " +
                    "dbo.Tebar (PetakId, JumlahKantong, BenihPerKantong, BeratKantong, Tanggal) " +
                    "VALUES(@petakId, @jumlahKantong, @benihPerKantong, @beratKantong, @tanggal)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@petakId", System.Data.SqlDbType.VarChar, 10).Value = tebar.Petak.Id;
                command.Parameters.Add("@jumlahKantong", System.Data.SqlDbType.Int, 3).Value = tebar.JumlahKantong;
                command.Parameters.Add("@benihPerKantong", System.Data.SqlDbType.Int, 3).Value = tebar.BenihPerKantong;
                command.Parameters.Add("beratKantong", System.Data.SqlDbType.Int, 3).Value = tebar.BeratKantong;
                command.Parameters.Add("@tanggal", System.Data.SqlDbType.Date, -1).Value = tebar.Tanggal;

                connection.Open();
                command.Prepare();

                command.ExecuteNonQuery();

                return true;
            }
        }

        public bool Update(Tebar tebar)
        {
            using (SqlConnection connection = new SqlConnection(Helper.ConnectionVal("SITUFishery")))
            {
                string query = "UPDATE dbo.Tebar " +
                    "SET PetakId=@petakId, JumlahKantong=@jumlahKantong, BenihPerKantong=@benihPerKantong, " +
                    "BeratKantong=@beratKantong, Tanggal=@tanggal " +
                    "WHERE Id=@id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = tebar.Id;
                command.Parameters.Add("@petakId", System.Data.SqlDbType.VarChar, 10).Value = tebar.Petak.Id;
                command.Parameters.Add("@jumlahKantong", System.Data.SqlDbType.Int, 3).Value = tebar.JumlahKantong;
                command.Parameters.Add("@benihPerKantong", System.Data.SqlDbType.Int, 3).Value = tebar.BenihPerKantong;
                command.Parameters.Add("beratKantong", System.Data.SqlDbType.Int, 3).Value = tebar.BeratKantong;
                command.Parameters.Add("@tanggal", System.Data.SqlDbType.Date, -1).Value = tebar.Tanggal;

                connection.Open();
                command.Prepare();

                command.ExecuteNonQuery();

                return true;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(Helper.ConnectionVal("SITUFishery")))
            {
                string query = "DELETE FROM dbo.Tebar WHERE Id=@id";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int, 3).Value = id;

                connection.Open();
                command.Prepare();

                command.ExecuteNonQuery();

                return true;
            }
        }
    }
}
