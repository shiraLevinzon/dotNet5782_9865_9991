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

                }
                    );
            }
        
        }
         static void InitializeBaseStation() { }

        static void InitializeCustomer() { }

         static void InitializeParcel() { }
        public static void AddDrone()
        {
            Drone tmp=new Drone();
            Console.WriteLine("enter the Drone's id");
            tmp.ID = Console.Read();
            Console.WriteLine("Insert the Drone model");
            tmp.Model = Console.ReadLine();
            Console.WriteLine("Press 0 if the package weight is low. 1 If the weight of the omelet is normal. 12 If the weight of the package is high");
            tmp.MaxWeight = Console.ReadLine();
            Console.WriteLine("Enter Drone loading status");
            tmp.BatteryStatus = Console.Read();
            Console.WriteLine("Insert the Drone condition(Enter 0 if the glider is available 1 if the glider is in maintenance or 2 if the glider is on delivery)");
            tmp.DroneCondition = Console.Read();
            DroneList.Add(tmp);
        }
        public static void AddBaseStation()
        {
            BaseStation tmp = new BaseStation();
            Console.WriteLine("Enter the ID number of the base station");
            tmp.ID = Console.Read();
            Console.WriteLine("Enter the name of the base station");
            tmp.StationName = Console.ReadLine();
            Console.WriteLine("Enter the number of available charging stations at the station");
            tmp.FreeChargingSlots = Console.Read();
            Console.WriteLine("Enter the Longitude of the station");
            tmp.Longitude = Console.Read();
            Console.WriteLine("Enter the latitude of the station");
            tmp.Latitude = Console.Read();
            BaseStationList.Add(tmp);
        }
        public static void AddCustomer()
        {
            Customer tmp = new Customer();
            Console.WriteLine("Enter the customer ID number");
            tmp.ID = Console.Read();
            Console.WriteLine("Enter the customer name");
            tmp.Name = Console.ReadLine();
            Console.WriteLine("Enter the customer's phone number");
            tmp.Phone = Console.ReadLine();
            Console.WriteLine("Enter the Longitude longitude");
            tmp.Longitude = Console.Read();
            Console.WriteLine("Enter the Latitude of the customer location");
            tmp.Latitude = Console.Read();
            CustomerList.Add(tmp);
        }
        public static void AddParcel()
        {
            Parcel tmp = new Parcel();
            Console.WriteLine("Enter an identification number of the package.");
            tmp.ID = Console.Read();
            Console.WriteLine("The conference identifies a sending customer");
            tmp.SenderID = Console.Read();
            Console.WriteLine("The conference identifies a receiving customer");
            tmp.TargetID = Console.Read();
            Console.WriteLine("Enter a weight category (light,normal,heavy)");
            tmp.Weight = Console.Read();
            Console.WriteLine("Enter Priority Level (Low, Normal, High)");
            tmp.priority = Console.Read();
            Console.WriteLine("Enter Operation Drone ID (0 if not assigned)");
            tmp.DroneId = Console.Read();
            tmp.Requested = DateTime.Now;
            Console.WriteLine("Enter the time to assign the package to the Drone");
            Console.WriteLine("Enter a package pick-up time from the sender");
            Console.WriteLine("Enter the time of arrival of the package to the recipient");
          //  tmp.Scheduled
          //tmp.PickedUp
          //tmp.Delivered
        }
    }

}

