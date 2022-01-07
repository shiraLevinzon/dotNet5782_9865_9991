using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
namespace Dal
{
     partial class DalObject : DalApi.IDal
    {
        public BaseStation GetBaseStation(int id)
        {

            BaseStation b = DataSource.baseStations.FirstOrDefault(par => par.ID == id);
            if (!CheckCustomer(id) && b.Deleted == (Deleted)0)
                throw new MissingIdException(id, "Customer");
            if (!CheckCustomer(id) && b.Deleted == (Deleted)2)
                throw new EntityHasBeenDeleted(id, "A customer no longer exists in the system");
            return b;
        }

        public bool CheckBaseStation(int id)
        {
            return DataSource.baseStations.Any(par => par.ID == id && par.Deleted == (Deleted)1);
        }
        public void UpdBaseStation(BaseStation tmp)
        {
            int count = DataSource.baseStations.RemoveAll(par => tmp.ID == par.ID && par.Deleted== (Deleted)1);

            if (count == 0)
                throw new MissingIdException(tmp.ID, "BaseStation");
            DataSource.baseStations.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddBaseStation(BaseStation tmp)
        {
            if (CheckBaseStation(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "BaseStation");
            DataSource.baseStations.Add(tmp);
        }
        public IEnumerable<BaseStation> GetAllBaseStations(Predicate<BaseStation> predicate=null)
        {
            if (predicate != null)
            {
                return from b in DataSource.baseStations
                       where predicate(b) && b.Deleted== (Deleted)1
                       select b;
            }
            return from b in DataSource.baseStations
                   where b.Deleted== (Deleted)1
                   select b;
        }
        /// <summary>
        /// Sending Drone To BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void SendingDroneToBaseStation(int bsID, int dID)
        {
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID && x.Deleted== (Deleted)1);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID && x.Deleted == (Deleted)1);

            BaseStation bs = DataSource.baseStations[index1];
            Drone d = DataSource.drones[index2];
            bs.FreeChargingSlots--;
            DataSource.baseStations[index1] = bs;
            DataSource.drones[index2] = d;
            DroneCharge dc = new DroneCharge();
            dc.DroneID = dID;
            dc.StationID = bsID;
            DataSource.droneCharges.Add(dc);
        }
        /// <summary>
        /// Release Drone From Charging At BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void ReleaseDroneFromChargingAtBaseStation(int bsID, int dID)
        {
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID && x.Deleted== (Deleted)1);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID && x.Deleted == (Deleted)1);
            int index3 = DataSource.droneCharges.FindIndex(x => x.DroneID == dID && x.Deleted == (Deleted)1);
            BaseStation bs = DataSource.baseStations[index1];
            Drone d = DataSource.drones[index2];
            bs.FreeChargingSlots++;
            DataSource.baseStations[index1] = bs;
            DataSource.drones[index2] = d;
             DataSource.droneCharges.RemoveAt(index3);
        }
        public void DeleteBaseStatin(int bsID)
        {
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID);
            BaseStation bs = DataSource.baseStations[index1];
            if (bs.Deleted == (Deleted)2)
                throw new EntityHasBeenDeleted(bsID, "This base station has already been deleted");
            bs.Deleted = (Deleted)2;
            DataSource.baseStations[index1] = bs;
        }
    }
}
