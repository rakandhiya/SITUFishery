using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITUFishery.Models
{
    public class PakanHarian
    {
        public int Id { get; set; }
        public Petak Petak { get; set; }
        public Pakan Pakan { get; set; }
        public int Quantity { get; set; }
        public DateTime Tanggal { get; set; }
    }
}
