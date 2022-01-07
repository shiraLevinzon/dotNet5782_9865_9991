using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Drone
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneConditions Conditions { get; set; }
        public Location location { get; set; }
        public ParcelInTransfer PackageInTransfer { get; set; }
        public Deleted Deleted { get => Deleted; set => Deleted = (Deleted)0; }
    }
}
