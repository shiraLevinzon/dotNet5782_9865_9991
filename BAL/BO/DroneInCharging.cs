using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
  public  class DroneInCharging
    {
        public int ID { get; set; }
        public double BatteryStatus { get; set; }
        public Deleted Deleted { get => Deleted; set => Deleted = (Deleted)0; }
    }
}
