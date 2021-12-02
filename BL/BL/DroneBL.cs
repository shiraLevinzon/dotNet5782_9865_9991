using System;
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
                //עדכון כל הפרופרטיז חוץ מחבילה בהעברה
                dronesToList.Find(dro=>dro.ID==id).CopyPropertiesTo(boDrone);
                //עדכון חבילה בעברה
                if(boDrone.Conditions==(BO.DroneConditions)2)
                {
                    //עדכון תז עדיפות ומצב חבילה 
                    BO.ParcelToList parcelHalper = new BO.ParcelToList();
                    parcelHalper = GetAllParcels().First(par => par.ID == dronesToList.Find(dro => dro.ID == id).PackagNumberOnTransferred);
                    parcelHalper.CopyPropertiesTo(boDrone.PackageInTransfer);
                    //עדכון לקוח בחבילה (השולח) 
                    BO.CustomerToList customerToListSender = GetAllCustomer().First(cus => cus.ID == parcelHalper.SenderID);
                    boDrone.PackageInTransfer.Sender.ID = customerToListSender.ID;
                    boDrone.PackageInTransfer.Sender.CustomerName = customerToListSender.Name;
                    //עדכון לקוח בחבילה (המקבל)
                    BO.CustomerToList customerToListReciver = GetAllCustomer().First(cus => cus.ID == parcelHalper.RecieverID);
                    boDrone.PackageInTransfer.Receives.ID = customerToListSender.ID;
                    boDrone.PackageInTransfer.Receives.CustomerName = customerToListSender.Name;
                    //עדכון מקום איסוף ומקום יעד
                    boDrone.PackageInTransfer.Collection = GetCustomer(parcelHalper.SenderID).Location;
                    boDrone.PackageInTransfer.PackageDestination = GetCustomer(parcelHalper.RecieverID).Location;
                    //עדכון מרחק הובלה
                    boDrone.PackageInTransfer.distance = DistanceTo(boDrone.PackageInTransfer.Collection.Latitude, boDrone.PackageInTransfer.Collection.Longitude, boDrone.PackageInTransfer.PackageDestination.Latitude, boDrone.PackageInTransfer.PackageDestination.Longitude);
                }

            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }

            return boDrone; 
        }
        public IEnumerable<BO.DroneToList> GetAllDrones()
        {
            return dronesToList;

        }
        public void AddDrone(BO.Drone drone,int id)
        {
           
            //Add DO.BaseStation            
            IDAL.DO.Drone DroneDO = new IDAL.DO.Drone();
            drone.CopyPropertiesTo(DroneDO);
            drone.BatteryStatus = (random.Next(20, 40)) % 100;
            drone.Conditions = (BO.DroneConditions)0;
            drone.location = GetBaseStation(id).BaseStationLocation;


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

            try
            {
            BO.DroneToList dtl = new BO.DroneToList();

            dronesToList.Find(dro => dro.ID == id).CopyPropertiesTo(dtl);
            dronesToList.Remove(dronesToList.Find(dro => dro.ID == id));

            dtl.Model = model;
            dronesToList.Add(dtl);
            dtl.CopyPropertiesTo(DroneDO);
           
                dalLayer.UpdDrone(DroneDO);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(DroneDO.ID, "Drone", "Drone ID is illegal", ex);
            }
        }
    }
}
