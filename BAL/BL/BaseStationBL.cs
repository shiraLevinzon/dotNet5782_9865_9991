using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DalApi;
using System.Runtime.CompilerServices;
namespace BL
{
    partial class BL: BlApi.IBL
    {


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BO.BaseStation> GetBaseStations()
        {
          return  (from item in dalLayer.GetAllBaseStations()
                                                      select new BaseStation()
                                                      {
                                                          ID = item.ID,
                                                          StationName = item.StationName,
                                                          FreeChargingSlots = item.FreeChargingSlots,
                                                          BaseStationLocation = new Location() { Longitude = item.Longitude, Latitude = item.Latitude },
                                                          DronesInCharge = new List<DroneInCharging>()
                                                      });
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
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
                boBaseStation.DronesInCharge = new List<DroneInCharging>();
                //boBaseStation.DronesInCharge = from d in GetAllDrones(dro=> dro.location.Latitude == boBaseStation.BaseStationLocation.Latitude&&dro.location.Longitude == boBaseStation.BaseStationLocation.Longitude)
                //                               select new BO.DroneInCharging()
                //                               {
                //                                   ID = d.ID,
                //                                   BatteryStatus =d.BatteryStatus,
                //                               };
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


        [MethodImpl(MethodImplOptions.Synchronized)]
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


        [MethodImpl(MethodImplOptions.Synchronized)]
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
                Deleted = false,
            };
            if (baseStationDO.FreeChargingSlots < 0)
                baseStationDO.FreeChargingSlots = 0;
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


        [MethodImpl(MethodImplOptions.Synchronized)]
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


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteBaseStation(int id)
        {
            try
            {
                BO.BaseStation bs = GetBaseStation(id);
                if (bs.DronesInCharge.Count() != 0)
                    throw new BO.ChargingStationsMaintained(bs.ID, "BaseStation", "Charging stations at the base station contain Drone in charge");
                dalLayer.DeleteBaseStatin(id);
            }
            catch(DO.EntityHasBeenDeleted ex)
            {
                throw new BO.EntityHasBeenDeleted(id, "BaseStation", "This base station has already been deleted", ex);
            }
        }
    }
}
