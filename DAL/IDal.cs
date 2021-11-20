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
        public Drone DroneSearch(int p);
        public IEnumerable<Drone> printDrone();
        public void AssignPackageToDrone(int pID, int dID);

        public void ParcelCollectionByDrone(int pID, int dID);
        #endregion

        #region BaseStation
        public void AddBaseStation(BaseStation tmp);
        public IEnumerable<BaseStation> printBaseStation();
        public BaseStation BaseStationSearch(int p);
        public void SendingDroneToBaseStation(int bsID, int dID);

        public void ReleaseDroneFromChargingAtBaseStation(int bsID, int dID);
        #endregion

        #region Customer

        public void AddCustomer(Customer tmp);
        public Customer CustomerSearch(int p);
        public IEnumerable<Customer> printCustomer();
        public void DeliveryParcelToCustomer(int pID, int dID);
            #endregion

        #region Parcel

        public void AddParcel(Parcel tmp);
        public Parcel ParcelSearch(int p);

        public IEnumerable<Parcel> printParcel();
        #endregion

       public IEnumerable<double> RequestPowerConsumptionByDrone();
    }
}
