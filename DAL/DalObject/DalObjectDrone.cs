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
        /// Parcel Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns>spesific Parcel</returns>
        public Drone GetDrone(int id)
        {
            if (!CheckDrone(id))
                throw new MissingIdException(id, "Drone");

            Drone d = DataSource.drones.Find(par => par.ID == id);
            return d;
        }
        public bool CheckDrone(int id)
        {
            return DataSource.drones.Any(par => par.ID == id);
        }

        public void UpdDrone(Drone tmp)
        {
            int count = DataSource.drones.RemoveAll(par => tmp.ID == par.ID);

            if (count == 0)
                throw new MissingIdException(tmp.ID, "Drone");

            DataSource.drones.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddDrone(Drone tmp)
        {
            if (CheckDrone(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "Drone");

            DataSource.drones.Add(tmp);
        }

        public IEnumerable<Drone> printDrone()
        {
            return DataSource.drones.Take(DataSource.drones.Count);

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
