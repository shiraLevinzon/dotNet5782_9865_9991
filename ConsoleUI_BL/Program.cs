using System;
using IBL;
using IBL.BL;
using IBL.BO;
using System.Collections.Generic;
namespace ConsoleUI_BL
{
    class Program
    {
        public static void Main(string[] args)
        {
            IBL.IBL blObject = new BL();
            int i = 0;
            while (i!=5)
            {
                Console.WriteLine("To add an option, press 1,");
                Console.WriteLine("For the Update option, press 2,");
                Console.WriteLine("For display option press 3,");
                Console.WriteLine("To view the inventory option, press 4,");
                Console.WriteLine("To exit, press 5,");
                i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        int a;
                        Console.WriteLine("To add Drone press 1,");
                        Console.WriteLine("To add base station press 2,");
                        Console.WriteLine("To add a client press 3,");
                        Console.WriteLine("To add a package, press 4,");
                        a = int.Parse(Console.ReadLine());
                        switch (a)
                        {
                            case 1:
                                Console.WriteLine("enter id, model, max wight (0-light, 1-normal, 2-heavy), base station id to put the drone to the first charge ");
                                Drone drone = new Drone();
                                drone.ID = int.Parse(Console.ReadLine());
                                drone.Model = Console.ReadLine();
                                drone.MaxWeight = (WeightCategories)int.Parse(Console.ReadLine());
                                drone.location = new Location { };
                                drone.PackageInTransfer = new ParcelInTransfer { };
                                blObject.AddDrone(drone, int.Parse(Console.ReadLine()));
                                
                                break;
                            case 2:
                                Console.WriteLine("enter id, station name, location- latitude and longtitude, number of free slots charge");
                                BaseStation baseStation = new BaseStation();
                                baseStation.ID = int.Parse(Console.ReadLine());
                                baseStation.StationName = Console.ReadLine();
                                baseStation.BaseStationLocation = new Location { Latitude = double.Parse(Console.ReadLine()),Longitude= double.Parse(Console.ReadLine()) };
                                baseStation.FreeChargingSlots= int.Parse(Console.ReadLine());
                                baseStation.DronesInCharge = new List<DroneInCharging>();
                                blObject.AddBaseStation(baseStation);
                                break;
                            case 3:
                                Console.WriteLine("enter id, name, phone, location- latitude and longtitude");
                                Customer customer = new Customer();
                                customer.ID = int.Parse(Console.ReadLine());
                                customer.Name = Console.ReadLine();
                                customer.Phone = Console.ReadLine();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
                                customer.Location = new Location { Latitude = double.Parse(Console.ReadLine()), Longitude = double.Parse(Console.ReadLine()) };
                                customer.PackagesFromCustomer = new List<ParcelAtCustomer>();
                                customer.PackagesToCustomer = new List<ParcelAtCustomer>();
                                blObject.AddCustomer(customer);
                                break;
                            case 4:
                                Console.WriteLine("enter sender id, reciver id, wight (0-light, 1-normal, 2-heavy), Priority (0-low, 1-normal, 2-hight)");
                                Parcel parcel = new Parcel();
                                parcel.Sender.ID = int.Parse(Console.ReadLine());
                                parcel.Receiver.ID = int.Parse(Console.ReadLine());
                                parcel.Weight = (WeightCategories)int.Parse(Console.ReadLine());
                                parcel.Priority = (Priorities)int.Parse(Console.ReadLine());
                                parcel.DroneInParcel = new DroneInParcel();
                                blObject.AddParcel(parcel);

                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:                       
                        Console.WriteLine("To Assign A Package To Drone press 1,");
                        Console.WriteLine("To Parcel Collection By Drone press 2,");
                        Console.WriteLine("To Delivery Parcel To  Customer press 3,");
                        Console.WriteLine("To Sending Drone To Charge, press 4,");
                        Console.WriteLine("To Release Drone From Charging At Base Station, press 5,");
                        Console.WriteLine("To update drone data press 6");
                        Console.WriteLine("To Update station data press 7");
                        Console.WriteLine("To Update customer data 8");
                        int u = int.Parse(Console.ReadLine());
                        switch (u)
                        {
                            case 1:
                                Console.WriteLine("enter id");
                                blObject.AssignPackageToDrone(int.Parse(Console.ReadLine()));
                                break;
                            case 2:
                                Console.WriteLine("enter id");
                                blObject.CollectParcelByDrone(int.Parse(Console.ReadLine()));
                                break;
                            case 3:
                                Console.WriteLine("enter id");
                                blObject.DeliveryOfPackageByDrone(int.Parse(Console.ReadLine()));
                                break;
                            case 4:
                                Console.WriteLine("enter id");
                                blObject.DroneToCharging(int.Parse(Console.ReadLine()));
                                break;
                            case 5:
                                Console.WriteLine("enter id, Charging time");
                                blObject.ReleaseDroneFromCharging(int.Parse(Console.ReadLine()), TimeSpan.Parse(Console.ReadLine()));
                                break;
                            case 6:
                                Console.WriteLine("enter id, model");
                                blObject.UpdateDrone(int.Parse(Console.ReadLine()),Console.ReadLine());
                                break;
                            case 7:
                                Console.WriteLine("enter id, station name, Total amount of charging stations");
                                blObject.UpdateBaseStation(int.Parse(Console.ReadLine()), Console.ReadLine(), int.Parse(Console.ReadLine()));
                                break;
                            case 8:
                                Console.WriteLine("enter id, name, phone");
                                blObject.UpdateCustomer(int.Parse(Console.ReadLine()), Console.ReadLine(),Console.ReadLine());
                                break;

                            default:
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("To view Drone press 1,");
                        Console.WriteLine("To view base station press 2,");
                        Console.WriteLine("To view a Customer press 3,");
                        Console.WriteLine("To view a package, press 4,");
                        int s = int.Parse(Console.ReadLine());
                        switch (s)
                        {
                            case 1:
                                Console.WriteLine("enter id");
                                blObject.GetDrone(int.Parse(Console.ReadLine()));
                                break;
                            case 2:
                                Console.WriteLine("enter id");
                                blObject.GetBaseStation(int.Parse(Console.ReadLine()));
                                break;
                            case 3:
                                Console.WriteLine("enter id");
                                blObject.GetCustomer(int.Parse(Console.ReadLine()));
                                break;
                            case 4:
                                Console.WriteLine("enter id");
                                blObject.GetParcel(int.Parse(Console.ReadLine()));
                                break;
                            default:
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
                        int t = 0;
                        int.TryParse(Console.ReadLine(), out t);
                        switch (t)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("have a nice day,");
                        break;
                }
            }
        }
    }
}
