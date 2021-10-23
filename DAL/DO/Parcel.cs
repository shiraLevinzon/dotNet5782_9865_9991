﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        struct Parcel
        {
            public int ID { get; set; }
            public int SenderID { get; set; }
            public int TargetID { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities priority { get; set; }
            public int DroneId { get; set; }
            public DateTime Requestsed { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
        }
    }
}
