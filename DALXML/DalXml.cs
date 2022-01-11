using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
namespace DalXml
{
    sealed class DalXml:IDal
    {
        #region Singelton
        static readonly IDal intance = new DalXml();
        public static IDal Intance { get => intance; }
        #region DS XML Files

        string DronesPath = @"DronesXml.xml"; //XMLSerializer

        string BaseStationsPath = @"BaseStationsXml.xml"; //XElement
        string CustonersPath = @"CustomersXml.xml"; //XMLSerializer
        string ParcelsPath = @"ParcelsXml.xml"; //XMLSerializer
        string UsersPath = @"UsersXml.xml"; //XMLSerializer
        string DronesInChargePath = @"DronesInChargeXml.xml"; //XMLSerializer


        #endregion
        DalXml() { }
        #endregion  
        #region Drone
        public void UpdDrone(Drone tmp)
        {
            throw new NotImplementedException();
        }
        public void AddDrone(Drone tmp)
        {
            throw new NotImplementedException();
        }
        public bool CheckDrone(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteDrone(int dID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Drone> GetAllDrones(Predicate<Drone> predicate = null)
        {
            throw new NotImplementedException();
        }
        public Drone GetDrone(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region BaseStation
        
        public bool CheckBaseStation(int id)
        {
            XElement BaseStationRootElem = XMLTools.LoadListFromXMLElement(BaseStationsPath);
            return (from bs in BaseStationRootElem.Elements()
                    where int.Parse(bs.Element("ID").Value) == id && bool.Parse(bs.Element("Deleted").Value) != true
                    select bs).Any();
        }
        public IEnumerable<DO.BaseStation> GetAllBaseStations(Predicate<BaseStation> predicate = null)
        {
            XElement BaseStationRootElem = XMLTools.LoadListFromXMLElement(BaseStationsPath);
            IEnumerable<DO.BaseStation> bases =(from bs in BaseStationRootElem.Elements()
                    select new BaseStation()
                    {
                        ID = Int32.Parse(bs.Element("ID").Value),
                        StationName = bs.Element("StationName").Value,
                        FreeChargingSlots = Int32.Parse(bs.Element("FreeChargingSlots").Value),
                        Deleted=bool.Parse(bs.Element("Deleted").Value),
                        Latitude=double.Parse(bs.Element("Latitude").Value),
                        Longitude=double.Parse(bs.Element("Longitude").Value),
                    }
                   );
            if (predicate != null)
            {
                return from b in bases
                       where predicate(b) && b.Deleted == false
                       select b;
            }
            return from b in bases
                   where b.Deleted == false
                   select b;
        }
        public BaseStation GetBaseStation(int id)
        {
            XElement BaseStationRootElem = XMLTools.LoadListFromXMLElement(BaseStationsPath);
            BaseStation bases= (from bs in BaseStationRootElem.Elements()
                                where int.Parse(bs.Element("ID").Value) == id && bool.Parse(bs.Element("Deleted").Value) != true
                                select new BaseStation()
                                {
                                    ID = Int32.Parse(bs.Element("ID").Value),
                                    StationName = bs.Element("StationName").Value,
                                    FreeChargingSlots = Int32.Parse(bs.Element("FreeChargingSlots").Value),
                                    Deleted = bool.Parse(bs.Element("Deleted").Value),
                                    Latitude = double.Parse(bs.Element("Latitude").Value),
                                    Longitude = double.Parse(bs.Element("Longitude").Value),
                                }
                        ).FirstOrDefault();
            if (bases.ID!=id && bases.Deleted==false )
                throw new DO.MissingIdException(id, $"bad Baes Station id: {id}");
            else if(bases.Deleted==true)
                throw new DO.EntityHasBeenDeleted(id, $"the Baes Station {id} is already been deleted");
            return bases;
        }
        public void UpdBaseStation(BaseStation tmp)
        {
            XElement BaseStationRootElem = XMLTools.LoadListFromXMLElement(BaseStationsPath);
            XElement bases = (XElement)(from p in BaseStationRootElem.Elements()
                                        where int.Parse(p.Element("ID").Value) == tmp.ID && bool.Parse(p.Element("Deleted").Value) != true
                                        select p).FirstOrDefault();
            if(bases!=null)
            {
                bases.Element("ID").Value = tmp.ID.ToString();
                bases.Element("StationName").Value = tmp.StationName;
                bases.Element("FreeChargingSlots").Value = tmp.FreeChargingSlots.ToString();
                bases.Element("Deleted").Value = tmp.Deleted.ToString();
                bases.Element("Longitude").Value = tmp.Longitude.ToString();
                bases.Element("Latitude").Value = tmp.Latitude.ToString();
                XMLTools.SaveListToXMLElement(BaseStationRootElem, BaseStationsPath);
            }
            else
                throw new DO.MissingIdException(tmp.ID, $"bad Baes Station id: {tmp.ID}");
        }
        public void DeleteBaseStatin(int bsID)
        {
            XElement BaseStationRootElem = XMLTools.LoadListFromXMLElement(BaseStationsPath);
            XElement bases = (XElement)(from p in BaseStationRootElem.Elements()
                                        where int.Parse(p.Element("ID").Value) == bsID 
                                        select p).FirstOrDefault();
            if(bases.Element("ID").Value!=bsID.ToString())
                throw new DO.MissingIdException(bsID,$"there is no base station with such ID: {bsID}");
            if (bases.Element("Deleted").Value == true.ToString())
                throw new DO.EntityHasBeenDeleted(bsID, "the base station is already been deleted");
            bases.Element("Deleted").Value = true.ToString();
        }
        public void AddBaseStation(BaseStation tmp)
        {
            XElement BaseStationRootElem = XMLTools.LoadListFromXMLElement(BaseStationsPath);
            XElement bases = (XElement)(from p in BaseStationRootElem.Elements()
                                        where int.Parse(p.Element("ID").Value) == tmp.ID && p.Element("Deleted").Value==false.ToString()
                                        select p).FirstOrDefault();
            if (bases != null)
                throw new DO.DuplicateIdException(tmp.ID,$"There is already a Base Station with ID :{tmp.ID}");
            bases = new XElement("BaseStation", new XElement("ID", tmp.ID.ToString()),
                    new XElement("StationName", tmp.StationName),
                    new XElement("FreeChargingSlots", tmp.FreeChargingSlots.ToString()),
                    new XElement("Longitude", tmp.Longitude.ToString()),
                    new XElement("Latitude", tmp.Latitude.ToString()),
                    new XElement("Deleted", tmp.Deleted.ToString()));
            BaseStationRootElem.Add(bases);
            XMLTools.SaveListToXMLElement(BaseStationRootElem, BaseStationsPath);
        }
        #endregion
        #region Customer
        public void UpdCustomer(Customer tmp)
        {
            throw new NotImplementedException();
        }
        public void AddCustomer(Customer tmp)
        {
            throw new NotImplementedException();
        }
        public bool CheckCustomer(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteCustomer(int csID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Customer> GetAllCustomers(Predicate<Customer> predicate = null)
        {
            throw new NotImplementedException();
        }
        public Customer GetCostumer(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region parcel
        public void AddParcel(Parcel tmp)
        {
            throw new NotImplementedException();
        }
        public bool CheckParcel(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteParcel(int pID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Parcel> GetAllParcels(Predicate<Parcel> predicate = null)
        {
            throw new NotImplementedException();
        }
        public Parcel GetParcel(int id)
        {
            throw new NotImplementedException();
        }
        public void UpdParcel(Parcel tmp)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region DroneCharging
        public bool CheckDroneCharge(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteDroneInCharge(int dgID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DroneCharge> GetAllDroneCharge(Predicate<DroneCharge> predicate = null)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DroneCharge> GetAllDroneCharge()
        {
            throw new NotImplementedException();
        }
        public DroneCharge GetDroneInCharging(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region User
        public void AddUser(User tmp)
        {
            throw new NotImplementedException();
        }
        public bool CheckUser(int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteUser(int uID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAllUser(Predicate<User> predicate = null)
        {
            throw new NotImplementedException();
        }
        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }
        public void UpdUser(User tmp)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region UPdateFunction
        public void AssignPackageToDrone(int pID, int dID)
        {
            throw new NotImplementedException();
        }
        public void DeliveryParcelToCustomer(int pID, int dID)
        {
            throw new NotImplementedException();
        }
        public void ParcelCollectionByDrone(int pID, int dID)
        {
            throw new NotImplementedException();
        }
        public void ReleaseDroneFromChargingAtBaseStation(int bsID, int dID)
        {
            throw new NotImplementedException();
        }
        public void SendingDroneToBaseStation(int bsID, int dID)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Help Functions
        public double Deg2rad(double deg)
        {
            throw new NotImplementedException();
        }
        public double[] RequestPowerConsumptionByDrone()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
