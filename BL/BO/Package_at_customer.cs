using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL.BO
{
    public class Package_at_customer
    {
        public int ID { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities priority { get; set; }
        public Situations Situation { get; set; }
        public Customer_In_Parcel CustomerInParcel { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
