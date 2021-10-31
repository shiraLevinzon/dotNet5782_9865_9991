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
                                tmp2.Requested = DateTime.Now;
                                tmp2.Scheduled = DateTime.Now;
                                DalObject.DalObject.AddParcel(tmp2);
                                break;
                            default:
                                Console.WriteLine("Option not found Please re-enter given");
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("To Assign A Package To Drone press 1,");
                        Console.WriteLine("To Parcel Collection By Drone press 2,");
                        Console.WriteLine("To Delivery Parcel To  Customer press 3,");
                        Console.WriteLine("To Sending Drone To Base Station, press 4,");
                        Console.WriteLine("To Release Drone From Charging At Base Station, press 5,");

                        int u = Console.Read();

                        switch (u)
                        {
                            case 1:

                                Console.WriteLine("Enter the Parcel ID number");
                                int pID1 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the Drone ID number");
                                int dID1 = int.Parse(Console.ReadLine());
                                DalObject.DalObject.AssignPackageToDrone(pID1, dID1);
                                break;
                            case 2:
                                Console.WriteLine("Enter the Parcel ID number");
                                int pID2 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the Drone ID number");
                                int dID2 = int.Parse(Console.ReadLine());
                                DalObject.DalObject.ParcelCollectionByDrone(pID2, dID2);
                                break;
                            case 3:
                                Console.WriteLine("Enter the Parcel ID number");
                                int pID3 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the Drone ID number");
                                int dID3 = int.Parse(Console.ReadLine());
                                DalObject.DalObject.DeliveryParcelToCustomer(pID3, dID3);
                                break;
                            case 4:
                                foreach (BaseStation tmp in DalObject.DalObject.printBaseStation())
                                    tmp.ToString();
                                Console.WriteLine("Enter the BaseStation ID number");
                                int pID4 = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the Drone ID number");
                                int dID4 = int.Parse(Console.ReadLine());
                                DalObject.DalObject.SendingDroneToBaseStation(pID4, dID4);
                                break;

                            case 5:

                                Console.WriteLine("Enter the BaseStation ID number");
                                int pID = int.Parse(Console.ReadLine());
                                Console.WriteLine("Enter the Drone ID number");
                                int dID = int.Parse(Console.ReadLine());
                                DalObject.DalObject.ReleaseDroneFromChargingAtBaseStation(pID, dID);
                                break;

                            default:
                                break;
                        }


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
                                Drone tmp = DalObject.DalObject.DroneSearch(s);
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
                        Console.WriteLine("To view all base stations Press 1");
                        Console.WriteLine("To view all existing Drone press 2");
                        Console.WriteLine("To view all customers in the press database 3");
                        Console.WriteLine("To view the Parcels list, press 4");
                        Console.WriteLine("To view a list of packages that have not yet been assigned to the Drone press 5");
                        Console.WriteLine("To view base stations with available charging stations, press 6");
                        int t = Console.Read();
                        switch (t)
                        {
                            case 1:
                                foreach (BaseStation tmp in DalObject.DalObject.printBaseStation())
                                    tmp.ToString();
                                break;
                            case 2:
                                foreach (Drone tmp in DalObject.DalObject.printDrone())
                                    tmp.ToString();
                                break;
                            case 3:
                                foreach (Customer tmp in DalObject.DalObject.printCustomer())
                                    tmp.ToString();
                                break;
                            case 4:
                                foreach (Parcel tmp in DalObject.DalObject.printParcel())
                                    tmp.ToString();
                                break;
                            case 5:
                                foreach (Parcel tmp in DalObject.DalObject.printParcel())
                                {
                                    if (tmp.DroneId == 0)
                                        tmp.ToString();
                                }
                                break;
                            case 6:
                                foreach (BaseStation tmp in DalObject.DalObject.printBaseStation())
                                {
                                    if (tmp.FreeChargingSlots != 0)
                                        tmp.ToString();
                                }
                                break;
                            default:
                                Console.WriteLine("Option not found.");
                                break;
                        }
                        break;
                    case 5:
                        Console.WriteLine("have a nice day,");
                        break;
                }
            }
        }
    }
}