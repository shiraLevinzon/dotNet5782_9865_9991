using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;

namespace DO
{
    public struct BaseStation
    {
        public int ID { get; set; }
        public string StationName { get; set; }
        public int FreeChargingSlots { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Deleted Deleted { get=> Deleted; set => Deleted = (Deleted)0; }
    }
}

