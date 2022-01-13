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

            if (!CheckBaseStation(id))
                throw new MissingIdException(id, "Base Station does not exist in the system");
            BaseStation b = DataSource.baseStations.FirstOrDefault(par => par.ID == id);
            return b;
        }

        public bool CheckBaseStation(int id)
        {
            return DataSource.baseStations.Any(par => par.ID == id && par.Deleted == false);
        }
        public void UpdBaseStation(BaseStation tmp)
        {
            int count = DataSource.baseStations.RemoveAll(par => tmp.ID == par.ID && par.Deleted== false);

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
                       where predicate(b) && b.Deleted== false
                       select b;
            }
            return from b in DataSource.baseStations
                   where b.Deleted== false
                   select b;
        }
        /// <summary>
        /// Sending Drone To BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void SendingDroneToBaseStation(int bsID, int dID)
        {
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID && x.Deleted== false);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID && x.Deleted == false);

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
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID && x.Deleted== false);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID && x.Deleted == false);
            int index3 = DataSource.droneCharges.FindIndex(x => x.DroneID == dID && x.Deleted == false);
            BaseStation bs = DataSource.baseStations[index1];
            Drone d = DataSource.drones[index2];
            bs.FreeChargingSlots++;
            DataSource.baseStations[index1] = bs;
            DataSource.drones[index2] = d;
            //DeleteDroneInCharge(dID);
            DataSource.droneCharges.RemoveAt(index3);
        }
        public void DeleteBaseStatin(int bsID)
        {
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID);
            BaseStation bs = DataSource.baseStations[index1];
            if (bs.Deleted == true)
                throw new EntityHasBeenDeleted(bsID, "This base station has already been deleted");
            bs.Deleted = true;
            DataSource.baseStations[index1] = bs;
        }
    }
}
