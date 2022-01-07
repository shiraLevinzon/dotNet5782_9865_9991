using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DalApi;
using DO;
namespace BL
{
    partial class BL: BlApi.IBL
    {
        public BO.DroneInCharging GetDroneInCharge(int id)
        {
            try
            {
                BO.DroneInCharging dc = new DroneInCharging()
                {
                    BatteryStatus = GetDrone(id).BatteryStatus,
                    ID = dalLayer.GetDroneInCharging(id).DroneID,
                };
                return dc;
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch(DO.EntityHasBeenDeleted ex)
            {
                throw new BO.EntityHasBeenDeleted(ex.ID, ex.EntityName);
            }
        }
        public IEnumerable<BO.DroneInCharging> GetAllDroneInCharge(int id,Predicate<BO.DroneInCharging> predicate = null)
        {
            BO.BaseStation bs = GetBaseStation(id);
            IEnumerable<BO.DroneInCharging> droneInChargings = from Drone in bs.DronesInCharge
                                                                   select new BO.DroneInCharging()
                                                                   {
                                                                       ID=Drone.ID,
                                                                       BatteryStatus=Drone.BatteryStatus,
                                                                   };
            if (predicate == null)
                return droneInChargings.Where(p=>p.Deleted==(BO.Deleted)1);
            return droneInChargings.Where(p => predicate(p));

        }
    }
}
