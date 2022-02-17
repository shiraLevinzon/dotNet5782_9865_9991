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


    
    partial class DalObject: DalApi.IDal
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCostumer(int id)
        {
            if (!CheckCustomer(id))
                throw new MissingIdException(id, "Customer");
            Customer d = DataSource.customers.FirstOrDefault(par => par.ID == id);
            return d;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool CheckCustomer(int id)
        {
            return DataSource.customers.Any(par => par.ID == id && par.Deleted== false);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdCustomer(Customer tmp)
        {
            int count = DataSource.customers.RemoveAll(par => tmp.ID == par.ID && par.Deleted== false);

            if (count == 0)
                throw new MissingIdException(tmp.ID, "Customer");

            DataSource.customers.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer tmp)
        {
            if (CheckCustomer(tmp.ID))
                throw new DuplicateIdException(tmp.ID, "Customer");

            DataSource.customers.Add(tmp);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetAllCustomers(Predicate<Customer> predicate=null)
        {
            if(predicate!=null)
            {
                return from c in DataSource.customers
                       where predicate(c) && c.Deleted== false
                       select c;
            }
            return from c in DataSource.customers
                   where c.Deleted== false
                   select c;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int csID)
        {
            int index1 = DataSource.customers.FindIndex(x => x.ID == csID);
            Customer cs = DataSource.customers[index1];
            if (cs.Deleted == true)
                throw new EntityHasBeenDeleted(csID, "This Customer has already been deleted");
            cs.Deleted = true;
            DataSource.customers[index1] = cs;
        }
    }
}
