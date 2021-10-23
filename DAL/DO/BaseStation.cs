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
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public override string ToString()
            {
                return string.Format("ID: {0}\t StationName:{1}\t FreeChargingSlots:{2}\t Longitude:{3}\t Latitude:{4}\t", ID, StationName, FreeChargingSlots, Longitude, Latitude);
            }

        }
    }
    
}
