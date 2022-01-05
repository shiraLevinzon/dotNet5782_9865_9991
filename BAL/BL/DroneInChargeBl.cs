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
        } 
    }
}
