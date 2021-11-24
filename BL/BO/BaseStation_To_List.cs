using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class BaseStation_To_List
    {
        public int ID { get; set; }
        public string StationName { get; set; }
        public int FreeChargingSlots { get; set; }
        public int BusyChargingSlots { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
