using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelInTransfer
    {
        public int ID { get; set; }
        public Priorities priority { get; set; }
        public float distance { get; set; }
        public bool Package_mode { get; set; }
        public WeightCategories Weight { get; set; }
        public CustomerInParcel Sender { get; set; }
        public CustomerInParcel Receives { get; set; }
        public Location Collection { get; set; }
        public Location PackageDestination { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
