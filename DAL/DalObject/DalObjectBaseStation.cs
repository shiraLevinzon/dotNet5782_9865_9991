using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL;
using IDAL.DO;
namespace DalObject
{
    public partial class DalObject : IDal
    {
        public BaseStation GetBaseStation(int id)
        {
            if (!CheckBaseStation(id))
                throw new MissingIdException(id, "BaseStation");

            BaseStation d = DataSource.baseStations.Find(par => par.ID == id);
            return d;
        }
        public bool CheckBaseStation(int id)
        {
            return DataSource.baseStations.Any(par => par.ID == id);
        }
        public void UpdBaseStation(BaseStation tmp)
        {
            int count = DataSource.baseStations.RemoveAll(par => tmp.ID == par.ID);

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
        public IEnumerable<BaseStation> printBaseStation()
        {
            return DataSource.baseStations.Take(DataSource.baseStations.Count);
        }
        /// <summary>
        /// Sending Drone To BaseStation
        /// </summary>
        /// <param name="bsID"></param>
        /// <param name="dID"></param>
        public void SendingDroneToBaseStation(int bsID, int dID)
        {
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);

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
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);
            int index3 = DataSource.droneCharges.FindIndex(x => x.DroneID == dID);
            BaseStation bs = DataSource.baseStations[index1];
            Drone d = DataSource.drones[index2];
            bs.FreeChargingSlots++;

            DataSource.baseStations[index1] = bs;
            DataSource.drones[index2] = d;
            DataSource.droneCharges.RemoveAt(index3);
        }
    }
}
