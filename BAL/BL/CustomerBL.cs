using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DalApi;
namespace BL
{
     partial class BL : BlApi.IBL
    {
        public BO.Customer GetCustomer(int id)
        {
            BO.Customer boCustomer = new BO.Customer();
            try
            {
                DO.Customer doCustomer = dalLayer.GetCostumer(id);
                //doCustomer.CopyPropertiesTo(boCustomer);
                boCustomer.ID = doCustomer.ID;
                boCustomer.Name = doCustomer.Name;
                boCustomer.Phone = doCustomer.Phone;
                boCustomer.Location = new BO.Location();
                boCustomer.Location.Latitude = doCustomer.Latitude;
                boCustomer.Location.Longitude = doCustomer.Longitude;
                boCustomer.PackagesFromCustomer = from p in dalLayer.GetAllParcels(par => par.SenderID == id)
                                                  select new BO.ParcelAtCustomer()
                                                  {
                                                      ID = p.ID,
                                                      Weight = (BO.WeightCategories)p.Weight,
                                                      priority = (BO.Priorities)p.priority,
                                                      Situation=(BO.Situations)func(p),
                                                      CustomerInParcel = new BO.CustomerInParcel()
                                                      {
                                                          ID = p.TargetID,
                                                          CustomerName = dalLayer.GetCostumer(p.TargetID).Name,
                                                      }
                                                  };

                boCustomer.PackagesToCustomer = from par in dalLayer.GetAllParcels(par => par.TargetID == id)
                                                select new BO.ParcelAtCustomer()
                                                {
                                                    ID = par.ID,
                                                    Weight = (BO.WeightCategories)par.Weight,
                                                    priority = (BO.Priorities)par.priority,
                                                    Situation = (BO.Situations)func(par),
                                                    CustomerInParcel = new BO.CustomerInParcel()
                                                    {
                                                        ID = par.SenderID,
                                                        CustomerName = dalLayer.GetCostumer(par.SenderID).Name,
                                                    }
                                                };
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }
            catch(DO.EntityHasBeenDeleted ex)
            {
                throw new BO.EntityHasBeenDeleted(ex.ID, ex.EntityName);
            }
            return boCustomer;
        }
        public IEnumerable<BO.CustomerToList> GetAllCustomer(Predicate<BO.CustomerToList> predicate = null)
        {
            //אסור להשתשמש בגט פרסל
            IEnumerable<BO.CustomerToList> CustomerToLists = from c in dalLayer.GetAllCustomers()
                   let cu = new BO.Customer()
                   select new BO.CustomerToList()
                   {
                       ID = c.ID,
                       Name = c.Name,
                       Phone = c.Phone,
                       NumberofPackagesSentandDelivered = GetCustomer(c.ID).PackagesFromCustomer.Count(par => par.Situation == (BO.Situations)3),
                       NumberofPackagesSentButNotDelivered = GetCustomer(c.ID).PackagesFromCustomer.Count(par => par.Situation == (BO.Situations)2),
                       NumberOfPackagesHeReceived = GetCustomer(c.ID).PackagesToCustomer.Count(par => par.Situation == (BO.Situations)3),
                       NumberofPackagesOnTheWayToCustomer = GetCustomer(c.ID).PackagesToCustomer.Count(par => par.Situation == (BO.Situations)2),
                   };
            if (predicate == null)
                return CustomerToLists;
            return CustomerToLists.Where(p => predicate(p));
        }
        public void AddCustomer(BO.Customer customer)
        {

            //Add DO.Customer            
            DO.Customer customerDO = new DO.Customer()
            {
                ID = customer.ID,
                Name = customer.Name,
                Phone = customer.Phone,
                Latitude = customer.Location.Latitude,
                Longitude = customer.Location.Longitude,
                Deleted = (DO.Deleted)1,
            };
            try
            {
                dalLayer.AddCustomer(customerDO);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(customerDO.ID, "Customer", "Customer ID is illegal", ex);
            }
        }
        public void UpdateCustomer(int id,string name,string phone)
        {

            //Update DO.Customer            
            DO.Customer CustomerDO = new DO.Customer();
            CustomerDO = dalLayer.GetCostumer(id);
            if (name!=null)
            {
                CustomerDO.Name = name;
            }
            if (phone != null)
            {
                CustomerDO.Phone = phone;
            }
            try
            {
                dalLayer.UpdCustomer(CustomerDO);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(CustomerDO.ID, "Customer", "Customer ID is illegal", ex);
            }

        }
    }
}
