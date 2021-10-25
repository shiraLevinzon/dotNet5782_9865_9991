using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{

    internal class DataSource
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
            for(int i=0;i<5;i++)
            {
                DroneList.Add(new Drone()
                {
                    ID = r1.Next(1000, 10000),
                    Model = "p1",
                    MaxWeight = (WeightCategories)r1.Next(1, 4),
                    BatteryStatus = r1.NextDouble(),
                    DroneCondition = (DroneStatuses)r1.Next(1, 4),
                }
                    ) ;
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
                    Latitude = GetRandomNumber(33.7 , 36.3),
                    Longitude= GetRandomNumber(29.3, 33.5),

                }
                    ) ;
                ;
            }

        }
        static void InitializeBaseStation() {
            for (int i = 0; i < 2; i++)
            {
                BaseStationList.Add(new BaseStation()
                {
                    ID = r1.Next(1000, 10000),
                    StationName = $"Station{i}",
                    FreeChargingSlots = r1.Next(1, 31),
                    Latitude = GetRandomNumber(33.7, 36.3),
                    Longitude = GetRandomNumber(29.3, 33.5),

                }
                    );
                ;
            }
        }

         static void InitializeParcel() {
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
                    Requested =
                    Scheduled =
                    PickedUp =
                    Delivered =

         static void InitializeParcel() { }
        public static void AddDrone(Drone tmp)
        {
        DroneList.Add(tmp);
        }
        public static void AddBaseStation(BaseStation tmp)
        {
            BaseStationList.Add(tmp);
        }
        public static void AddCustomer(Customer tmp)
        {
            CustomerList.Add(tmp);
        }
        public static void AddParcel(Parcel tmp)
        {
            ParcelList.Add(tmp);
        }
    }

}

