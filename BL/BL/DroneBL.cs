/using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BL
{
    public partial class BL : IBL
    {
        public BO.Drone GetDrone(int id)
        {
            BO.Drone boDrone = new BO.Drone();
            try
            {
                dronesToList.Find(dro=>dro.ID==id).CopyPropertiesTo(boDrone);
                if(boDrone.Conditions==(BO.DroneConditions)2)
                {
                    BO.Parcel parcelHalper = new BO.Parcel();
                    parcelHalper = GetAllParcels().First(par => par.ID == dronesToList.Find(dro => dro.ID == id).PackagNumberOnTransferred);
                    parcelHalper.CopyPropertiesTo(boDrone.PackageInTransfer);
                    //להוסיף את שאר הפרופרטיז שנמצאים בחבילה בהעברה ולא נמצאים בחבילה
                    //יש צורך בעזרה :-)

                }

            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }

            return boDrone; 
        }
        public IEnumerable<BO.Drone> GetAllDrones()
        {
            return from DroneDO in dalLayer.printDrone()
                   orderby DroneDO.ID//מיון לפי תז
                   select GetDrone(DroneDO.ID);
        }
        public void AddDrone(BO.Drone drone,int id)
        {
           
            //Add DO.BaseStation            
            IDAL.DO.Drone DroneDO = new IDAL.DO.Drone();
            drone.CopyPropertiesTo(DroneDO);
            drone.BatteryStatus = (random.Next(20, 40)) % 100;
            drone.Conditions = (BO.DroneConditions)0;
            drone.location = GetAllBaseStation().First(bas => bas.ID == id).BaseStationLocation;


            BO.DroneToList droneToListTMP = new BO.DroneToList();
            drone.CopyPropertiesTo(droneToListTMP);
            dronesToList.Add(droneToListTMP);
            try
            {
                dalLayer.AddDrone(DroneDO);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(DroneDO.ID, "Drone", "Drone ID is illegal", ex);
            }
        }
        public void UpdateDrone(int id,string model)
        {          
            IDAL.DO.Drone DroneDO = new IDAL.DO.Drone();
            BO.DroneToList dtl = new BO.DroneToList();

            dronesToList.Find(dro => dro.ID == id).CopyPropertiesTo(dtl);
            dronesToList.Remove(dronesToList.Find(dro => dro.ID == id));

            dtl.Model = model;
            dronesToList.Add(dtl);
            dtl.CopyPropertiesTo(DroneDO);
            try
            {
                dalLayer.UpdDrone(DroneDO);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(DroneDO.ID, "Drone", "Drone ID is illegal", ex);
            }
        }
    }
}
