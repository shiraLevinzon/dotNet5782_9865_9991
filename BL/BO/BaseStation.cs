﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStation
    {
        public int ID { get; set; }
        public string StationName { get; set; }
        public Location BaseStationLocation { get; set; }
        public int FreeChargingSlots { get; set; }
        public List<Drone_in_charging> DronesInCharge { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
