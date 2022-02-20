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
        /// Get func of base station
        /// </summary>
        /// <param name="GetBaseStation"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBaseStation(int id)
        {

            if (!CheckBaseStation(id))
                throw new MissingIdException(id, "Base Station does not exist in the system");
            BaseStation b = DataSource.baseStations.FirstOrDefault(par => par.ID == id);
            return b;
        }

        /// <summary>
        /// check func of base station
        /// </summary>
        /// <param name="CheckBaseStation"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool CheckBaseStation(int id)
        {
            return DataSource.baseStations.Any(par => par.ID == id && par.Deleted == false);
        }



        /// <summary>
        /// update func of base station
        /// </summary>
        /// <param name="UpdBaseStation"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdBaseStation(BaseStation tmp)
        {
            int count = DataSource.baseStations.RemoveAll(par => tmp.ID == par.ID && par.Deleted== false);

            if (count == 0)
                throw new MissingIdException(tmp.ID, "BaseStation");
            DataSource.baseStations.Add(tmp);
        }


        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddBaseStation(BaseStation tmp)
        {
            if (CheckBaseStation(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "BaseStation");
            DataSource.baseStations.Add(tmp);
        }

        /// <summary>
        /// Get func of list of base station
        /// </summary>
        /// <param name="GetAllBaseStations"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetAllBaseStations(Predicate<BaseStation> predicate=null)
        {
            if (predicate != null)
            {
                return from b in DataSource.baseStations
                       where predicate(b) && b.Deleted== false
                       select b;
            }
            return from b in DataSource.baseStations
                   where b.Deleted== false
                   select b;
        }

       
        /// <summary>
        /// Delete func of base station
        /// </summary>
        /// <param name="DeleteBaseStatin"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteBaseStatin(int bsID)
        {
            int index1 = DataSource.baseStations.FindIndex(x => x.ID == bsID);
            BaseStation bs = DataSource.baseStations[index1];
            if (bs.Deleted == true)
                throw new EntityHasBeenDeleted(bsID, "This base station has already been deleted");
            bs.Deleted = true;
            DataSource.baseStations[index1] = bs;
        }
    }
}
