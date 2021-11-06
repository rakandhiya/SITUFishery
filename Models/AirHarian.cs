using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITUFishery.Models
{
    public class AirHarian
    {
        public int Id { get; set; }
        public Petak Petak { get; set; }
        public double Alga { get; set; }
        public double PH { get; set; }
        public double Obat { get; set; }
        public double Kaporit { get; set; }
        public DateTime Tanggal { get; set; }
    }
}
