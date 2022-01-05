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
            if (!CheckDroneCharge(id))
                throw new MissingIdException(id, "DroneCharge");

            DroneCharge d = DataSource.droneCharges.Find(par => par.DroneID == id);
            return d;
        }
        public bool CheckDroneCharge(int id)
        {
            return DataSource.droneCharges.Any(par => par.DroneID == id);
        }
        public IEnumerable<DroneCharge> GetAllDroneCharge(Predicate<DroneCharge> predicate = null)
        {
            if (predicate != null)
            {
                return from b in DataSource.droneCharges
                       where predicate(b)
                       select b;
            }
            return from b in DataSource.droneCharges
                   select b;
        }
        //no need of add and update functions becauese there is no need of them
    }
}
