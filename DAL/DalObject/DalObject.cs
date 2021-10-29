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
                    BatteryStatus = r1.NextDouble(),
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
                    ID = r1.Next(1000, 10000),
                    SenderID = r1.Next(1000, 10000),
                    TargetID = r1.Next(1000, 10000),
                    Weight = (WeightCategories)r1.Next(1, 4),
                    priority = (Priorities)r1.Next(1, 4),
                    DroneId = r1.Next(1000, 10000),
                    Requested = DateTime.Now,
                    Scheduled = DateTime.Now,
                });
            }
        }
    }
    public class DalObject
    {
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public static void AddDrone(Drone tmp)
        {
            DataSource.DroneList.Add(tmp);
        }
        public static void AddBaseStation(BaseStation tmp)
        {
            DataSource.BaseStationList.Add(tmp);
        }
        public static void AddCustomer(Customer tmp)
        {
            DataSource.CustomerList.Add(tmp);
        }
        public static void AddParcel(Parcel tmp)
        {
            DataSource.ParcelList.Add(tmp);
        }
        /// Update functions 



        /// Search functions
        public static Drone DroneSearch(int p)
        {
            foreach (Drone tmp in DataSource.DroneList)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new Drone();
        }
        public static BaseStation BaseStationSearch(int p)
        {
            foreach (BaseStation tmp in DataSource.BaseStationList)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new BaseStation();
        }
        public static Customer CustomerSearch(int p)
        {
            foreach (Customer tmp in DataSource.CustomerList)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new Customer();
        }
        public static Parcel ParcelSearch(int p)
        {
            foreach (Parcel tmp in DataSource.ParcelList)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new Parcel();
        }
        //פונקציות הדפסה 
        public static List<Drone> printDrone()
        {
            return DataSource.DroneList;
        }
        public static List<BaseStation> printBaseStation()
        {
            return DataSource.BaseStationList;
        }
        public static List<Customer> printCustomer()
        {
            return DataSource.CustomerList;
        }
        public static List<Parcel> printParcel()
        {
            return DataSource.ParcelList;
        }
        //פונקציית עדכון
        public static void AssignPackageToDrone(int pID, int dID)
        {
            Drone d = DroneSearch(dID);
            Parcel p = ParcelSearch(pID);
            p.DroneId = dID;
            p.Scheduled = DateTime.Now;
            d.DroneCondition = (DroneStatuses)2;
        }
        public static void ParcelCollectionByDrone(int pID, int dID)
        {
            Drone d = DroneSearch(dID);
            Parcel p = ParcelSearch(pID);
            p.PickedUp = DateTime.Now;
            d.MaxWeight = p.Weight;
        }
        public static void DeliveryParcelToCustomer(int pID, int dID)
        {
            Drone d = DroneSearch(dID);
            Parcel p = ParcelSearch(pID);
            p.Delivered = DateTime.Now;
            d.DroneCondition = (DroneStatuses)0;
        }
        public static void SendingDroneToBaseStation(int bsID, int dID)
        {
            Drone d = DroneSearch(dID);
            BaseStation bs = BaseStationSearch(bsID);
            d.DroneCondition = (DroneStatuses)1;
            DroneCharge dc = new DroneCharge();
            dc.DroneID = dID;
            dc.StationID = bsID;
            bs.FreeChargingSlots--;

        }
        public static void ReleaseDroneFromChargingAtBaseStation(int bsID,int dID)
        {
            Drone d = DroneSearch(dID);
            BaseStation bs = BaseStationSearch(bsID);
            d.DroneCondition = (DroneStatuses)0;
            d.BatteryStatus = 1;
            bs.FreeChargingSlots++;

        }
    }
}