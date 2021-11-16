using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using IDAL;


namespace DalObject
{
    public class DataSource
    {
        internal static List<Drone> drones = new List<Drone>();
        internal static List<BaseStation> baseStations = new List<BaseStation>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();

        internal static Random r1 = new Random();
        internal static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        internal class Config
        {
            public static int IdCount = 0;
            public static double available = 0.04;
            public static double lightWeight = 0.08;
            public static double mediumWeight =0.15 ;
            public static double heavyWeight = 0.20;
            public static double DroneLoadingRate = 0.30;
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
                drones.Add(new Drone()
                {
                    ID = r1.Next(1000, 10000),
                    Model = "p1",
                    MaxWeight = (WeightCategories)r1.Next(0, 3),
          
                });
                ;
            }

        }
        static void InitializeCustomer()
        {
            for (int i = 0; i < 10; i++)
            {
                customers.Add(new Customer()
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
                baseStations.Add(new BaseStation()
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
                parcels.Add(new Parcel()
                {
                    ID = Config.IdCount++,
                    SenderID = r1.Next(1000, 10000),
                    TargetID = r1.Next(1000, 10000),
                    Weight = (WeightCategories)r1.Next(0, 3),
                    priority = (Priorities)r1.Next(0, 3),
                    DroneId = r1.Next(1000, 10000),
                    Requested = DateTime.Now,
                }) ;
            }
        }
    }
    #endregion Initialize

    public class DalObject : IDal
    {
        public DalObject() { DataSource.Initialize(); }

        #region add functions
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddDrone(Drone tmp)
        {
            DataSource.drones.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddBaseStation(BaseStation tmp)
        {
            DataSource.baseStations.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddCustomer(Customer tmp)
        {
            DataSource.customers.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddParcel(Parcel tmp)
        {
            tmp.ID = DataSource.Config.IdCount++;
            DataSource.parcels.Add(tmp);
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
            foreach (Drone tmp in DataSource.drones)
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
            foreach (BaseStation tmp in DataSource.baseStations)
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
            foreach (Customer tmp in DataSource.customers)
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
            foreach (Parcel tmp in DataSource.parcels)
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
        public IEnumerable<Drone> printDrone()
        {
            return DataSource.drones.Take(DataSource.drones.Count).ToList();

        }
        /// <summary>
        /// print BaseStation
        /// </summary>
        /// <returns>BaseStation List</returns>
        public IEnumerable<BaseStation> printBaseStation()
        {
            return DataSource.baseStations.Take(DataSource.baseStations.Count).ToList();
        }
        /// <summary>
        /// print Customer
        /// </summary>
        /// <returns>Customer List</returns>
        public IEnumerable<Customer> printCustomer()
        {
            return DataSource.customers.Take(DataSource.customers.Count).ToList();

        }
        /// <summary>
        /// print Parcel
        /// </summary>
        /// <returns>Parcel List</returns>
        public IEnumerable<Parcel> printParcel()
        {
            return DataSource.parcels.Take(DataSource.parcels.Count).ToList();
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
            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);

            Parcel p = DataSource.parcels[index1];
            Drone d = DataSource.drones[index2];

            p.DroneId = dID;
            p.Scheduled = DateTime.Now;
            

            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;
            
        }
        /// <summary>
        /// Parcel Collection By A Drone
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void ParcelCollectionByDrone(int pID, int dID)
        {

            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);

            Parcel p = DataSource.parcels[index1];
            Drone d = DataSource.drones[index2];

            p.PickedUp = DateTime.Now;
            d.MaxWeight = p.Weight;

            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;
            
        }
        /// <summary>
        /// Delivery Parcel To Customer
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void DeliveryParcelToCustomer(int pID, int dID)
        {
            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);

            Parcel p = DataSource.parcels[index1];
            Drone d = DataSource.drones[index2];

            p.Delivered = DateTime.Now;
            

            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;
            
        }
        /// <summary>
        /// Sending Drone To BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void SendingDroneToBaseStation(int bsID, int dID)
        {
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);

            BaseStation bs = DataSource.baseStations[index1];
            Drone d = DataSource.drones[index2];

            bs.FreeChargingSlots--;
            

            DataSource.baseStations[index1] = bs;
            DataSource.drones[index2] = d;


            DroneCharge dc = new DroneCharge();
            dc.DroneID = dID;
            dc.StationID = bsID;
            DataSource.droneCharges.Add(dc);

        }
        /// <summary>
        /// Release Drone From Charging At BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void ReleaseDroneFromChargingAtBaseStation(int bsID,int dID)
        {
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);

            BaseStation bs = DataSource.baseStations[index1];
            Drone d = DataSource.drones[index2];

            
            bs.FreeChargingSlots++;
           


            DataSource.baseStations[index1] = bs;
            DataSource.drones[index2] = d;
        }
        #endregion Update functions
        public IEnumerable<double> RequestPowerConsumptionByDrone()
        {
            double[] PowerConsumption = new double[5];
            PowerConsumption[0] = DataSource.Config.available;
            PowerConsumption[1] = DataSource.Config.lightWeight;
            PowerConsumption[2] = DataSource.Config.mediumWeight;
            PowerConsumption[3] = DataSource.Config.heavyWeight;
            PowerConsumption[4] = DataSource.Config.DroneLoadingRate;
            return PowerConsumption;
        }
    }
}
