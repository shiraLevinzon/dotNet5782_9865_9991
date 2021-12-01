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
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("To Assign A Package To Drone press 1,");
                        Console.WriteLine("To Parcel Collection By Drone press 2,");
                        Console.WriteLine("To Delivery Parcel To  Customer press 3,");
                        Console.WriteLine("To Sending Drone To Base Station, press 4,");
                        Console.WriteLine("To Release Drone From Charging At Base Station, press 5,");
                        int u = int.Parse(Console.ReadLine());
                        switch (u)
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
                            default:
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("To view Drone press 1,");
                        Console.WriteLine("To view base station press 2,");
                        Console.WriteLine("To view a client press 3,");
                        Console.WriteLine("To view a package, press 4,");
                        int s = int.Parse(Console.ReadLine());
                        switch (s)
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
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
