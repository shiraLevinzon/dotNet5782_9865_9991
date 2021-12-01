using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;
using IDAL;
namespace DalObject
{
    public partial class DalObject : IDal
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
            return DataSource.drones.Take(DataSource.drones.Count).ToList();

        }
       
    }
}
