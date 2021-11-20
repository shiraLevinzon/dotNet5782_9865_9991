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
    class DalObjectBaseStation 
    {
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddBaseStation(BaseStation tmp)
        {
            DataSource.baseStations.Add(tmp);
        }
        /// <summary>
        /// BaseStationSearch
        /// </summary>
        /// <param name="p"></param>
        /// <returns>specific BaseStation</returns>
        public BaseStation BaseStationSearch(int p)
        {
            foreach (BaseStation tmp in DataSource.baseStations)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new BaseStation();
        }
        /// <summary>
        /// print BaseStation
        /// </summary>
        /// <returns>BaseStation List</returns>
        public IEnumerable<BaseStation> printBaseStation()
        {
            return DataSource.baseStations.Take(DataSource.baseStations.Count).ToList();
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

            BaseStation bs = DataSource.baseStations[index1];
            Drone d = DataSource.drones[index2];

            bs.FreeChargingSlots++;

            DataSource.baseStations[index1] = bs;
            DataSource.drones[index2] = d;
        }
    }
}
