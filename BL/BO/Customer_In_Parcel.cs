using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer_In_Parcel
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
