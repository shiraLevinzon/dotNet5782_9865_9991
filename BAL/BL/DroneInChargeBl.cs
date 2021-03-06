using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DalApi;
using DO;
using System.Runtime.CompilerServices;
namespace BL
{
    partial class BL : BlApi.IBL
    {
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.DroneInCharging> GetAllDroneInCharge(int id, Predicate<BO.DroneInCharging> predicate = null)
        {
            BO.BaseStation bs = GetBaseStation(id);
            IEnumerable<BO.DroneInCharging> droneInChargings = from Drone in bs.DronesInCharge
                                                               select new BO.DroneInCharging()
                                                               {
                                                                   ID = Drone.ID,
                                                                   BatteryStatus = Drone.BatteryStatus,
                                                               };
            if (predicate == null)
                return droneInChargings.Where(p => p.Deleted == false);
            return droneInChargings.Where(p => predicate(p));

        }
    }
}
