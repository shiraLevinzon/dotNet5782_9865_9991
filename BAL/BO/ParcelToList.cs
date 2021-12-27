using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelToList
    {
        public int ID { get; set; }
        public   int SenderID { get; set; }
        public int RecieverID { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities ParcelPriority { get; set; }
        public Situations ParcelCondition { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
