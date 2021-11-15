using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class Package_in_transfer
    {
        public int ID { get; set; }
        public Priorities priority { get; set; }
        public float distance { get; set; }
        public bool Package_mode { get; set; }
        public WeightCategories Weight { get; set; }
        public Customer_In_Parcel Sender { get; set; }
        public Customer_In_Parcel receives { get; set; }
        public Location Collection { get; set; }
        public Location PackageDestination { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
