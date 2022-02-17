using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Runtime.CompilerServices;
namespace Dal
{
     partial class DalObject : DalApi.IDal
    {

        /// <summary>
        /// Parcel Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns>spesific Parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            if (!CheckDrone(id))
                throw new MissingIdException(id, "Drone");
            Drone d = DataSource.drones.FirstOrDefault(par => par.ID == id);
            return d;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool CheckDrone(int id)
        {
            return DataSource.drones.Any(par => par.ID == id && par.Deleted== false);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdDrone(Drone tmp)
        {
            int count = DataSource.drones.Count(par => tmp.ID == par.ID && par.Deleted == false);
            DataSource.drones.RemoveAll(par => tmp.ID == par.ID && par.Deleted == false);

            if (count == 0)
                throw new MissingIdException(tmp.ID, "Drone");

            DataSource.drones.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddDrone(Drone tmp)
        {
            if (CheckDrone(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "Drone");
            DataSource.drones.Add(tmp);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<Drone> GetAllDrones(Predicate<Drone> predicate=null)
        {
            if (predicate != null)
            {
                return from d in DataSource.drones
                       where predicate(d) && d.Deleted== false
                       select d;
            }
            return from d in DataSource.drones
                   where d.Deleted== false
                   select d;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int dID)
        {
            int index1 = DataSource.drones.FindIndex(x => x.ID == dID);
            Drone cs = DataSource.drones[index1];
            if (cs.Deleted == true)
                throw new EntityHasBeenDeleted(dID, "This Drones has already been deleted");
            cs.Deleted = true;
            DataSource.drones[index1] = cs;
        }
    }
}
