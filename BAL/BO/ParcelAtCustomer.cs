using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{
    public class ParcelAtCustomer
    {
        public int ID { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities priority { get; set; }
        public Situations Situation { get; set; }
        public CustomerInParcel CustomerInParcel { get; set; }
    }
}
