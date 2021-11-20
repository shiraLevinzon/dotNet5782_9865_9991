using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;
namespace DalObject
{
    class DalObjectDrone
    {
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddDrone(Drone tmp)
        {
            DataSource.drones.Add(tmp);
        }
        /// <summary>
        /// Drone Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns> specific Drone</returns>
        public Drone DroneSearch(int p)
        {
            foreach (Drone tmp in DataSource.drones)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new Drone();
        }
        /// <summary>
        /// print Drone
        /// </summary>
        /// <returns>drone list</returns>
        public IEnumerable<Drone> printDrone()
        {
            return DataSource.drones.Take(DataSource.drones.Count).ToList();

        }
        /// <summary>
        /// Assign A Package To A Drone
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void AssignPackageToDrone(int pID, int dID)
        {
            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);

            Parcel p = DataSource.parcels[index1];
            Drone d = DataSource.drones[index2];

            p.DroneId = dID;
            p.Scheduled = DateTime.Now;

            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;
        }
    }
}
