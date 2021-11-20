using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
namespace DalObject
{
    public class DalObjectParcal : DalObject
    {
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddParcel(Parcel tmp)
        {
            tmp.ID = DataSource.Config.IdCount++;
            DataSource.parcels.Add(tmp);
        }
        /// <summary>
        /// Parcel Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns>spesific Parcel</returns>
        public Parcel ParcelSearch(int p)
        {
            foreach (Parcel tmp in DataSource.parcels)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new Parcel();
        }
        /// <summary>
        /// print Parcel
        /// </summary>
        /// <returns>Parcel List</returns>
        public IEnumerable<Parcel> printParcel()
        {
            return DataSource.parcels.Take(DataSource.parcels.Count).ToList();
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

            p.PickedUp = DateTime.Now;
            d.MaxWeight = p.Weight;

            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;

        }
    }
}
