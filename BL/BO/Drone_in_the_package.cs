using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Drone_in_the_package
    {
        public int ID { get; set; }
        public double BatteryStatus { get; set; }
        public Location location { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
