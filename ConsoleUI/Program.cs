using System;
using IDAL.DO;
using IDAL;
using DalObject;
namespace ConsoleUI
{
    class Program
    { 
        public static void Main(string[] args)
        {
            int i = 0;
            while (i != 5)
            {
                Console.WriteLine("To add an option, press 1,");
                Console.WriteLine("For the Update option, press 2,");
                Console.WriteLine("For display option press 3,");
                Console.WriteLine("To view the inventory option, press 4,");
                Console.WriteLine("To exit, press 5,");
                i = Console.Read();
                switch (i)
                {
                    case 1:
                        string a;
                        int b = 0;
                        Console.WriteLine("What do you want to add? to finish enter end");
                        a = Console.ReadLine();
                        while (a != "end")
                        {
                            switch (a)
                            {
                                case "Drone":
                                    Drone tmp = new Drone();
                                    Console.WriteLine("enter the Drone's id");
                                    tmp.ID = Console.Read();
                                    Console.WriteLine("Insert the Drone model");
                                    tmp.Model = Console.ReadLine();
                                    Console.WriteLine("Press 0 if the package weight is low. 1 If the weight of the omelet is normal. 12 If the weight of the package is high");
                                    b = Console.Read();
                                    tmp.MaxWeight = (WeightCategories)b;
                                    Console.WriteLine("Enter Drone loading status");
                                    tmp.BatteryStatus = Console.Read();
                                    Console.WriteLine("Insert the Drone condition(Enter 0 if the Drone is available 1 if the Drone is in maintenance or 2 if the Drone is on delivery)");
                                    b = Console.Read();
                                    tmp.DroneCondition = (DroneStatuses)b;
                                    DalObject.DalObject.AddDrone(tmp);
                                    break;
                                case "BaseStation":
                                    BaseStation tmp3 = new BaseStation();
                                    Console.WriteLine("Enter the ID number of the base station");
                                    tmp3.ID = Console.Read();
                                    Console.WriteLine("Enter the name of the base station");
                                    tmp3.StationName = Console.ReadLine();
                                    Console.WriteLine("Enter the number of available charging stations at the station");
                                    tmp3.FreeChargingSlots = Console.Read();
                                    Console.WriteLine("Enter the Longitude of the station");
                                    tmp3.Longitude = Console.Read();
                                    Console.WriteLine("Enter the latitude of the station");
                                    tmp3.Latitude = Console.Read();
                                    DalObject.DalObject.AddBaseStation(tmp3);
                                    break;
                                case "Customer":
                                    Customer tmp1 = new Customer();
                                    Console.WriteLine("Enter the customer ID number");
                                    tmp1.ID = Console.Read();
                                    Console.WriteLine("Enter the customer name");
                                    tmp1.Name = Console.ReadLine();
                                    Console.WriteLine("Enter the customer's phone number");
                                    tmp1.Phone = Console.ReadLine();
                                    Console.WriteLine("Enter the Longitude longitude");
                                    tmp1.Longitude = Console.Read();
                                    Console.WriteLine("Enter the Latitude of the customer location");
                                    tmp1.Latitude = Console.Read();
                                    DalObject.DalObject.AddCustomer(tmp1);
                                    break;
                                case "Parcel":
                                    Parcel tmp2 = new Parcel();
                                    Console.WriteLine("Enter an identification number of the package.");
                                    tmp2.ID = Console.Read();
                                    Console.WriteLine("The conference identifies a sending customer");
                                    tmp2.SenderID = Console.Read();
                                    Console.WriteLine("The conference identifies a receiving customer");
                                    tmp2.TargetID = Console.Read();
                                    Console.WriteLine("Enter a weight category (light,normal,heavy)");
                                    b = Console.Read();
                                    tmp2.Weight = (WeightCategories)b;
                                    Console.WriteLine("Enter Priority Level (Low=0, Normal=1, High=2)");
                                    b = Console.Read();
                                    tmp2.priority = (Priorities)b;
                                    Console.WriteLine("Enter Operation Drone ID (0 if not assigned)");
                                    tmp2.DroneId = Console.Read();
                                    // tmp.Requested = DateTime.Now;
                                    tmp2.Scheduled = DateTime.Now;
                                    DalObject.DalObject.AddParcel(tmp2);
                                    break;
                                default:
                                    Console.WriteLine("Option not found Please re-enter given");
                                    a = Console.ReadLine();
                                    break;
                            }
                        }
                        break;
                    case 2:



                        break;
                    case 3:
                        Console.WriteLine("To view Drone press 1,");
                        Console.WriteLine("To view base station press 2,");
                        Console.WriteLine("To view a client press 3,");
                        Console.WriteLine("To view a package, press 4,");
                        int s = Console.Read();
                        switch (s)
                        {
                            case 1:
                                Console.WriteLine("Enter the Drone ID number");
                                s = Console.Read();
                                Drone tmp=DalObject.DalObject.DroneSearch(s);
                                tmp.ToString();
                                break;
                            case 2:
                                Console.WriteLine("Enter the base station ID number");
                                s = Console.Read();
                                BaseStation tmp1 = DalObject.DalObject.BaseStationSearch(s);
                                tmp1.ToString();
                                break;
                            case 3:
                                Console.WriteLine("Enter the customer ID number");
                                s = Console.Read();
                                Customer tmp2 = DalObject.DalObject.CustomerSearch(s);
                                tmp2.ToString();
                                break;
                            case 4:
                                Console.WriteLine("Enter the ID number of the package");
                                s = Console.Read();
                                Parcel tmp4 = DalObject.DalObject.ParcelSearch(s);
                                tmp4.ToString();
                                break;
                            default:
                                Console.WriteLine("Option not found,");
                                break;
                        }
                        break;
                    case 4:

                        break;
                    case 5:
                        Console.WriteLine("have a nice day,");
                        break;
                }
            }
        }
    }
}
