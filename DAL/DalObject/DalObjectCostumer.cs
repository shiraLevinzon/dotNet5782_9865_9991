using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using IDAL.DO;
namespace DalObject
{
    public class DalObjectCostumer:DalObject
    {
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddCustomer(Customer tmp)
        {
            DataSource.customers.Add(tmp);
        }
        /// <summary>
        /// Customer Search
        /// </summary>
        /// <param name="p"></param>
        /// <returns>specific Customer</returns>

        public Customer CustomerSearch(int p)
        {
            foreach (Customer tmp in DataSource.customers)
            {
                if (tmp.ID == p)
                    return tmp;
            }
            return new Customer();
        }
        /// <summary>
        /// print Customer
        /// </summary>
        /// <returns>Customer List</returns>
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
