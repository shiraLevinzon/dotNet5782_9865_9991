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
                return string.Format("ID: {0}\t SenderID:{1}\t TargetID:{2}\t Weight:{3}\t priority:{4}\t DroneId: {5}\t Requested:{6}\t Scheduled:{7}\t PickedUp:{8}\t Delivered:{9}\t", ID, SenderID, TargetID, Weight, priority, DroneId, Requested, Scheduled, PickedUp, Delivered);
            }
        }
    }
}
