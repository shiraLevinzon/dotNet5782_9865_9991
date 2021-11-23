using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DalObject;
namespace IDAL
{
    public interface IDal
    {
        #region Drone
        public void AddDrone(Drone tmp);
        public Drone GetDrone(int id);
        public bool CheckDrone(int id);
        public void UpdDrone(Drone tmp);
        public IEnumerable<Drone> printDrone();
        public void AssignPackageToDrone(int pID, int dID);

        public void ParcelCollectionByDrone(int pID, int dID);
        #endregion

        #region BaseStation
        public BaseStation GetBaseStation(int id);
        public bool CheckBaseStation(int id);
        public void UpdBaseStation(BaseStation tmp);

        public void AddBaseStation(BaseStation tmp);
        public IEnumerable<BaseStation> printBaseStation();
        public void SendingDroneToBaseStation(int bsID, int dID);

        public void ReleaseDroneFromChargingAtBaseStation(int bsID, int dID);
        #endregion

        #region Customer
        public Customer GetCostumer(int id);
        public bool CheckCustomer(int id);
        public void UpdCustomer(Customer tmp);
        public void AddCustomer(Customer tmp);
        public IEnumerable<Customer> printCustomer();
        public void DeliveryParcelToCustomer(int pID, int dID);
            #endregion
        #region Parcel
        public void AddParcel(Parcel tmp);
        public Parcel GetParcel(int id);
        public IEnumerable<Parcel> printParcel();
        public void UpdParcel(Parcel tmp);
        public bool CheckParcel(int id);

        #endregion
        public double[] RequestPowerConsumptionByDrone();
    }
}
