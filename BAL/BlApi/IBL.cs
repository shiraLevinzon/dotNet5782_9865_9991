using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
   public interface IBL
   {
        #region פונקציות משתמשים של שיכבה 2
        public User GetUser(int id);
        public void UpdUser(User tmp);
        public void AddUser(User tmp);
        public IEnumerable<User> GetAllUser(Predicate<User> predicate = null);
        #endregion
        #region פונקציות תחנת בסיס של שיכבה 2
        public IEnumerable<BO.BaseStationToList> GetAllBaseStation(Predicate<BO.BaseStationToList> predicate = null);
        public BO.BaseStation GetBaseStation(int id);
        public void AddBaseStation(BO.BaseStation baseStation);
        public void UpdateBaseStation(int id, string name, int sum);
        public void DeleteBaseStation(int id);

        #endregion
        #region פונקציות רחפן של שיכבה 2
        public BO.Drone GetDrone(int id);
        public IEnumerable<BO.DroneToList> GetAllDrones(Predicate<BO.DroneToList> predicate = null);
        public void AddDrone(BO.Drone drone, int id);
        public void UpdateDrone(int id, string name);
        public void DeleteDrone(int id);
        #endregion
        #region פונקציות לקוח של שיכבה 2
        public BO.Customer GetCustomer(int id);
        public IEnumerable<BO.CustomerToList> GetAllCustomer(Predicate<BO.CustomerToList> predicate = null);
        public void AddCustomer(BO.Customer customer);
        public void UpdateCustomer(int id, string name, string phone);
        public void DeleteCustomer(int id);
        #endregion
        #region פונקציות חבילה של שיכבה 2
        public BO.Parcel GetParcel(int id);
        public IEnumerable<BO.ParcelToList> GetAllParcels(Predicate<BO.ParcelToList> predicate = null, DateTime? date = null);
        public void AddParcel(BO.Parcel parcel);
        public int func(DO.Parcel p);
        public void DeleteParcel(int id);

        //public void UpdateParcel(BO.Parcel Parcel);
        #endregion
        #region פונקציות עדכון ועזר נוספות משיכבה 2
        public void DroneToCharging(int id);
        public void ReleaseDroneFromCharging(int id, TimeSpan time);
        public void AssignPackageToDrone(int id);
        public void CollectParcelByDrone(int id);
        public void DeliveryOfPackageByDrone(int id);
        #endregion
        #region פונקציות רחפנים בטעיה של שיכבה 2
     //   public BO.DroneInCharging GetDroneInCharge(int id);
        public IEnumerable<BO.DroneInCharging> GetAllDroneInCharge(int id, Predicate<BO.DroneInCharging> predicate = null);
        #endregion
    }
}
