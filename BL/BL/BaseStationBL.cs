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
                //boBaseStation.ID = doBaseStation.ID;
                //boBaseStation.StationName = doBaseStation.StationName;
                //boBaseStation.FreeChargingSlots = doBaseStation.FreeChargingSlots;
                boBaseStation.BaseStationLocation.Latitude = doBaseStation.Latitude;
                boBaseStation.BaseStationLocation.Longitude = doBaseStation.Longitude;

                foreach (var item in GetAllDrones().Where(dro => (dro.location == boBaseStation.BaseStationLocation) && (dro.Conditions == (BO.DroneConditions)2)))
                {
                    boBaseStation.DronesInCharge.Add(new BO.DroneInCharging
                    {
                        ID = item.ID,
                        BatteryStatus = item.BatteryStatus,
                    }) ;
                }
            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }

            return boBaseStation;
        }
        public IEnumerable<BO.BaseStationToList> GetAllBaseStation(Predicate<BO.BaseStationToList> predicate = null)
        {
            IEnumerable<BO.BaseStation> baseStations = from BaseStationDO in dalLayer.printBaseStation()
                                                       orderby BaseStationDO.ID//מיון לפי תז
                                                       select GetBaseStation(BaseStationDO.ID);
            List<BO.BaseStationToList> baseStationToLists = new List<BO.BaseStationToList>();
            foreach (var item in baseStations)
            {
                BO.BaseStationToList bs = new BO.BaseStationToList();
                bs.ID = item.ID;
                bs.StationName = item.StationName;
                bs.FreeChargingSlots = item.FreeChargingSlots;
                bs.BusyChargingSlots = item.DronesInCharge.Count();
                baseStationToLists.Add(bs);
            }
            if (predicate == null)
                return baseStationToLists;
            return baseStationToLists.FindAll(p => predicate(p));

        }
        public void AddBaseStation(BO.BaseStation baseStation)
        {

            //Add DO.BaseStation            
            IDAL.DO.BaseStation baseStationDO = new IDAL.DO.BaseStation();
            baseStation.CopyPropertiesTo(baseStationDO);
            //baseStationDO.ID = baseStation.ID;
            //baseStationDO.StationName = baseStation.StationName;
            //baseStationDO.FreeChargingSlots = baseStation.FreeChargingSlots;
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
