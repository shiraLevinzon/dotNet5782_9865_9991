using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
namespace Dal
{
   

    public class DalXml:IDal
    {
        #region Singelton
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        #region DS XML Files

        string DronesPath = @"DronesXml.xml"; //XMLSerializer
        string BaseStationsPath = @"BaseStationsXml.xml"; //XElement
        string CustonersPath = @"CustomersXml.xml"; //XMLSerializer
        string ParcelsPath = @"ParcelsXml.xml"; //XMLSerializer
        string UsersPath = @"UsersXml.xml"; //XMLSerializer
        string DronesInChargePath = @"DronesInChargeXml.xml"; //XMLSerializer
        string configPath = @"configXml.xml"; //XMLSerializer


        #endregion
        static DalXml() {  }
        #endregion  

        public double[] RequestPowerConsumptionByDrone()
        {
            return XMLTools.LoadListFromXMLElement(configPath).Element("BatteryUsages").Elements()
                .Select(e => Convert.ToDouble(e.Value)).ToArray();
        }
        public int GetParcelId()
        {
            return XMLTools.LoadListFromXMLElement(configPath).Element("RowNumbers").Elements()
                .Select(X => Convert.ToInt32(X.Value)).FirstOrDefault();
        }
        public void SetParcelId(int id)
        {
            XElement pIdR = XMLTools.LoadListFromXMLElement(configPath);
            pIdR.Element("RowNumbers").Element("NewParcelId").Value = id.ToString();
            XMLTools.SaveListToXMLElement(pIdR, configPath);
        }
        #region Drone
        public void UpdDrone(Drone tmp)
        {

            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            if (!CheckDrone(tmp.ID))
                throw new MissingIdException(tmp.ID, "Drone");
            ListDrones.Remove(tmp);
            ListDrones.Add(tmp);

            XMLTools.SaveListToXMLSerializer(ListDrones, DronesPath);
        }
        public void AddDrone(Drone tmp)
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            if (CheckDrone(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "Drone");
            ListDrones.Add(tmp);
            XMLTools.SaveListToXMLSerializer(ListDrones, DronesPath);

        }
        public bool CheckDrone(int id)
        {
            List<Drone> ListDrone = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            return ListDrone.Any(par => par.ID == id && par.Deleted == false);

        }
        public void DeleteDrone(int dID)
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            int index1 = ListDrones.FindIndex(x => x.ID == dID);
            Drone cs = ListDrones[index1];
            if (cs.Deleted == true)
                throw new EntityHasBeenDeleted(dID, "This Drones has already been deleted");
            cs.Deleted = true;
            ListDrones[index1] = cs;
            XMLTools.SaveListToXMLSerializer(ListDrones, DronesPath);
        }
        public IEnumerable<Drone> GetAllDrones(Predicate<Drone> predicate = null)
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            if (predicate != null)
            {
                return from d in ListDrones
                       where predicate(d) && d.Deleted == false
                       select d;
            }
            return from d in ListDrones
                   where d.Deleted == false
                   select d;
        }
        public Drone GetDrone(int id)
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            DO.Drone drone = ListDrones.Find(p => p.ID == id);
            if (!CheckDrone(id))
                throw new MissingIdException(id, "Drone");
            else
                return drone;
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

            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(CustonersPath);
            if (!CheckCustomer(tmp.ID))
                throw new MissingIdException(tmp.ID, "Customer");
            ListCustomers.Remove(tmp);
            ListCustomers.Add(tmp);

            XMLTools.SaveListToXMLSerializer(ListCustomers, CustonersPath);
        }
        public void AddCustomer(Customer tmp)
        {
            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(CustonersPath);

            if (CheckCustomer(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "Customer");
            ListCustomers.Add(tmp);
            XMLTools.SaveListToXMLSerializer(ListCustomers, CustonersPath);

        }
        public bool CheckCustomer(int id)
        {
            List<Customer> ListCustomer = XMLTools.LoadListFromXMLSerializer<Customer>(CustonersPath);
            return ListCustomer.Any(par => par.ID == id && par.Deleted == false);

        }
        public void DeleteCustomer(int dID)
        {
            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(CustonersPath);

            int index1 = ListCustomers.FindIndex(x => x.ID == dID);
            Customer cs = ListCustomers[index1];
            if (cs.Deleted == true)
                throw new EntityHasBeenDeleted(dID, "This Customer has already been deleted");
            cs.Deleted = true;
            ListCustomers[index1] = cs;
            XMLTools.SaveListToXMLSerializer(ListCustomers, CustonersPath);
        }
        public IEnumerable<Customer> GetAllCustomers(Predicate<Customer> predicate = null)
        {
            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(CustonersPath);

            if (predicate != null)
            {
                return from d in ListCustomers
                       where predicate(d) && d.Deleted == false
                       select d;
            }
            return from d in ListCustomers
                   where d.Deleted == false
                   select d;
        }
        public Customer GetCostumer(int id)
        {
            List<Customer> ListCustomers = XMLTools.LoadListFromXMLSerializer<Customer>(CustonersPath);

            DO.Customer customer = ListCustomers.Find(p => p.ID == id);
            if (!CheckCustomer(id))
                throw new MissingIdException(id, "Customer");
            else
                return customer;
        }

        #endregion
        #region parcel
        public void UpdParcel(Parcel tmp)
        {

            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            if (!CheckParcel(tmp.ID))
                throw new MissingIdException(tmp.ID, "Parcel");
            ListParcels.Remove(tmp);
            ListParcels.Add(tmp);

            XMLTools.SaveListToXMLSerializer(ListParcels, ParcelsPath);
        }
        public void AddParcel(Parcel tmp)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            
            if (CheckParcel(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "Parcel");
            tmp.ID = GetParcelId();
            SetParcelId(tmp.ID + 1);
            tmp.Requested = DateTime.Now;
            ListParcels.Add(tmp);
            XMLTools.SaveListToXMLSerializer(ListParcels, ParcelsPath);

        }
        public bool CheckParcel(int id)
        {
            List<Parcel> ListParcel = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            return ListParcel.Any(par => par.ID == id && par.Deleted == false);

        }
        public void DeleteParcel(int dID)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);

            int index1 = ListParcels.FindIndex(x => x.ID == dID);
            Parcel cs = ListParcels[index1];
            if (cs.Deleted == true)
                throw new EntityHasBeenDeleted(dID, "This Parcel has already been deleted");
            cs.Deleted = true;
            ListParcels[index1] = cs;
            XMLTools.SaveListToXMLSerializer(ListParcels, ParcelsPath);
        }
        public IEnumerable<Parcel> GetAllParcels(Predicate<Parcel> predicate = null)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);

            if (predicate != null)
            {
                return from d in ListParcels
                       where predicate(d) && d.Deleted == false
                       select d;
            }
            return from d in ListParcels
                   where d.Deleted == false
                   select d;
        }
        public Parcel GetParcel(int id)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);

            DO.Parcel parcel = ListParcels.Find(p => p.ID == id);
            if (!CheckParcel(id))
                throw new MissingIdException(id, "Parcel");
            else
                return parcel;
        }

        #endregion
        #region DroneCharging
        public bool CheckDroneCharge(int id)
        {
            List<DroneCharge> ListDroneCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DronesInChargePath);
            return ListDroneCharge.Any(par => par.DroneID == id && par.Deleted == false);
        }
        public void DeleteDroneInCharge(int dgID)
        {
            List<DroneCharge> ListDroneCharge = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DronesInChargePath);

            int index1 = ListDroneCharge.FindIndex(x => x.DroneID == dgID && x.Deleted == false);
            DroneCharge ps = ListDroneCharge[index1];
            if (ps.Deleted == true)
                throw new EntityHasBeenDeleted(dgID, "This Drone has already been remuved");
            ps.Deleted = true;
            ListDroneCharge[index1] = ps;

            XMLTools.SaveListToXMLSerializer(ListDroneCharge, DronesInChargePath);

        }
        public IEnumerable<DroneCharge> GetAllDroneCharge(Predicate<DroneCharge> predicate = null)
        {
            List<DroneCharge> ListDrones = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DronesInChargePath);

            if (predicate != null)
            {
                return from b in ListDrones
                       where predicate(b) && b.Deleted == false
                       select b;
            }
            return from b in ListDrones
                   where b.Deleted == false
                   select b;
        }
        public DroneCharge GetDroneInCharging(int id)
        {
            List<DroneCharge> ListDrones = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DronesInChargePath);

            if (!CheckDroneCharge(id))
                throw new MissingIdException(id, "DroneCharge");
            DroneCharge d = ListDrones.FirstOrDefault(par => par.DroneID == id);
            return d;
        }
        public IEnumerable<DroneCharge> GetAllDroneCharge()
        {
            List<DroneCharge> ListDrones = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DronesInChargePath);

            return from d in ListDrones
                          where d.Deleted == false
                          select d;
        }
        #endregion
        #region User
        public void UpdUser(User tmp)
        {

            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);
            if (!CheckUser(tmp.Id))
                throw new MissingIdException(tmp.Id, "User");
            ListUsers.Remove(tmp);
            ListUsers.Add(tmp);

            XMLTools.SaveListToXMLSerializer(ListUsers, UsersPath);
        }
        public void AddUser(User tmp)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);

            if (CheckUser(tmp.Id))
                throw new DuplicateIdException(tmp.Id, "User");
            ListUsers.Add(tmp);
            XMLTools.SaveListToXMLSerializer(ListUsers, UsersPath);

        }
        public bool CheckUser(int id)
        {
            List<User> ListUser = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);
            return ListUser.Any(par => par.Id == id && par.Deleted == false);

        }
        public void DeleteUser(int dID)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);

            int index1 = ListUsers.FindIndex(x => x.Id == dID);
            User cs = ListUsers[index1];
            if (cs.Deleted == true)
                throw new EntityHasBeenDeleted(dID, "This User has already been deleted");
            cs.Deleted = true;
            ListUsers[index1] = cs;
            XMLTools.SaveListToXMLSerializer(ListUsers, UsersPath);
        }
        public IEnumerable<User> GetAllUser(Predicate<User> predicate = null)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);

            if (predicate != null)
            {
                return from d in ListUsers
                       where predicate(d) && d.Deleted == false
                       select d;
            }
            return from d in ListUsers
                   where d.Deleted == false
                   select d;
        }
        public User GetUser(int id)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(UsersPath);

            DO.User user = ListUsers.Find(p => p.Id == id);
            if (!CheckUser(id))
                throw new MissingIdException(id, "User");
            else
                return user;
        }
        #endregion
        #region UPdateFunction
        /// <summary>
        /// Assign A Package To A Drone
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void AssignPackageToDrone(int pID, int dID)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            int index1 = ListParcels.FindIndex(x => x.ID == pID && x.Deleted == false);
            int index2 = ListDrones.FindIndex(x => x.ID == dID && x.Deleted == false);

            Parcel p = ListParcels[index1];
            Drone d = ListDrones[index2];

            p.DroneId = dID;
            p.Scheduled = DateTime.Now;

            ListParcels[index1] = p;
            ListDrones[index2] = d;

            XMLTools.SaveListToXMLSerializer(ListParcels, ParcelsPath);
            XMLTools.SaveListToXMLSerializer(ListDrones, DronesPath);


        }
        /// <summary>
        /// Parcel Collection By A Drone
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void ParcelCollectionByDrone(int pID, int dID)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            int index1 = ListParcels.FindIndex(x => x.ID == pID && x.Deleted == false);
            int index2 = ListDrones.FindIndex(x => x.ID == dID && x.Deleted == false);


            Parcel p = ListParcels[index1];
            Drone d = ListDrones[index2];

            p.PickedUp = DateTime.Now;
            d.MaxWeight = p.Weight;

            ListParcels[index1] = p;
            ListDrones[index2] = d;

            XMLTools.SaveListToXMLSerializer(ListParcels, ParcelsPath);
            XMLTools.SaveListToXMLSerializer(ListDrones, DronesPath);

        }
        /// <summary>
        /// Delivery Parcel To Customer
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void DeliveryParcelToCustomer(int pID, int dID)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(ParcelsPath);
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            int index1 = ListParcels.FindIndex(x => x.ID == pID && x.Deleted == false);
            int index2 = ListDrones.FindIndex(x => x.ID == dID && x.Deleted == false);

            Parcel p = ListParcels[index1];
            Drone d = ListDrones[index2];

            p.Delivered = DateTime.Now;
            p.DroneId = 0;

            ListParcels[index1] = p;
            ListDrones[index2] = d;

            XMLTools.SaveListToXMLSerializer(ListParcels, ParcelsPath);
            XMLTools.SaveListToXMLSerializer(ListDrones, DronesPath);

        }
        /// <summary>
        /// Sending Drone To BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void SendingDroneToBaseStation(int bsID, int dID)
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(BaseStationsPath);
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DronesInChargePath);

            int index1 = ListBaseStations.FindIndex(x => x.ID == bsID && x.Deleted == false);
            int index2 = ListDrones.FindIndex(x => x.ID == dID && x.Deleted == false);

            BaseStation bs = ListBaseStations[index1];
            Drone d = ListDrones[index2];
            bs.FreeChargingSlots--;

            ListBaseStations[index1] = bs;
            ListDrones[index2] = d;

            DroneCharge dc = new DroneCharge();
            dc.DroneID = dID;
            dc.StationID = bsID;
            ListDroneCharges.Add(dc);

            XMLTools.SaveListToXMLSerializer(ListBaseStations, BaseStationsPath);
            XMLTools.SaveListToXMLSerializer(ListDrones, DronesPath);
            XMLTools.SaveListToXMLSerializer(ListDroneCharges, DronesInChargePath);

        }
        /// <summary>
        /// Release Drone From Charging At BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void ReleaseDroneFromChargingAtBaseStation(int bsID, int dID)
        {
            List<BaseStation> ListBaseStations = XMLTools.LoadListFromXMLSerializer<BaseStation>(BaseStationsPath);
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            List<DroneCharge> ListDroneCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(DronesInChargePath);

            int index1 = ListBaseStations.FindIndex(x => x.ID == bsID && x.Deleted == false);
            int index2 = ListDrones.FindIndex(x => x.ID == dID && x.Deleted == false);
            int index3 = ListDroneCharges.FindIndex(x => x.DroneID == dID && x.Deleted == false);

            BaseStation bs = ListBaseStations[index1];
            Drone d = ListDrones[index2];
            bs.FreeChargingSlots++;

            ListBaseStations[index1] = bs;
            ListDrones[index2] = d;
            //DeleteDroneInCharge(dID);
            ListDroneCharges.RemoveAt(index3);

            XMLTools.SaveListToXMLSerializer(ListBaseStations, BaseStationsPath);
            XMLTools.SaveListToXMLSerializer(ListDrones, DronesPath);
            XMLTools.SaveListToXMLSerializer(ListDroneCharges, DronesInChargePath);

        }
        #endregion
        #region Help Functions
        public double Deg2rad(double deg)
        {
            throw new NotImplementedException();
        }
       
        #endregion
    }
}
