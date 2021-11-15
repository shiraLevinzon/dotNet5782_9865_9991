using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;

namespace IBL.BL
{
    class BL
    {
        public BL()
        {
            IDAL.IDal dalobject = new DalObject.DalObject();
            IEnumerable<double> l1 = dalobject.RequestPowerConsumptionByDrone();
           droneList= dalobject.printDrone();
        }
        
    }
}
