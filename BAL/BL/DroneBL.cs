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
     partial class BL : BlApi.IBL
     {
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Drone GetDrone(int id)
        {
            BO.Drone boDrone = new BO.Drone();
            try
            {
                //עדכון כל הפרופרטיז חוץ מחבילה בהעברה
                BO.DroneToList dtl = dronesToList.Find(dro => dro.ID == id && dro.Deleted== false);
                DO.Drone d = dalLayer.GetDrone(id);
                d.CopyPropertiesTo(boDrone);
         //       boDrone.Conditions = dtl.Conditions;
                boDrone.location = new BO.Location();
                dtl.CopyPropertiesTo(boDrone);
                boDrone.location.Latitude = dtl.location.Latitude;
                boDrone.location.Longitude = dtl.location.Longitude;
                //עדכון חבילה בעברה
                if (boDrone.Conditions == (BO.DroneConditions)2)
                {
                    //עדכון תז עדיפות ומצב חבילה 
                    BO.ParcelToList parcelHalper = new BO.ParcelToList();
                    parcelHalper = GetAllParcels().FirstOrDefault(par => par.ID == dronesToList.Find(dro => dro.ID == id).PackagNumberOnTransferred);
                    boDrone.PackageInTransfer = new BO.ParcelInTransfer();
                    if (parcelHalper != null)
                    {
                        parcelHalper.CopyPropertiesTo(boDrone.PackageInTransfer);
                        //עדכון לקוח בחבילה (השולח)                    
                        BO.CustomerToList customerToListSender = GetAllCustomer().FirstOrDefault(cus => cus.ID == parcelHalper.SenderID);
                        boDrone.PackageInTransfer.Sender = new BO.CustomerInParcel();
                        boDrone.PackageInTransfer.Sender.ID = customerToListSender.ID;
                        boDrone.PackageInTransfer.Sender.CustomerName = customerToListSender.Name;
                        //עדכון לקוח בחבילה (המקבל)
                        BO.CustomerToList customerToListReciver = GetAllCustomer().FirstOrDefault(cus => cus.ID == parcelHalper.RecieverID);
                        boDrone.PackageInTransfer.Receives = new BO.CustomerInParcel();
                        boDrone.PackageInTransfer.Receives.ID = customerToListSender.ID;
                        boDrone.PackageInTransfer.Receives.CustomerName = customerToListSender.Name;
                        //עדכון מקום איסוף ומקום יעד
                        boDrone.PackageInTransfer.Collection = new BO.Location();
                        boDrone.PackageInTransfer.PackageDestination = new BO.Location();
                        boDrone.PackageInTransfer.Collection.Latitude = GetCustomer(parcelHalper.SenderID).Location.Latitude;
                        boDrone.PackageInTransfer.Collection.Longitude = GetCustomer(parcelHalper.SenderID).Location.Longitude;
                        boDrone.PackageInTransfer.PackageDestination.Latitude = GetCustomer(parcelHalper.RecieverID).Location.Latitude;
                        boDrone.PackageInTransfer.PackageDestination.Longitude = GetCustomer(parcelHalper.RecieverID).Location.Longitude;
                        //עדכון מרחק הובלה
                        boDrone.PackageInTransfer.distance = DistanceTo(boDrone.PackageInTransfer.Collection.Latitude, boDrone.PackageInTransfer.Collection.Longitude, boDrone.PackageInTransfer.PackageDestination.Latitude, boDrone.PackageInTransfer.PackageDestination.Longitude);
                    }
                    else
                    {
                        throw new BO.ImproperMaintenanceCondition(id, "no pakage found that are in transfer with this Drone");
                    }
                }       
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID,"Drone", ex.EntityName);
            }
            catch(BO.ImproperMaintenanceCondition ex)
            {
                throw new BO.ImproperMaintenanceCondition(ex.ID,"Drone", ex.EntityName);
            }
            return boDrone;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<BO.DroneToList> GetAllDrones(Predicate<BO.DroneToList> predicate=null)
        {
            if(predicate!=null)
            {
                return dronesToList.FindAll(dro => predicate(dro)&& dro.Deleted== false);
            }
            return dronesToList.FindAll(dro => dro.Deleted == false);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddDrone(BO.Drone drone, int id)
        {
            DO.Drone DroneDO = new DO.Drone();
            try
            {
                drone.Deleted = false;
                DroneDO.ID = drone.ID;
                DroneDO.MaxWeight = (DO.WeightCategories)drone.MaxWeight;
                DroneDO.Model = drone.Model;

                BO.DroneToList droneToListTMP = new BO.DroneToList();
                drone.CopyPropertiesTo(droneToListTMP);

                droneToListTMP.BatteryStatus = (random.Next(20, 40));
                droneToListTMP.Conditions = (BO.DroneConditions)1;
                droneToListTMP.Deleted= false;
                droneToListTMP.location = new BO.Location();
                droneToListTMP.location.Latitude = GetBaseStation(id).BaseStationLocation.Latitude;
                droneToListTMP.location.Longitude = GetBaseStation(id).BaseStationLocation.Longitude;

                dronesToList.Add(droneToListTMP);
                dalLayer.AddDrone(DroneDO);
                //dalLayer.SendingDroneToBaseStation(id, drone.ID);
                DroneToCharging(drone.ID);
            }
            catch(DO.MissingIdException ex)
            {
                throw new BO.DuplicateIdException(DroneDO.ID, "Drone", "Drone ID is missing", ex);

            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(DroneDO.ID, "Drone", "Drone ID is illegal", ex);
            } 
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateDrone(int id, string model)
        {
            DO.Drone DroneDO = new DO.Drone();

            try
            {
                BO.DroneToList dtl = dronesToList.Find(dro => dro.ID == id && dro.Deleted== false);
                dtl.Model = model;
                DroneDO.ID = dtl.ID;
                DroneDO.Model = dtl.Model;
                DroneDO.MaxWeight = (DO.WeightCategories)dtl.MaxWeight;

                dalLayer.UpdDrone(DroneDO);
            }
            catch(DO.DuplicateIdException ex)
            {
                throw new BO.MissingIdException(DroneDO.ID, "Drone", "Drone ID is illegal", ex);
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(DroneDO.ID, "Drone", "Drone ID is illegal", ex);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteDrone(int id)
        {
            try
            {
                if (GetDrone(id).Conditions == (DroneConditions)2)
                    throw new BO.ImproperMaintenanceCondition(id, "Drone", "Drone in delivery - can not be deleted");
                dronesToList.RemoveAll(x => x.ID == id);
                dalLayer.DeleteDrone(id);
            }
            catch (EntityHasBeenDeleted ex)
            {
                throw new BO.EntityHasBeenDeleted(id, "Drone", "This Drone has already been deleted", ex);
            }
        }
      
        public void simula(int droneId , Action reportProgress,Func<bool> IsTimeRun)
        {
            new Simulator(this, droneId, reportProgress, IsTimeRun);
        }
     }
}

