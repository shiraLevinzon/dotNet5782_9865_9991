using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL; 


namespace IBL.BL
{
    public partial class BL:IBL
    {
        public BO.BaseStation GetBaseStation(int id)
        {
            BO.BaseStation boBaseStation = new BO.BaseStation();
            try
            {
                IDAL.DO.BaseStation doBaseStation = dalLayer.GetBaseStation(id);
                doBaseStation.CopyPropertiesTo(boBaseStation);
                boBaseStation.BaseStationLocation=new BO.Location();
                boBaseStation.BaseStationLocation.Latitude = doBaseStation.Latitude;
                boBaseStation.BaseStationLocation.Longitude = doBaseStation.Longitude;
                boBaseStation.DronesInCharge = from d in GetAllDrones()
                                               where d.location == boBaseStation.BaseStationLocation && d.Conditions == (BO.DroneConditions)2
                                               select new BO.DroneInCharging()
                                               {
                                                   ID = d.ID,
                                                   BatteryStatus = d.BatteryStatus,
                                               };
            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }

            return boBaseStation;
        }
        public IEnumerable<BO.BaseStationToList> GetAllBaseStation(Predicate<BO.BaseStationToList> predicate = null)
        {
            IEnumerable<BO.BaseStationToList> baseStationToLists= from BaseStationDO in dalLayer.printBaseStation()
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
            IDAL.DO.BaseStation baseStationDO = new IDAL.DO.BaseStation();
            baseStation.CopyPropertiesTo(baseStationDO);
            baseStationDO.Latitude = baseStation.BaseStationLocation.Latitude;
            baseStationDO.Longitude = baseStation.BaseStationLocation.Longitude;
            try
            {
                dalLayer.AddBaseStation(baseStationDO);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(baseStationDO.ID,"BaseStation", "Student ID is illegal", ex);
            }
        }
        public void UpdateBaseStation(int id, string name, int sum)
        {
            //Update DO.BaseStation            
            IDAL.DO.BaseStation BaseStationDO = new IDAL.DO.BaseStation();
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
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(BaseStationDO.ID, "BaseStation", "Student ID is illegal", ex);
            }

        }
    }
}
