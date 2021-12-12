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
            tmp.Requested = DateTime.Now;
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
        public IEnumerable<Parcel> GetAllParcels()
        {
            return DataSource.parcels.Take(DataSource.parcels.Count);
        }
        public IEnumerable<Parcel> GetAllParcelsByPredicate(Predicate<Parcel> predicate/*=null*/)
        {
            return from p in DataSource.parcels
                   where predicate(p)
                  // where predicate!=null
                   select p;
        }
        public void UpdParcel(Parcel tmp)
        {
            int count = DataSource.parcels.RemoveAll(par => tmp.ID == par.ID);

            if (count == 0)
                throw new MissingIdException(tmp.ID, "Parcel");

            DataSource.parcels.Add(tmp);
        }
    }
}
