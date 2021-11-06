using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITUFishery.Models
{
    public class Tebar
    {
        public int Id {  get; set; }
        public Petak Petak {  get; set; }
        public int JumlahKantong {  get; set; }
        public int BenihPerKantong {  get; set; }
        public int BeratKantong {  get; set; }
        public DateTime Tanggal {  get; set; }
    }
}
