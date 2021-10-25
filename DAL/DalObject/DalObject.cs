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

