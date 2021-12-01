using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL
{
   public interface IBL
   {
        #region פונקציות תחנת בסיס של שיכבה 2
        public IEnumerable<BO.BaseStation> GetAllBaseStation();
        public BO.BaseStation GetBaseStation(int id);
        public void AddBaseStation(BO.BaseStation baseStation);
        public void UpdateBaseStation(BO.BaseStation baseStation);
        #endregion
        #region פונקציות רחפן של שיכבה 2
        public BO.Drone GetDrone(int id);
        public IEnumerable<BO.Drone> GetAllDrones();
        public void AddDrone(BO.Drone drone, int id);
        public void UpdateDrone(int id, string name);
        #endregion
        #region פונקציות לקוח של שיכבה 2
        public BO.Customer GetCustomer(int id);
        public IEnumerable<BO.Customer> GetAllCustome();
        public void AddCustomer(BO.Customer customer);
        public void UpdateCustomer(BO.Customer customer);
        #endregion
        #region פונקציות חבילה של שיכבה 2
        public BO.Parcel GetParcel(int id);
        public IEnumerable<BO.Parcel> GetAllParcels();
        public void AddParcel(BO.Parcel parcel);
        public void UpdateParcel(BO.Parcel Parcel);
        #endregion
        #region פונקציות עדכון ועזר נוספות מיכבה 2
        public void DroneToCharging(int id);
        public void ReleaseDroneFromCharging(int id, TimeSpan time);
        public void AssignPackageToDrone(int id);
        public void DeliveryOfPackageByDrone(int id);
        #endregion
    }
}
