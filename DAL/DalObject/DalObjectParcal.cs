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
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddParcel(Parcel tmp)
        {
            if (CheckParcel(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "Parcel");

            tmp.ID = DataSource.Config.IdCount++;
            DataSource.parcels.Add(tmp);
        }
        /// <summary>
        /// Parcel Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns>spesific Parcel</returns>
        public Parcel GetParcel(int id)
        {
            if (!CheckParcel(id))
                throw new MissingIdException(id, "Parcel");

            Parcel p = DataSource.parcels.Find(par => par.ID == id);
            return p;
        }
        public bool CheckParcel(int id)
        {
            return DataSource.parcels.Any(par => par.ID == id);
        }
        /// <summary>
        /// print Parcel
        /// </summary>
        /// <returns>Parcel List</returns>
        public IEnumerable<Parcel> printParcel()
        {
            return DataSource.parcels.Take(DataSource.parcels.Count);
        }
        public void UpdParcel(Parcel tmp)
        {
            int count = DataSource.parcels.RemoveAll(par => tmp.ID == par.ID);

            if (count == 0)
                throw new MissingIdException(tmp.ID, "Parcel");

            DataSource.parcels.Add(tmp);
        }
        /// <summary>
        /// Parcel Collection By A Drone
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void ParcelCollectionByDrone(int pID, int dID)
        {

            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);

            Parcel p = DataSource.parcels[index1];
            Drone d = DataSource.drones[index2];

            p.PickedUp = DateTime.Now;//עדכון זמן
            d.MaxWeight = p.Weight;//עדכון משקל

            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;

        }
    }
}
