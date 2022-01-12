using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    public interface IDal
    {
        #region User
        public User GetUser(int id);
        public bool CheckUser(int id);
        public void UpdUser(User tmp);
        public void AddUser(User tmp);
        public void DeleteUser(int uID);
        public IEnumerable<User> GetAllUser(Predicate<User> predicate = null);

        #endregion

        #region Drone
        void AddDrone(Drone tmp);
        public Drone GetDrone(int id);
        public bool CheckDrone(int id);
        public void UpdDrone(Drone tmp);
        public IEnumerable<Drone> GetAllDrones(Predicate<Drone> predicate = null);
        public void AssignPackageToDrone(int pID, int dID);

        public void ParcelCollectionByDrone(int pID, int dID);
        public void DeleteDrone(int dID);
        #endregion

        #region BaseStation
        public BaseStation GetBaseStation(int id);
        public bool CheckBaseStation(int id);
        public void UpdBaseStation(BaseStation tmp);

        public void AddBaseStation(BaseStation tmp);
        public IEnumerable<BaseStation> GetAllBaseStations(Predicate<BaseStation> predicate = null);
        public void SendingDroneToBaseStation(int bsID, int dID);
        public void ReleaseDroneFromChargingAtBaseStation(int bsID, int dID);
        public void DeleteBaseStatin(int bsID);
        #endregion

        #region Customer
        public Customer GetCostumer(int id);
        public bool CheckCustomer(int id);
        public void UpdCustomer(Customer tmp);
        public void AddCustomer(Customer tmp);
        public IEnumerable<Customer> GetAllCustomers(Predicate<Customer> predicate = null);
        public void DeliveryParcelToCustomer(int pID, int dID);
        public void DeleteCustomer(int csID);
            #endregion

        #region Parcel
        public void AddParcel(Parcel tmp);
        public Parcel GetParcel(int id);
        public IEnumerable<Parcel> GetAllParcels(Predicate<Parcel> predicate = null);
        public void DeleteParcel(int pID);

        public void UpdParcel(Parcel tmp);
        public bool CheckParcel(int id);

        #endregion

        #region DroneCharge
        public DroneCharge GetDroneInCharging(int id);
        public bool CheckDroneCharge(int id);
        public IEnumerable<DroneCharge> GetAllDroneCharge(Predicate<DroneCharge> predicate = null);
        public void DeleteDroneInCharge(int dgID);
        #endregion
        public double[] RequestPowerConsumptionByDrone();
        public IEnumerable<DroneCharge> GetAllDroneCharge();

    }
}
