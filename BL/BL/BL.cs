using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL;
using DalObject;
namespace IBL.BL
{
    public partial class BL : IBL
    {
        IDAL.IDal dalLayer = new DalObject.DalObject();
        List<DroneToList> dronesToList;
        private readonly DroneConditions delivery;

        public Random random = new Random();

        public double free;
        public double light;
        public double normal;
        public double heavy;
        public double droneLoadingRate;
        public BL() 
        {
            dronesToList = new List<DroneToList>();
            List<Customer> customersBL = new List<Customer>();
            List<BaseStation> baseStationsBL = new List<BaseStation>();


            double[] arr = dalLayer.RequestPowerConsumptionByDrone();
            free = arr[0];
            light = arr[1];
            normal = arr[2];
            heavy = arr[3];
            droneLoadingRate =arr[4];
            //List<Drone> TMPdrone = new List<Drone>();
            //List<Parcel> TMPparcel = new List<Parcel>();
            List<IDAL.DO.Drone> TMPdrone = new List<IDAL.DO.Drone>();
            TMPdrone = dalLayer.printDrone().ToList();
            foreach (var item in TMPdrone)
            {
                dronesToList.Add(new DroneToList
                {
                    ID = item.ID,
                    MaxWeight=(WeightCategories)item.MaxWeight,
                    Model= item.Model
                });
            }
            List<IDAL.DO.Customer> TMPcustomer = new List<IDAL.DO.Customer>();
            TMPcustomer = dalLayer.printCustomer().ToList();
            foreach (var item in TMPcustomer)
            {
                customersBL.Add(new Customer
                {
                    ID = item.ID,
                    Phone=item.Phone,
                    Name=item.Name,
                    Location = new Location { Latitude=item.Latitude,Longitude=item.Longitude},

                }) ;
            }
             List<IDAL.DO.BaseStation> TMPbaseStation = new List<IDAL.DO.BaseStation>();
            TMPbaseStation = dalLayer.printBaseStation().ToList();
            foreach (var item in TMPbaseStation)
            {
                baseStationsBL.Add(new BaseStation
                {
                    ID = item.ID,
                    StationName = item.StationName,
                    FreeChargingSlots=item.FreeChargingSlots,
                   BaseStationLocation = new Location { Latitude = item.Latitude, Longitude = item.Longitude },

                }) ;
            }
            List<IDAL.DO.Parcel> TMPparcel = dalLayer.printParcel().ToList();//Where(par=> par.DroneId!=0).ToList();
            foreach (var item in TMPparcel)
            {
                if(item.DroneId!=0)
                {
                    DroneToList d = dronesToList.Find(dro => dro.ID == item.DroneId);
                    if(item.Requested!= DateTime.MinValue)
                    {
                       if (item.Delivered == DateTime.MinValue)
                       {                    
                           d.Conditions = (DroneConditions)2;
                            if (item.PickedUp == DateTime.MinValue)
                            {
                                BO.BaseStation stationHalper = new BO.BaseStation();
                                double min = double.MaxValue;
                                foreach (var item2 in baseStationsBL)
                                {
                                    double dis = dalLayer.distanceCal(item2.BaseStationLocation.Latitude, item2.BaseStationLocation.Longitude, d.location.Latitude, d.location.Longitude);
                                    if (dis < min)
                                    {
                                        min = dis;
                                        stationHalper = item2;
                                    }
                                }
                                d.location = stationHalper.BaseStationLocation;
                            }
                            else
                            {
                                d.location.Latitude = customersBL.Find(cu => cu.ID == item.SenderID).Location.Latitude;
                                d.location.Longitude = customersBL.Find(cu => cu.ID == item.SenderID).Location.Longitude;
                            }
                       }
                       
                    }
                }

            }
            foreach (var item in dronesToList)
            {
                if (item.Conditions != (BO.DroneConditions)2)
                {
                    item.Conditions = (BO.DroneConditions)random.Next(1, 3);
                }
                if(item.Conditions==(BO.DroneConditions)0)
                {
                    int ran = random.Next(0, baseStationsBL.Count());
                    item.location = baseStationsBL[ran].BaseStationLocation;
                    item.BatteryStatus = (random.Next(0, 21)) % 100;
                }
                else
                if(item.Conditions == (BO.DroneConditions)1)
                {
                  int ran= random.Next(0,  customersBL.FindAll(cus => cus.PackagesToCustomer.Any(par => par.Situation == (BO.Situations)3)).Count);
                    item.location = customersBL.FindAll(cus => cus.PackagesToCustomer.Any(par => par.Situation == (BO.Situations)3))[ran].Location;
                    //להוסיף עדכון של מצב סוללה לפי הבקשות המטומטמות בתרגיל
                }
            }
            //foreach (var parcel in dalLayer.printParcel())
            //{



            //    TMPparcel[i].Delivered= parcel.Delivered;
            //    TMPparcel[i].ID = parcel.ID;
            //    TMPparcel[i].PickedUp = parcel.PickedUp;
            //    TMPparcel[i].Priority = (Priorities)parcel.priority;
            //    TMPparcel[i].Requested = parcel.Requested;
            //    TMPparcel[i].Scheduled = parcel.Scheduled;
            //    TMPparcel[i].Weight = (WeightCategories)parcel.Weight;
            //    i++;
            //}
            //i = 0;
            //foreach(var drone in dalLayer.printDrone())
            //{
            //    TMPdrone[i].BatteryStatus = drone.BatteryStatus;
            //    TMPdrone[i].ID = drone.ID;
            //    TMPdrone[i].Model = drone.Model;
            //    TMPdrone[i].MaxWeight = (WeightCategories)drone.MaxWeight;
            //    i++;
            //}
            /*foreach (var drone in dalLayer.printDrone())
            {
                int index = TMPparcel.FindIndex(x=>x.DroneId == drone.ID);
                int index1 = dronesToList.FindIndex(x => x.ID == drone.ID);
                if (TMPparcel[index].PickedUp > DateTime.Now) 
                {
                    Drone p = drone;
                    p.Conditions = delivery;
                    TMPdrone[index1] = p;
                }
            }*/
        }
       
    }
}