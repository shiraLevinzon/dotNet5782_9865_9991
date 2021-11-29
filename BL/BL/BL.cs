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
        List<DroneToList> dronesToList= new List<DroneToList>();;
        private readonly DroneConditions delivery;
        public Random random = new Random();
        public double free;
        public double light;
        public double normal;
        public double heavy;
        public double droneLoadingRate;
        List<Customer> customersBL = new List<Customer>();
        List<BaseStation> baseStationsBL = new List<BaseStation>();
        public BL() 
        {
            
            double[] arr = dalLayer.RequestPowerConsumptionByDrone();
            free = arr[0];
            light = arr[1];
            normal = arr[2];
            heavy = arr[3];
            droneLoadingRate =arr[4];
            #region מילוי רשימת רחפנים מסוג דאל
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
            #endregion
            #region מילוי רשימת הלקוחות מסוג דאל 
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
            #endregion
            #region מילוי רשימת תחנות בסיס מסוג דאל
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
            #endregion
            #region מילוי רשימת חבילה מסוג דאל
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
            #endregion
            #region אין לי מושג מה זה 
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
                    //להוסיף עדכון של מצב סוללה לפי הבקשות המטומטמות(חחחחח גדול!!) בתרגיל
                }
            }
        }
        public void DroneToCharging(int id)
        {
            BO.Drone drone = GetDrone(id);
            if (drone.Conditions != (DroneConditions)1)
                throw new BO.ImproperMaintenanceCondition(id, "DroneConditions stuck");
            double distance = DistanceTo(baseStationsBL[0].BaseStationLocation.Latitude, baseStationsBL[0].BaseStationLocation.Longitude, drone.location.Longitude, drone.location.Longitude);
            int idbasetation = 0;
            foreach (var item in baseStationsBL)
            {
                if (DistanceTo(item.BaseStationLocation.Latitude,item.BaseStationLocation.Longitude,drone.location.Latitude,drone.location.Longitude)< distance)
                {
                    distance= DistanceTo(item.BaseStationLocation.Latitude,item.BaseStationLocation.Longitude,drone.location.Latitude,drone.location.Longitude);
                } 
            }
            if(free* distance>drone.BatteryStatus)
                 throw new BO.ImproperMaintenanceCondition(id, "Drone Battery low");
            if(GetBaseStation(idbasetation).FreeChargingSlots==0)
                throw new BO.ImproperMaintenanceCondition(id, "no free charging slot left");
            //עדכון כמות הטעינות עבור תחנת הבסיס אליה נשלח הרחפן 
            IDAL.DO.BaseStation basestation = dalLayer.GetBaseStation(idbasetation);
            basestation.FreeChargingSlots--;
            dalLayer.UpdBaseStation(basestation);
            //עדכון שדות הרחפן בהתאם למצב בחדש
            drone.location.Latitude = basestation.Latitude;
            drone.location.Longitude = basestation.Longitude;
            drone.Conditions = (DroneConditions)0;
            drone.BatteryStatus = drone.BatteryStatus - (free * distance);
            //צריך עבור על הפונקציה הזאת לא בטוח שהיא מתפקדת באמת כמו שצריך
        }
        public void ReleaseDroneFromCharging(int id,DateTime time)
        {
            BO.Drone drone = GetDrone(id);
            if(drone.Conditions!=(DroneConditions)0)
                throw new BO.ImproperMaintenanceCondition(id, "DroneConditions stuck");
            drone.Conditions = (DroneConditions)1;

        }
        public void AssignPackageToDrone(int id)
        {

        }
        private double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 1.609344;
        }
    }
}