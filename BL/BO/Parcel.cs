using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Parcel
    {

        public int ID { get; set; }
        public Customer_In_Parcel Sender { get; set; }
        public Customer_In_Parcel Receiver { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public Drone_in_the_package DroneInParcel { get; set; }
        public DateTime Requested { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Delivered { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
