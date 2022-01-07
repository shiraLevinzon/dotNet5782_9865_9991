using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
namespace Dal
{
     partial class DalObject: DalApi.IDal
    {
        public Customer GetCostumer(int id)
        {
            Customer d = DataSource.customers.FirstOrDefault(par => par.ID == id);
            if (!CheckCustomer(id) && d.Deleted== (Deleted)0)
                throw new MissingIdException(id, "Customer");
            if (!CheckCustomer(id) && d.Deleted == (Deleted)2)
                throw new EntityHasBeenDeleted(id, "A customer no longer exists in the system");
            return d;
        }
        public bool CheckCustomer(int id)
        {
            return DataSource.customers.Any(par => par.ID == id && par.Deleted== (Deleted)1);
        }
        public void UpdCustomer(Customer tmp)
        {
            int count = DataSource.customers.RemoveAll(par => tmp.ID == par.ID && par.Deleted== (Deleted)1);

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
        public IEnumerable<Customer> GetAllCustomers(Predicate<Customer> predicate=null)
        {
            if(predicate!=null)
            {
                return from c in DataSource.customers
                       where predicate(c) && c.Deleted== (Deleted)1
                       select c;
            }
            return from c in DataSource.customers
                   where c.Deleted== (Deleted)1
                   select c;
        }
        public void DeleteCustomer(int csID)
        {
            int index1 = DataSource.customers.FindIndex(x => x.ID == csID);
            Customer cs = DataSource.customers[index1];
            if (cs.Deleted == (Deleted)2)
                throw new EntityHasBeenDeleted(csID, "This Customer has already been deleted");
            cs.Deleted = (Deleted)2;
            DataSource.customers[index1] = cs;
        }
    }
}
