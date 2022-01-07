using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DalApi;
namespace BL
{
    partial class BL: BlApi.IBL
    {
        public BO.BaseStation GetBaseStation(int id)
        {
            BO.BaseStation boBaseStation = new BO.BaseStation();
            try
            {
                DO.BaseStation doBaseStation = dalLayer.GetBaseStation(id);
                doBaseStation.CopyPropertiesTo(boBaseStation);
                boBaseStation.BaseStationLocation=new BO.Location();
                boBaseStation.BaseStationLocation.Latitude = doBaseStation.Latitude;
                boBaseStation.BaseStationLocation.Longitude = doBaseStation.Longitude;
                boBaseStation.DronesInCharge = from d in dalLayer.GetAllDroneCharge()
                                               where d.StationID == id
                                               select new BO.DroneInCharging()
                                               {
                                                   ID = d.DroneID,
                                                   BatteryStatus = GetDrone(d.DroneID).BatteryStatus,
                                               };
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch(DO.EntityHasBeenDeleted ex)
            {
                throw new BO.EntityHasBeenDeleted(ex.ID, ex.EntityName);
            }
            return boBaseStation;
        }
        public IEnumerable<BO.BaseStationToList> GetAllBaseStation(Predicate<BO.BaseStationToList> predicate = null)
        {
            IEnumerable<BO.BaseStationToList> baseStationToLists= from BaseStationDO in dalLayer.GetAllBaseStations()
                   select new BO.BaseStationToList()
                   {
                       ID = BaseStationDO.ID,
                       StationName = BaseStationDO.StationName,
                       FreeChargingSlots = BaseStationDO.FreeChargingSlots,
                       BusyChargingSlots = GetBaseStation(BaseStationDO.ID).DronesInCharge.Count(),
                   };
            if (predicate == null)
                return baseStationToLists;
            return baseStationToLists.Where(p => predicate(p));

        }
 
        public void AddBaseStation(BO.BaseStation baseStation)
        {

            //Add DO.BaseStation            
            DO.BaseStation baseStationDO = new DO.BaseStation()
            {
                ID = baseStation.ID,
                StationName = baseStation.StationName,
                FreeChargingSlots = baseStation.FreeChargingSlots,
                Latitude = baseStation.BaseStationLocation.Latitude,
                Longitude = baseStation.BaseStationLocation.Longitude,
                Deleted = (DO.Deleted)1,
            };
            
            try
            {
                dalLayer.AddBaseStation(baseStationDO);
            }
            catch(DO.MissingIdException ex)
            {
                throw new BO.DuplicateIdException(baseStationDO.ID, "BaseStation", "BaseStation ID is missing", ex);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(baseStationDO.ID,"BaseStation", "BaseStation ID is illegal", ex);
            }
        }
        public void UpdateBaseStation(int id, string name, int sum)
        {
            //Update DO.BaseStation            
            DO.BaseStation BaseStationDO = new DO.BaseStation();
            BaseStationDO = dalLayer.GetBaseStation(id);
            if (name!=null)
            {
                BaseStationDO.StationName = name;
            }
            if (sum!=0)
            {
                BaseStationDO.FreeChargingSlots = sum - dronesToList.Count(dro => dro.Conditions == (BO.DroneConditions)0);
            }
            try
            {
                dalLayer.UpdBaseStation(BaseStationDO);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(BaseStationDO.ID, "BaseStation", "BaseStation ID is illegal", ex);
            }

        }
    }
}
