using System;
using IDAL.DO;
namespace ConsoleUI
{
    class Program
    { 
        static void Main(string[] args)
        {
            string a;
            Console.WriteLine("What do you want to add? to finish enter end");
            a = Console.ReadLine();
            while (a!="end")
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
                        tmp.MaxWeight = Console.ReadLine();
                        Console.WriteLine("Enter Drone loading status");
                        tmp.BatteryStatus = Console.Read();
                        Console.WriteLine("Insert the Drone condition(Enter 0 if the glider is available 1 if the glider is in maintenance or 2 if the glider is on delivery)");
                        tmp.DroneCondition = Console.Read();
                        AddDrone(tmp);
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
                        AddBaseStation(tmp3);
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
                        AddCustomer(tmp1);
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
                        tmp2.Weight = Console.Read();
                        Console.WriteLine("Enter Priority Level (Low, Normal, High)");
                        tmp2.priority = Console.Read();
                        Console.WriteLine("Enter Operation Drone ID (0 if not assigned)");
                        tmp2.DroneId = Console.Read();
                        // tmp.Requested = DateTime.Now;
                        Console.WriteLine("Enter the time to assign the package to the Drone");
                        Console.WriteLine("Enter a package pick-up time from the sender");
                        Console.WriteLine("Enter the time of arrival of the package to the recipient");
                        tmp2.Scheduled = DateTime.Now;
                        AddParcel(tmp2);
                        break;
                    default:
                        Console.WriteLine("Option not found Please re-enter given");
                        a = Console.ReadLine();
                        break;
                }
                a = Console.ReadLine();
            }
        }
    }
}
