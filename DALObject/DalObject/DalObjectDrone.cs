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

        /// <summary>
        /// Parcel Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns>spesific Parcel</returns>
        public Drone GetDrone(int id)
        {
            Drone d = DataSource.drones.FirstOrDefault(par => par.ID == id);
            if (!CheckDrone(id) && d.Deleted== (Deleted)0)
                throw new MissingIdException(id, "Drone");
            if (!CheckDrone(id) && d.Deleted == (Deleted)2)
                throw new EntityHasBeenDeleted(id, "The Drone no longer exists in the system");
            return d;
        }
        public bool CheckDrone(int id)
        {
            return DataSource.drones.Any(par => par.ID == id && par.Deleted== (Deleted)1);
        }

        public void UpdDrone(Drone tmp)
        {
            int count = DataSource.drones.Count(par => tmp.ID == par.ID && par.Deleted == (Deleted)1);
            DataSource.drones.RemoveAll(par => tmp.ID == par.ID && par.Deleted == (Deleted)1);

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
        public IEnumerable<Drone> GetAllDrones(Predicate<Drone> predicate=null)
        {
            if (predicate != null)
            {
                return from d in DataSource.drones
                       where predicate(d) && d.Deleted== (Deleted)1
                       select d;
            }
            return from d in DataSource.drones
                   where d.Deleted== (Deleted)1
                   select d;
        }
        public void DeleteDrone(int dID)
        {
            int index1 = DataSource.customers.FindIndex(x => x.ID == dID);
            Drone cs = DataSource.drones[index1];
            if (cs.Deleted == (Deleted)2)
                throw new EntityHasBeenDeleted(dID, "This Drones has already been deleted");
            cs.Deleted = (Deleted)2;
            DataSource.drones[index1] = cs;
        }
    }
}
