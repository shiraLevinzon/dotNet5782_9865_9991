using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneToList
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneConditions Conditions { get; set; }
        public Location location { get; set; }
        public int PackagNumberOnTransferred { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}