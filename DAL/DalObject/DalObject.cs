using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace DalObject
{
    public class DataSource
    {
        internal static List<Drone> DroneList = new List<Drone>();
        internal static List<BaseStation> BaseStationList = new List<BaseStation>();
        internal static List<Customer> CustomerList = new List<Customer>();
        internal static List<Parcel> ParcelList = new List<Parcel>();
        internal static List<DroneCharge> DroneChargeList = new List<DroneCharge>();

        internal static Random r1 = new Random();
        internal static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        internal class Config
        {
            public static int IdCount = 0;
        }
        #region Initialize
        public static void Initialize()
        {
            InitializeBaseStation();
            InitializeCustomer();
            InitializeDrone();
            InitializeParcel();
        }

        static void InitializeDrone()
        {
            for (int i = 0; i < 5; i++)
            {
                DroneList.Add(new Drone()
                {
                    ID = r1.Next(1000, 10000),
                    Model = "p1",
                    MaxWeight = (WeightCategories)r1.Next(1, 4),
                    BatteryStatus = Math.Round(r1.NextDouble(),2),
                    DroneCondition = (DroneStatuses)r1.Next(1, 4),
                });
                ;
            }

        }
        static void InitializeCustomer()
        {
            for (int i = 0; i < 10; i++)
            {
                CustomerList.Add(new Customer()
                {
                    ID = r1.Next(100000000, 1000000000),
                    Name = $"Customer {i}",
                    Phone = $"0{r1.Next(50, 60)}{r1.Next(1000000, 10000000)}",
                    Latitude = GetRandomNumber(33.7, 36.3),
                    Longitude = GetRandomNumber(29.3, 33.5),

                });
                ;
            }

        }
        static void InitializeBaseStation()
        {
            for (int i = 0; i < 2; i++)
            {
                BaseStationList.Add(new BaseStation()
                {
                    ID = r1.Next(1000, 10000),
                    StationName = $"Station{i}",
                    FreeChargingSlots = r1.Next(1, 31),
                    Latitude = GetRandomNumber(33.7, 36.3),
                    Longitude = GetRandomNumber(29.3, 33.5),

                } );
                ;
            }
        }
        public static void InitializeParcel()
        {
            for (int i = 0; i < 10; i++)
            {
                ParcelList.Add(new Parcel()
                {
                    ID = Config.IdCount++,
                    SenderID = r1.Next(1000, 10000),
                    TargetID = r1.Next(1000, 10000),
                    Weight = (WeightCategories)r1.Next(1, 4),
                    priority = (Priorities)r1.Next(1, 4),
                    DroneId = r1.Next(1000, 10000),
                    Requested = DateTime.Now,
                }) ;
            }
        }
    }
    #endregion Initialize

    public class DalObject
    {
        public DalObject() { DataSource.Initialize(); }

        #region add functions
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddDrone(Drone tmp)
        {
            DataSource.DroneList.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddBaseStation(BaseStation tmp)
        {
            DataSource.BaseStationList.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddCustomer(Customer tmp)
        {
            DataSource.CustomerList.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddParcel(Parcel tmp)
        {
            tmp.ID = DataSource.Config.IdCount++;
            DataSource.ParcelList.Add(tmp);
        }
        #endregion add functions
        #region Search functions
        /// <summary>
        /// Drone Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns> specific Drone</returns>
        public Drone DroneSearch(int p)
        {
            foreach (Drone tmp in DataSource.DroneList)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new Drone();
        }
        /// <summary>
        /// BaseStationSearch
        /// </summary>
        /// <param name="p"></param>
        /// <returns>specific BaseStation</returns>
        public BaseStation BaseStationSearch(int p)
        {
            foreach (BaseStation tmp in DataSource.BaseStationList)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new BaseStation();
        }
        /// <summary>
        /// Customer Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns>specific Customer</returns>
        public Customer CustomerSearch(int p)
        {
            foreach (Customer tmp in DataSource.CustomerList)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new Customer();
        }
        /// <summary>
        /// Parcel Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns>spesific Parcel</returns>
        public Parcel ParcelSearch(int p)
        {
            foreach (Parcel tmp in DataSource.ParcelList)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new Parcel();
        }
        #endregion Search functions
        #region print function
        /// <summary>
        /// print Drone
        /// </summary>
        /// <returns>drone list</returns>
        public List<Drone> printDrone()
        {
            return DataSource.DroneList.Take(DataSource.DroneList.Count).ToList();

        }
        /// <summary>
        /// print BaseStation
        /// </summary>
        /// <returns>BaseStation List</returns>
        public List<BaseStation> printBaseStation()
        {
            return DataSource.BaseStationList.Take(DataSource.BaseStationList.Count).ToList();
        }
        /// <summary>
        /// print Customer
        /// </summary>
        /// <returns>Customer List</returns>
        public List<Customer> printCustomer()
        {
            return DataSource.CustomerList.Take(DataSource.CustomerList.Count).ToList();

        }
        /// <summary>
        /// print Parcel
        /// </summary>
        /// <returns>Parcel List</returns>
        public List<Parcel> printParcel()
        {
            return DataSource.ParcelList.Take(DataSource.ParcelList.Count).ToList();
        }
        #endregion print function
        #region Update functions 
        /// <summary>
        /// Assign A Package To A Drone
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void AssignPackageToDrone(int pID, int dID)
        {
            int index1 = DataSource.ParcelList.FindIndex(x => x.ID == pID);
            int index2 = DataSource.DroneList.FindIndex(x => x.ID == dID);

            Parcel p = DataSource.ParcelList[index1];
            Drone d = DataSource.DroneList[index2];

            p.DroneId = dID;
            p.Scheduled = DateTime.Now;
            d.DroneCondition = (DroneStatuses)2;

            DataSource.ParcelList[index1] = p;
            DataSource.DroneList[index2] = d;
            
        }
        /// <summary>
        /// Parcel Collection By A Drone
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void ParcelCollectionByDrone(int pID, int dID)
        {

            int index1 = DataSource.ParcelList.FindIndex(x => x.ID == pID);
            int index2 = DataSource.DroneList.FindIndex(x => x.ID == dID);

            Parcel p = DataSource.ParcelList[index1];
            Drone d = DataSource.DroneList[index2];

            p.PickedUp = DateTime.Now;
            d.MaxWeight = p.Weight;

            DataSource.ParcelList[index1] = p;
            DataSource.DroneList[index2] = d;
            
        }
        /// <summary>
        /// Delivery Parcel To Customer
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void DeliveryParcelToCustomer(int pID, int dID)
        {
            int index1 = DataSource.ParcelList.FindIndex(x => x.ID == pID);
            int index2 = DataSource.DroneList.FindIndex(x => x.ID == dID);

            Parcel p = DataSource.ParcelList[index1];
            Drone d = DataSource.DroneList[index2];

            p.Delivered = DateTime.Now;
            d.DroneCondition = (DroneStatuses)0;

            DataSource.ParcelList[index1] = p;
            DataSource.DroneList[index2] = d;
            
        }
        /// <summary>
        /// Sending Drone To BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void SendingDroneToBaseStation(int bsID, int dID)
        {
            int index1 = DataSource.BaseStationList.FindIndex(x => x.ID == bsID);
            int index2 = DataSource.DroneList.FindIndex(x => x.ID == dID);

            BaseStation bs = DataSource.BaseStationList[index1];
            Drone d = DataSource.DroneList[index2];

            bs.FreeChargingSlots--;
            d.DroneCondition = (DroneStatuses)1;

            DataSource.BaseStationList[index1] = bs;
            DataSource.DroneList[index2] = d;


            DroneCharge dc = new DroneCharge();
            dc.DroneID = dID;
            dc.StationID = bsID;
            DataSource.DroneChargeList.Add(dc);

        }
        /// <summary>
        /// Release Drone From Charging At BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void ReleaseDroneFromChargingAtBaseStation(int bsID,int dID)
        {
            int index1 = DataSource.BaseStationList.FindIndex(x => x.ID == bsID);
            int index2 = DataSource.DroneList.FindIndex(x => x.ID == dID);

            BaseStation bs = DataSource.BaseStationList[index1];
            Drone d = DataSource.DroneList[index2];

            d.BatteryStatus = 1;
            bs.FreeChargingSlots++;
            d.DroneCondition = (DroneStatuses)0;


            DataSource.BaseStationList[index1] = bs;
            DataSource.DroneList[index2] = d;
        }
        #endregion Update functions
    }
}