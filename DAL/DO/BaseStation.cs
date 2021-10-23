using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using IDAL.DO;

namespace IDAL
{
    namespace DO
    {
        public struct BaseStation
        {
            public int ID { get; set; }
            public string StationName { get; set; }
            public int FreeChargingSlots { get; set; }
            public double longitude { get; set; }
            public double Latitude { get; set; }
        }
    }
    
}
