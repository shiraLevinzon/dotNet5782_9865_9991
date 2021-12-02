using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelToList
    {
        public int ID { get; set; }
        public string SenderName { get; set; }
        public string RecieverName { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities ParcelPriority { get; set; }
        public ParcelConditions ParcelCondition { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
