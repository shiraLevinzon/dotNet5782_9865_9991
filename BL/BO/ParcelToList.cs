using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ParcelToList
    {
        public int ID { get; set; }
        public string SenderName { get; set; }
        public string RecieverName { get; set; }
        public WeightCategory Weight { get; set; }
        public Priority ParcelPriority { get; set; }
        public ParcelConditions ParcelCondition { get; set; }


    }
}
