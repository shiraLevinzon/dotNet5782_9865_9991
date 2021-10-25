using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int ID { get; set; }
            public int SenderID { get; set; }
            public int TargetID { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities priority { get; set; }
            public int DroneId { get; set; }
            public DateTime Requested { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public override string ToString()
            {
                return "ID is" + ID + "Sender-id is" + SenderID + "Target-id is" + TargetID + "The Weight is" + Weight + "The priority is" + priority + "The Drone-id is" + DroneId
                    + "the Requested time is" + Requested + "the Scheduled time is" + Scheduled + "The PickedUp time is" + PickedUp + "The Delivered time is" + Delivered;
            }
        }
    }
}
