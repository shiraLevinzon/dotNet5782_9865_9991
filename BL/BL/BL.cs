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
        internal static Random r1 = new Random();
        internal static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        List<DroneToList> dronesToList = new List<DroneToList>();
        public Random random = new Random();
        public double free;
        public double lightC;
        public double normalC;
        public double heavyC;
        public double droneLoadingRate;
        List<Customer> customersBL = new List<Customer>();
        List<BaseStation> baseStationsBL = new List<BaseStation>();
        #region בנאי 
        public BL() 
        {
           
            double[] arr = dalLayer.RequestPowerConsumptionByDrone();
            free = arr[0];
            lightC = arr[1];
            normalC = arr[2];
            heavyC = arr[3];
            droneLoadingRate = arr[4];
            #region מילוי רשימת הלקוחות מסוג דאל 
            List<IDAL.DO.Customer> TMPcustomer = new List<IDAL.DO.Customer>();
            TMPcustomer = dalLayer.GetAllCustomers().ToList();
            foreach (var item in TMPcustomer)
            {
                BO.Customer cust = new Customer();
                cust.ID = item.ID;
                cust.Name = item.Name;
                cust.Phone = item.Phone;
                cust.Location = new Location();
                cust.Location.Latitude = item.Latitude;
                cust.Location.Longitude = item.Longitude;
            
                customersBL.Add(cust);
            }
            #endregion
            #region מילוי רשימת רחפנים מסוג דאל
            List<IDAL.DO.Drone> TMPdrone = new List<IDAL.DO.Drone>();
            TMPdrone = dalLayer.GetAllDrones().ToList();
            foreach (var item in TMPdrone)
            {
                DroneToList dtl = new DroneToList();
                dtl.ID = item.ID; 
                dtl.MaxWeight = (WeightCategories)item.MaxWeight;
                dtl.Model = item.Model;
                dtl.BatteryStatus = 0;
                dtl.Conditions = (DroneConditions)(r1.Next(0,3));
                dtl.PackagNumberOnTransferred = 0;
                dtl.location = new Location();
                dtl.location.Latitude = TMPcustomer[r1.Next(0,9)].Latitude;
                dtl.location.Longitude = TMPcustomer[r1.Next(0,9)].Longitude; ;
                dronesToList.Add(dtl);
                
            }
            #endregion
   
            #region מילוי רשימת תחנות בסיס מסוג דאל
            List<IDAL.DO.BaseStation> TMPbaseStation = new List<IDAL.DO.BaseStation>();
            TMPbaseStation = dalLayer.GetAllBaseStations().ToList();
            foreach (var item in TMPbaseStation)
            {
                BO.BaseStation bases = new BaseStation()
                {
                    ID = item.ID,
                    StationName = item.StationName,
                    FreeChargingSlots = item.FreeChargingSlots,
                    BaseStationLocation = new Location { Latitude = item.Latitude, Longitude = item.Longitude },

                };
                baseStationsBL.Add(bases);
            }
            #endregion
            #region מילוי רשימת חבילה מסוג דאל
            List<IDAL.DO.Parcel> TMPparcel = dalLayer.GetAllParcels().ToList();//Where(par=> par.DroneId!=0).ToList();
            foreach (var item in TMPparcel)
            {
                if (item.DroneId != 0)
                {
                    DroneToList d = dronesToList.Find(dro => dro.ID == item.DroneId);
                    d.Conditions = (DroneConditions)2;
                    if (item.Requested != DateTime.MinValue)
                    {
                        if (item.Delivered == DateTime.MinValue)
                        {
                            if (item.PickedUp == DateTime.MinValue)
                            {
                                BO.BaseStation basestationHalper = new BO.BaseStation();
                                double mini = double.MaxValue;
                                foreach (var item2 in baseStationsBL)
                                {
                                    double dis = DistanceTo(item2.BaseStationLocation.Latitude, item2.BaseStationLocation.Longitude, d.location.Latitude, d.location.Longitude);
                                    if (dis < mini)
                                    {
                                        mini = dis;
                                        basestationHalper = item2;
                                    }
                                }
                                d.location = basestationHalper.BaseStationLocation;
                            }
                            else
                            {
                                d.location.Latitude = customersBL.Find(cu => cu.ID == item.SenderID).Location.Latitude;
                                d.location.Longitude = customersBL.Find(cu => cu.ID == item.SenderID).Location.Longitude;

                            }
                        }
                        //עדכון מצב בטריה
                        BO.BaseStation stationHalper = new BO.BaseStation();
                        double min = double.MaxValue;
                        foreach (var item2 in baseStationsBL)
                        {

                            double dis = DistanceTo(item2.BaseStationLocation.Latitude, item2.BaseStationLocation.Longitude, customersBL.Find(cus =>item.TargetID==cus.ID).Location.Latitude,customersBL.Find(cus =>item.TargetID==cus.ID).Location.Longitude);
                            if (dis < min)
                            {
                                min = dis;
                                stationHalper = item2;
                            }
                        }
                        double distans = DistanceTo(d.location.Latitude, d.location.Longitude,customersBL.Find(cus =>item.SenderID==cus.ID).Location.Latitude,customersBL.Find(cus =>item.SenderID==cus.ID).Location.Longitude);                   
                        distans += min + DistanceTo(customersBL.Find(cus =>item.TargetID==cus.ID).Location.Latitude,customersBL.Find(cus =>item.TargetID==cus.ID).Location.Longitude,customersBL.Find(cus =>item.SenderID==cus.ID).Location.Latitude,customersBL.Find(cus =>item.SenderID==cus.ID).Location.Longitude);
                        int i = (int)(IDAL.DO.WeightCategories)item.Weight;
                        d.BatteryStatus = random.Next((int)distans*(int)arr[i+1], 100);
                    }

                }

            }
            #endregion
            #region היית צריכה לעשות לזה אנד לכן זה עושה באאאאג
            foreach (var item in dronesToList)
            {
                if (item.Conditions != (BO.DroneConditions)2)
                {
                    item.Conditions = (BO.DroneConditions)random.Next(1, 3);
                }
                if (item.Conditions == (BO.DroneConditions)0)
                {
                    int ran = random.Next(0, baseStationsBL.Count());
                    item.location = baseStationsBL[ran].BaseStationLocation;
                    item.BatteryStatus = (random.Next(0, 21));
                }
                else
                if (item.Conditions == (BO.DroneConditions)1)
                {
                  //  int ran = random.Next(0, customersBL.FindAll(cus => cus.PackagesToCustomer.Any(par => par.Situation == (BO.Situations)3)).Count);
                  //  item.location = customersBL.FindAll(cus => cus.PackagesToCustomer.Any(par => par.Situation == (BO.Situations)3))[ran].Location;
                    //עדכון מצב בטריה
                    BO.BaseStation basestationHalper = new BO.BaseStation();
                    double mini = double.MaxValue;
                    foreach (var item2 in baseStationsBL)
                    {
                        double dis = DistanceTo(item2.BaseStationLocation.Latitude, item2.BaseStationLocation.Longitude, item.location.Latitude, item.location.Longitude);
                        if (dis < mini)
                        {
                            mini = dis;
                            basestationHalper = item2;
                        }
                    }
                    item.BatteryStatus = random.Next((int)(mini * free), 100);
                }
            }
            #endregion
        }
        #endregion
        #region פונקציית שליחת רחפן לטעינה יש צורך בבדיקה!!
        public void DroneToCharging(int id)
        {
            try
            {
                BO.DroneToList drone = dronesToList.Find(x => x.ID == id);
                if (drone.Conditions != (DroneConditions)1)
                    throw new BO.ImproperMaintenanceCondition(id, "The drone is not available");
                double distance = DistanceTo(baseStationsBL[0].BaseStationLocation.Latitude, baseStationsBL[0].BaseStationLocation.Longitude, drone.location.Longitude, drone.location.Longitude);
                int idbasetation = 0;
                foreach (var item in baseStationsBL)
                {
                    if (DistanceTo(item.BaseStationLocation.Latitude, item.BaseStationLocation.Longitude, drone.location.Latitude, drone.location.Longitude) < distance)
                    {
                        distance = DistanceTo(item.BaseStationLocation.Latitude, item.BaseStationLocation.Longitude, drone.location.Latitude, drone.location.Longitude);
                        idbasetation = item.ID;
                    }
                }
                if (free * distance > drone.BatteryStatus || GetBaseStation(idbasetation).FreeChargingSlots == 0)
                    throw new BO.ImproperMaintenanceCondition(id, "There is not enough battery to get to the station or there is no space available at the station to recharge");
                BO.BaseStation basestation = GetBaseStation(idbasetation);
                BO.DroneToList dro = dronesToList.Find(x => x.ID == drone.ID);
                dro.BatteryStatus = dro.BatteryStatus - free * distance;
                dro.location.Latitude = basestation.BaseStationLocation.Latitude;
                dro.location.Longitude = basestation.BaseStationLocation.Longitude;
                dro.Conditions = (DroneConditions)0;            
                dalLayer.SendingDroneToBaseStation(basestation.ID, dro.ID);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch (BO.ImproperMaintenanceCondition ex)
            {
                throw new BO.ImproperMaintenanceCondition(ex.ID, ex.EntityName);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        #endregion
        #region פונקציית שחרור רחפן מעמדת טעינה
        public void ReleaseDroneFromCharging(int id, TimeSpan time)
        {
            try
            {
                BO.DroneToList drone = dronesToList.Find(x => x.ID == id);
                if (drone.Conditions != (DroneConditions)0)
                    throw new BO.ImproperMaintenanceCondition(id, "The drone is not available");
                BO.DroneToList dro = dronesToList.Find(x => x.ID == id);
                IEnumerable<BO.BaseStation> baseStations = from b in GetAllBaseStation()
                                                           select GetBaseStation(b.ID);
                BO.BaseStation bases = baseStations.FirstOrDefault(bas => bas.BaseStationLocation.Latitude== dro.location.Latitude&& bas.BaseStationLocation.Longitude == dro.location.Longitude);
                bases.DronesInCharge.ToList().RemoveAll(dr => dr.ID == dro.ID);
                dro.Conditions = (DroneConditions)1;
                if (droneLoadingRate * time.TotalHours > 100)
                    dro.BatteryStatus = 100;
                else
                    dro.BatteryStatus = droneLoadingRate * time.TotalHours;
                dalLayer.ReleaseDroneFromChargingAtBaseStation(bases.ID, dro.ID);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch (BO.ImproperMaintenanceCondition ex)
            {
                throw new BO.ImproperMaintenanceCondition(ex.ID, ex.EntityName);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        #endregion
        #region פונקציית שיוך חבילה לרחפן
        //עובדת!!
        public void AssignPackageToDrone(int id)
        {
            try
            {
                BO.DroneToList drone = dronesToList.Find(x => x.ID == id);
                if (drone.Conditions != (DroneConditions)1)
                    throw new BO.ImproperMaintenanceCondition(drone.ID, "Drone Conditions stuck");
                IDAL.DO.Parcel parcel = dalLayer.GetAllParcels().ToList()[0];
                foreach (IDAL.DO.Parcel item in dalLayer.GetAllParcels())
                {
                    if (item.priority > parcel.priority)
                        parcel = item;
                    else if (item.priority == parcel.priority)
                    {
                        if (item.Weight > parcel.Weight && item.Weight <= (IDAL.DO.WeightCategories)drone.MaxWeight)
                            parcel = item;
                        else if (item.Weight == parcel.Weight)
                        {
                            double distance1 = DistanceTo(drone.location.Latitude, drone.location.Longitude, dalLayer.GetCostumer(parcel.SenderID).Latitude, dalLayer.GetCostumer(parcel.SenderID).Longitude);
                            double distance2 = DistanceTo(drone.location.Latitude, drone.location.Longitude, dalLayer.GetCostumer(item.SenderID).Latitude, dalLayer.GetCostumer(item.SenderID).Longitude);
                            if (distance2 < distance1)
                                parcel = item;
                        }
                    }
                }
                int a = (int)parcel.Weight;
                double decrease = (double)dalLayer.RequestPowerConsumptionByDrone().GetValue(a++);
                decrease = free * DistanceTo(drone.location.Latitude, drone.location.Longitude, dalLayer.GetCostumer(parcel.SenderID).Latitude, dalLayer.GetCostumer(parcel.SenderID).Longitude)
                    + decrease * DistanceTo(dalLayer.GetCostumer(parcel.SenderID).Latitude, dalLayer.GetCostumer(parcel.SenderID).Longitude, dalLayer.GetCostumer(parcel.TargetID).Latitude, dalLayer.GetCostumer(parcel.TargetID).Longitude)
                    + free * DistanceTo(dalLayer.GetCostumer(parcel.TargetID).Latitude, dalLayer.GetCostumer(parcel.TargetID).Longitude, GetBaseStation(helpbasestation(drone)).BaseStationLocation.Latitude, GetBaseStation(helpbasestation(drone)).BaseStationLocation.Longitude);
                if (decrease > drone.BatteryStatus)
                    throw new BO.ImproperMaintenanceCondition(drone.ID, "Drone's battery too low ");
                //drone.BatteryStatus -= free * DistanceTo(drone.location.Latitude, drone.location.Longitude, dalLayer.GetCostumer(parcel.SenderID).Latitude, dalLayer.GetCostumer(parcel.SenderID).Longitude);
                drone.Conditions = (DroneConditions)2;
                drone.PackagNumberOnTransferred = parcel.ID;
                dalLayer.AssignPackageToDrone(parcel.ID, drone.ID);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch (BO.ImproperMaintenanceCondition ex)
            {
                throw new BO.ImproperMaintenanceCondition(ex.ID, ex.EntityName);
            }
        }
        #endregion
        #region איסוף חבילה עי רחפן
        public void CollectParcelByDrone(int id)//לסדר פונקציה כדי שרשימת הרחפנים תתעדכן
        {
            
            try
            {
                
                BO.DroneToList droneTOlist = dronesToList.Find(x => x.ID == id);
                BO.Drone drone = GetDrone(id);
                if ((drone.Conditions != (DroneConditions)2))
                    throw new BO.TheDroneDnotShip(id,"Drone", "Drone condition is not correct");   
                drone.BatteryStatus -= free * DistanceTo(drone.location.Latitude, drone.location.Longitude, drone.PackageInTransfer.Collection.Latitude, drone.PackageInTransfer.Collection.Longitude);
                drone.location.Latitude = drone.PackageInTransfer.Collection.Latitude;
                drone.location.Longitude = drone.PackageInTransfer.Collection.Longitude;
                droneTOlist.BatteryStatus = drone.BatteryStatus;
                droneTOlist.location.Latitude = drone.location.Latitude;
                droneTOlist.location.Longitude = drone.location.Longitude;
                dalLayer.ParcelCollectionByDrone(drone.PackageInTransfer.ID, id);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }
        #endregion
        #region אספקת חבילה ע"י רחפן
        public void DeliveryOfPackageByDrone(int id)
        {
            try
            {
                BO.DroneToList drone = dronesToList.FirstOrDefault(x => x.ID == id);
                IDAL.DO.Parcel parcel = dalLayer.GetAllParcels().ToList().Find(x => x.DroneId == id);
                if (parcel.Delivered != DateTime.MinValue || parcel.PickedUp == DateTime.MinValue)
                    throw new BO.PackageTimesException(id, "Parcel can't be Delivere- Time Problem");
                IDAL.DO.Customer customer = dalLayer.GetAllCustomers().ToList().Find(x => x.ID == parcel.TargetID);
                double distance = DistanceTo(drone.location.Latitude, drone.location.Longitude, customer.Latitude, customer.Longitude);
                int a = (int)parcel.Weight;
                double decrease = (double)dalLayer.RequestPowerConsumptionByDrone().GetValue(a++);
                if (distance * decrease > drone.BatteryStatus)
                    throw new BO.PackageTimesException(drone.ID, "not enough battery left");
                drone.BatteryStatus = drone.BatteryStatus - distance * decrease;
                drone.location.Latitude = customer.Latitude;
                drone.location.Longitude = customer.Longitude;
                drone.Conditions = (DroneConditions)1;
                dalLayer.DeliveryParcelToCustomer(parcel.ID, drone.ID);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName);
            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        #endregion
        #region פונקציית עזר לחישוב מרחק
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
        #endregion
        #region פונקציית עזר למציאת תחנת בסיס קרובה
        private int helpbasestation(BO.DroneToList drone)
        {
            double distance = DistanceTo(baseStationsBL[0].BaseStationLocation.Latitude, baseStationsBL[0].BaseStationLocation.Longitude, drone.location.Longitude, drone.location.Longitude);
            int idbasetation = 0;
            foreach (var item in baseStationsBL)
            {
                if (DistanceTo(item.BaseStationLocation.Latitude, item.BaseStationLocation.Longitude, drone.location.Latitude, drone.location.Longitude) < distance)
                {
                    distance = DistanceTo(item.BaseStationLocation.Latitude, item.BaseStationLocation.Longitude, drone.location.Latitude, drone.location.Longitude);
                    idbasetation = item.ID;
                }
            }
            return idbasetation;
        }
        #endregion
    }
}
