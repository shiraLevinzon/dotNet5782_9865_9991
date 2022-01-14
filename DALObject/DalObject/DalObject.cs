﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace Dal
{
    public class DataSource
    {
        internal static List<Drone> drones = new List<Drone>();
        internal static List<BaseStation> baseStations = new List<BaseStation>();
        internal static List<Customer> customers = new List<Customer>();
        internal static List<Parcel> parcels = new List<Parcel>();
        internal static List<DroneCharge> droneCharges = new List<DroneCharge>();
        internal static List<User> users = new List<User>();

        //static string DronesPath = @"DronesXml.xml"; //XMLSerializer
        //static string BaseStationsPath = @"BaseStationsXml.xml"; //XElement
        //static string CustonersPath = @"CustomersXml.xml"; //XMLSerializer
        //static string ParcelsPath = @"ParcelsXml.xml"; //XMLSerializer
        //static string UsersPath = @"UsersXml.xml"; //XMLSerializer
        //static string DronesInChargePath = @"DronesInChargeXml.xml"; //XMLSerializer
        //static string configPath = @"configXml.xml"; //XMLSerializer
        internal static Random r1 = new Random();
        internal static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
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
                    ID = i+1,
                    Model = "p1",
                    MaxWeight = (WeightCategories)r1.Next(0, 3),
                    Deleted = false,
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
                    Deleted = false
                }) ;
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
                    StationName = $"Station {i}",
                    FreeChargingSlots = r1.Next(1, 6),
                    Latitude = GetRandomNumber(33.7, 36.3),
                    Longitude = GetRandomNumber(29.3, 33.5),
                    Deleted = false

                });
                ;
            }
        }
        public static void InitializeParcel()
        {
            for (int i = 0; i < 10; i++)
            {
                int temp1 = r1.Next(0, 10);
                int temp2 = r1.Next(0, 10);
                
                parcels.Add(new Parcel()
                {
                    ID = Config.IdCount++,
                    SenderID = customers[temp1].ID,
                    TargetID = customers[temp2].ID,
                    Weight = (WeightCategories)r1.Next(0, 3),
                    priority = (Priorities)r1.Next(0, 3),
                    DroneId = 0,
                    Requested = DateTime.Now,
                    Deleted = false




                    // PickedUp =DateTime(2022,r1.Next(1, 31), r1.Next(1, 31)),
                }) ;
            }
            //XMLTools.SaveListToXMLSerializer(DataSource.drones, DronesPath);
            //XMLTools.SaveListToXMLSerializer(DataSource.baseStations, BaseStationsPath);
            //XMLTools.SaveListToXMLSerializer(DataSource.customers, CustonersPath);
            //XMLTools.SaveListToXMLSerializer(DataSource.parcels, ParcelsPath);
            //XMLTools.SaveListToXMLSerializer(DataSource.users, UsersPath);
            //XMLTools.SaveListToXMLSerializer(DataSource.droneCharges, DronesInChargePath);
            //XMLTools.SaveListToXMLSerializer(RequestPowerConsumptionByDrone().ToList(), configPath);
            
        }
        #endregion Initialize
       
        internal class Config
        {
            public static int IdCount = 1;
            public static double available = 0.01;
            public static double lightWeight = 0.02;
            public static double mediumWeight = 0.03;
            public static double heavyWeight = 0.04;
            public static double DroneLoadingRate = 7.8;
        }
        //public static double[] RequestPowerConsumptionByDrone()
        //{
        //    double[] PowerConsumption = new double[5];
        //    PowerConsumption[0] = DataSource.Config.available;
        //    PowerConsumption[1] = DataSource.Config.lightWeight;
        //    PowerConsumption[2] = DataSource.Config.mediumWeight;
        //    PowerConsumption[3] = DataSource.Config.heavyWeight;
        //    PowerConsumption[4] = DataSource.Config.DroneLoadingRate;
        //    return PowerConsumption;
        //}
    }
    

     partial class DalObject : DalApi.IDal
     {
        //static readonly Lazy<IDal> instance = new Lazy<IDal>(() => new DalObject());
        //public static IDal Instance { get => instance.Value; }
        // static readonly IDal instance = new DalObject();

        // The public Instance property to use 
        //  public static IDal Instance { get { return instance; } }

        static readonly IDal instance = new DalObject();
        public static IDal Instance { get => instance; }
        public DalObject() {
            DataSource.Initialize();

        }


        public double[] RequestPowerConsumptionByDrone()
            {
            double[] PowerConsumption = new double[5];
            PowerConsumption[0] = DataSource.Config.available;
            PowerConsumption[1] = DataSource.Config.lightWeight;
            PowerConsumption[2] = DataSource.Config.mediumWeight;
            PowerConsumption[3] = DataSource.Config.heavyWeight;
            PowerConsumption[4] = DataSource.Config.DroneLoadingRate;
            return PowerConsumption;
        }

        public IEnumerable<DroneCharge> GetAllDroneCharge()
        {
            return DataSource.droneCharges.Take(DataSource.droneCharges.Count);
        }
    }
}
