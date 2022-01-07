using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
namespace Dal
{
    partial class DalObject : IDal
    {
        public DroneCharge GetDroneInCharging(int id)
        {
            DroneCharge d = DataSource.droneCharges.FirstOrDefault(par => par.DroneID == id);
            if (!CheckDroneCharge(id)&& d.Deleted== (Deleted)0)
                throw new MissingIdException(id, "DroneCharge");
            if (!CheckDroneCharge(id) && d.Deleted == (Deleted)2)
                throw new EntityHasBeenDeleted(id, "The Drone no longer exists in the system");
                return d;
        }
        public bool CheckDroneCharge(int id)
        {
            return DataSource.droneCharges.Any(par => par.DroneID == id && par.Deleted== (Deleted)1);
        }
        public IEnumerable<DroneCharge> GetAllDroneCharge(Predicate<DroneCharge> predicate = null)
        {
            if (predicate != null)
            {
                return from b in DataSource.droneCharges
                       where predicate(b) && b.Deleted== (Deleted)1
                       select b;
            }
            return from b in DataSource.droneCharges
                   where b.Deleted== (Deleted)1
                   select b;
        }
        public void DeleteDroneInCharge(int dgID)
        {
            int index1 = DataSource.droneCharges.FindIndex(x => x.DroneID == dgID && x.Deleted== (Deleted)1);
            DroneCharge ps = DataSource.droneCharges[index1];
            if (ps.Deleted == (Deleted)2)
                throw new EntityHasBeenDeleted(dgID, "This Drone has already been deleted");
            ps.Deleted = (Deleted)2;
            DataSource.droneCharges[index1] = ps;
        }
        //no need of add and update functions becauese there is no need of them
    }
}
