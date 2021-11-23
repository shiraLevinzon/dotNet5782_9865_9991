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
   public partial class DalObject:IDal
    {
        public Customer GetCostumer(int id)
        {
            if (!CheckCustomer(id))
                throw new MissingIdException(id, "Customer");

            Customer d = DataSource.customers.Find(par => par.ID == id);
            return d;
        }
        public bool CheckCustomer(int id)
        {
            return DataSource.customers.Any(par => par.ID == id);
        }

        public void UpdCustomer(Customer tmp)
        {
            int count = DataSource.customers.RemoveAll(par => tmp.ID == par.ID);

            if (count == 0)
                throw new MissingIdException(tmp.ID, "Customer");

            DataSource.customers.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddCustomer(Customer tmp)
        {
            if (CheckCustomer(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "Customer");

            DataSource.customers.Add(tmp);
        }

        public IEnumerable<Customer> printCustomer()
        {
            return DataSource.customers.Take(DataSource.customers.Count).ToList();
        }
        /// <summary>
        /// Delivery Parcel To Customer
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>
        public void DeliveryParcelToCustomer(int pID, int dID)
        {
            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID);

            Parcel p = DataSource.parcels[index1];
            Drone d = DataSource.drones[index2];

            p.Delivered = DateTime.Now;


            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;

        }
    }
}
