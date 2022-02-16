using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{
    public class BaseStation
    {
        public int ID { get; set; }
        public string StationName { get; set; }
        public int FreeChargingSlots { get; set; }
        public Location BaseStationLocation { get; set; }
        public IEnumerable<DroneInCharging> DronesInCharge { get; set; }
        public bool Deleted { get; set; }
  
    }
}
