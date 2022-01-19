using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SITUFishery.Models;
using System.Data.SqlClient;

namespace SITUFishery.DataAccess
{
    public class ReportPenggunaanSDMDAL
    {
        public static List<ReportPenggunaanSDM> GetReport(int month)
        {
            using SqlConnection connection = new(Helper.ConnectionVal("SITUFishery"));
            List<ReportPenggunaanSDM> report = new();

            string query = "SELECT NoPetak, " +
                "AVG(Quantity) AS RerataPakanHarian, " +
                "AVG(Obat) AS RerataObatHarian, " +
                "AVG(Kaporit) AS RerataKaporitHarian " +
                "FROM Petak " +
                "JOIN PakanHarian ON PakanHarian.PetakId = Petak.Id " +
                "JOIN AirHarian ON AirHarian.PetakId = Petak.Id " +
                "WHERE MONTH(PakanHarian.Tanggal) = @month " +
                "GROUP BY NoPetak";

            SqlCommand command = new(query, connection);
            command.Parameters.Add("@month", System.Data.SqlDbType.Int, 3).Value = month;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                report.Add(new ReportPenggunaanSDM
                {
                    NoPetak = (string) reader["NoPetak"],
                    RerataPakanHarian = Convert.ToDouble(reader["RerataPakanHarian"]),
                    RerataObatHarian = Convert.ToDouble(reader["RerataObatHarian"]),
                    RerataKaporitHarian = Convert.ToDouble(reader["RerataKaporitHarian"])
                });
            }

            return report;
        }
    }
}
