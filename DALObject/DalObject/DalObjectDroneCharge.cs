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
    partial class DalObject : IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneInCharging(int id)
        {
            if (!CheckDroneCharge(id))
                throw new MissingIdException(id, "DroneCharge");
            DroneCharge d = DataSource.droneCharges.FirstOrDefault(par => par.DroneID == id);
            return d;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool CheckDroneCharge(int id)
        {
            return DataSource.droneCharges.Any(par => par.DroneID == id && par.Deleted == false);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetAllDroneCharge(Predicate<DroneCharge> predicate = null)
        {
            if (predicate != null)
            {
                return from b in DataSource.droneCharges
                       where predicate(b) && b.Deleted == false
                       select b;
            }
            return from b in DataSource.droneCharges
                   where b.Deleted == false
                   select b;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneInCharge(int dgID)
        {
            int index1 = DataSource.droneCharges.FindIndex(x => x.DroneID == dgID && x.Deleted == false);
            DroneCharge ps = DataSource.droneCharges[index1];
            if (ps.Deleted == true)
                throw new EntityHasBeenDeleted(dgID, "This Drone has already been remuved");
            ps.Deleted = true;
            DataSource.droneCharges[index1] = ps;
        }
        //no need of add and update functions becauese there is no need of them
    }
}
