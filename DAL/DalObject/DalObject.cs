using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject 
{
    internal class DataSource
    {
        internal List <Drone> DroneList = new List<Drone>();
        internal List<BaseStation> BaseStationList = new List<BaseStation>();
        internal List<Customer> CustomerList = new List<Customer>();
        internal List<Parcel> ParcelList = new List<Parcel>();
        internal List<DroneCharge> DroneChargeList = new List<DroneCharge>();
    }
    internal class Config
    {
        static int D = 0, B = 0, C = 0, p = 0;
    }
    //static Initialize{}
}

