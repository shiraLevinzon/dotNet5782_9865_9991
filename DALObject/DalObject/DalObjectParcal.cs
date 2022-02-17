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
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel tmp)
        {
            if (CheckParcel(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "Parcel");

            tmp.ID =DataSource.Config.IdCount++;
            tmp.Requested = DateTime.Now;
            DataSource.parcels.Add(tmp);
        }
        /// <summary>
        /// Parcel Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns>spesific Parcel</returns>


        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            if (!CheckParcel(id))
                throw new MissingIdException(id, "Parcel");
            Parcel p = DataSource.parcels.FirstOrDefault(par => par.ID == id);
            return p;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool CheckParcel(int id)
        {
            return DataSource.parcels.Any(par => par.ID == id && par.Deleted== false);
        }
        /// <summary>
        /// print Parcel
        /// </summary>
        /// <returns>Parcel List</returns>


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetAllParcels(Predicate<Parcel> predicate = null)
        {
            if (predicate != null)
            {
                return from p in DataSource.parcels
                       where predicate(p) &&  p.Deleted == false
                       select p;
            }
            return from p in DataSource.parcels
                   where p.Deleted == false
                   select p;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdParcel(Parcel tmp)
        {
            int count = DataSource.parcels.RemoveAll(par => tmp.ID == par.ID && par.Deleted == false);
            if (count == 0)
                throw new MissingIdException(tmp.ID, "Parcel");
            DataSource.parcels.Add(tmp);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int pID)
        {
            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID);
            Parcel ps = DataSource.parcels[index1];
            if (ps.Deleted == true)
                throw new EntityHasBeenDeleted(pID, "This Parcel has already been deleted");
            ps.Deleted = true;
            DataSource.parcels[index1] = ps;
        }
    }
}
