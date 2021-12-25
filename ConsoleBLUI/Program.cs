using System;
using IBL;
using IBL.BL;
using IBL.BO;
using System.Collections.Generic;
namespace ConsoleBLUI
{
    class Program
    {
        
        
            public static void Main(string[] args)
            {
            
                IBL.IBL blObject = new BL();
                    int i = 0;
            while (i != 5)
            {
                Console.WriteLine("To add an option, press 1,");
                Console.WriteLine("For the Update option, press 2,");
                Console.WriteLine("For display option press 3,");
                Console.WriteLine("To view the inventory option, press 4,");
                Console.WriteLine("To exit, press 5,");
                i = int.Parse(Console.ReadLine());
                try
                {
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
                                    Drone drone = new Drone();
                                    Console.WriteLine("enter id");
                                    drone.ID = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter model");
                                    drone.Model = Console.ReadLine();
                                    Console.WriteLine("enter max wight (0-light, 1-normal,2-heavy)");
                                    drone.MaxWeight = (WeightCategories)int.Parse(Console.ReadLine());
                                    drone.location = new Location { };
                                    drone.PackageInTransfer = new ParcelInTransfer { };
                                    Console.WriteLine("enter base station id to put the drone to the first charge ");
                                    try
                                    {
                                        blObject.AddDrone(drone, int.Parse(Console.ReadLine()));

                                    }
                                    catch (IBL.BO.DuplicateIdException)
                                    {
                                        Console.WriteLine("this id already exist in the program");
                                    }
                                    catch (IBL.BO.MissingIdException)
                                    {
                                        Console.WriteLine("this id station dont exist");
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("error");
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("enter id");
                                    BaseStation baseStation = new BaseStation();
                                    baseStation.ID = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter station name");
                                    baseStation.StationName = Console.ReadLine();
                                    Console.WriteLine("enter location- latitude and location- longtitude");
                                    baseStation.BaseStationLocation = new Location { Latitude = double.Parse(Console.ReadLine()), Longitude = double.Parse(Console.ReadLine()) };
                                    Console.WriteLine("enter number of free slots charge");
                                    baseStation.FreeChargingSlots = int.Parse(Console.ReadLine());
                                    baseStation.DronesInCharge = new List<DroneInCharging>();
                                    blObject.AddBaseStation(baseStation);
                                    break;
                                case 3:
                                    Console.WriteLine("enter id");
                                    Customer customer = new Customer();
                                    customer.ID = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter name");
                                    customer.Name = Console.ReadLine();
                                    Console.WriteLine("enter phone");
                                    customer.Phone = Console.ReadLine();
                                    Console.WriteLine("enter  latitude");
                                    Console.WriteLine("enter location-longtitude");
                                    customer.Location = new Location { Latitude = double.Parse(Console.ReadLine()), Longitude = double.Parse(Console.ReadLine()) };
                                    customer.PackagesFromCustomer = new List<ParcelAtCustomer>();
                                    customer.PackagesToCustomer = new List<ParcelAtCustomer>();
                                    blObject.AddCustomer(customer);
                                    break;
                                case 4:
                                    Console.WriteLine("enter sender id");
                                    Parcel parcel = new Parcel();
                                    parcel.Sender = new CustomerInParcel();
                                    parcel.Receiver = new CustomerInParcel();
                                    parcel.Sender.ID = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter reciver id");
                                    parcel.Receiver.ID = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter  wight (0-light, 1-normal, 2-heavy)");
                                    parcel.Weight = (WeightCategories)int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter Priority (0-low, 1-normal, 2-hight)");
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
                                    blObject.UpdateDrone(int.Parse(Console.ReadLine()), Console.ReadLine());
                                    break;
                                case 7:
                                    Console.WriteLine("enter id, station name, Total amount of charging stations");
                                    blObject.UpdateBaseStation(int.Parse(Console.ReadLine()), Console.ReadLine(), int.Parse(Console.ReadLine()));
                                    break;
                                case 8:
                                    Console.WriteLine("enter id, name, phone");
                                    blObject.UpdateCustomer(int.Parse(Console.ReadLine()), Console.ReadLine(), Console.ReadLine());
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
                                    Console.WriteLine(blObject.GetDrone(int.Parse(Console.ReadLine())).ToString());


                                    break;
                                case 2:
                                    Console.WriteLine("enter id");

                                    IBL.BO.BaseStation b = blObject.GetBaseStation(int.Parse(Console.ReadLine()));
                                    Console.WriteLine(b.ToString());
                                    foreach (var item in b.DronesInCharge)
                                    {
                                        Console.WriteLine(item.ToString());
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("enter id");
                                    IBL.BO.Customer c = blObject.GetCustomer(int.Parse(Console.ReadLine()));
                                    Console.WriteLine(c.ToString());
                                    foreach (var item in c.PackagesFromCustomer)
                                    {
                                        Console.WriteLine(item.ToString());
                                    }
                                    foreach (var item in c.PackagesToCustomer)
                                    {
                                        Console.WriteLine(item.ToString());
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("enter id");
                                    Console.WriteLine(blObject.GetParcel(int.Parse(Console.ReadLine())).ToString());
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
                                    foreach (var item in blObject.GetAllBaseStation())
                                    {
                                        Console.WriteLine(item.ToString());
                                        Console.WriteLine();
                                    }
                                    break;
                                case 2:
                                    foreach (var item in blObject.GetAllDrones())
                                    {
                                        Console.WriteLine(item.ToString());
                                        Console.WriteLine();
                                    }
                                    break;
                                case 3:
                                    foreach (var item in blObject.GetAllCustomer())
                                    {
                                        Console.WriteLine(item.ToString());
                                        Console.WriteLine();
                                    }
                                    break;
                                case 4:
                                    foreach (var item in blObject.GetAllParcels())
                                    {
                                        Console.WriteLine(item.ToString());
                                        Console.WriteLine();
                                    }
                                    break;
                                case 5:
                                    foreach (var item in blObject.GetAllParcels(par => par.ParcelCondition == (Situations)0))
                                    {
                                        Console.WriteLine(item.ToString());
                                        Console.WriteLine();
                                    }
                                    break;
                                case 6:
                                    foreach (var item in blObject.GetAllBaseStation(bs => bs.FreeChargingSlots > 0))
                                    {
                                        Console.WriteLine(item.ToString());
                                        Console.WriteLine();
                                    }
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
                catch (IBL.BO.MissingIdException ex)
                {
                    Console.WriteLine("{0} id is not exist", ex.EntityName);
                }
                catch (IBL.BO.DuplicateIdException ex)
                {
                    Console.WriteLine("{0} id is already exist", ex.EntityName);

                }
                catch (IBL.BO.ImproperMaintenanceCondition ex)
                {

                    Console.WriteLine(ex.EntityName);
                }
            }
         }
        
    }
}
