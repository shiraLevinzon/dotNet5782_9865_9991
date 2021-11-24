﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Drone
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneConditions Conditions { get; set; }
        public Location location { get; set; }
        public Package_in_transfer PackageInTransfer { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
