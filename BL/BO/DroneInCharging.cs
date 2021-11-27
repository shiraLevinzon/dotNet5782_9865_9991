using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
  public  class DroneInCharging
    {
        public int ID { get; set; }
        public double BatteryStatus { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
