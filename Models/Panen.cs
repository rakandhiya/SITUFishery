﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITUFishery.Models
{
    public class Panen
    {
        public int Id { get; set; }
        public Petak Petak { get; set; }
        public double BeratTotal { get; set; }
        public DateTime Tanggal { get; set; }
    }
}
