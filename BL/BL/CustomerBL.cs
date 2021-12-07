using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BL
{
    public partial class BL : IBL
    {
        public BO.Customer GetCustomer(int id)
        {
            BO.Customer boCustomer = new BO.Customer();
            try
            {
                IDAL.DO.Customer doCustomer = dalLayer.GetCostumer(id);
                doCustomer.CopyPropertiesTo(boCustomer);
                boCustomer.Location = new BO.Location();
                boCustomer.Location.Latitude = doCustomer.Latitude;
                boCustomer.Location.Longitude = doCustomer.Longitude;
                boCustomer.PackagesFromCustomer = from p in dalLayer.GetAllParcelsByPredicate(par => par.SenderID == id)
                                                  select new BO.ParcelAtCustomer() { };




                //boCustomer.PackagesFromCustomer = new List<BO.ParcelAtCustomer>();
                //foreach (var item in GetAllParcels().Where(par => par.SenderID == boCustomer.ID).ToList())
                //{
                //    BO.ParcelAtCustomer pat = new BO.ParcelAtCustomer();
                //    pat.ID = item.ID;
                //    pat.Weight = item.Weight;
                //    pat.priority = item.ParcelPriority;
                //    BO.CustomerToList customerTo = GetAllCustomer().First(cus => cus.ID == item.RecieverID);
                //    pat.CustomerInParcel.ID = customerTo.ID;
                //    pat.CustomerInParcel.CustomerName = customerTo.Name;
                //    pat.Situation = item.ParcelCondition;
                //    boCustomer.PackagesFromCustomer.Add(pat);

                //}
                boCustomer.PackagesToCustomer = new List<BO.ParcelAtCustomer>();
                foreach (var item in GetAllParcels().Where(par => par.RecieverID == boCustomer.ID).ToList())
                {
                    BO.ParcelAtCustomer pat = new BO.ParcelAtCustomer();
                    pat.ID = item.ID;
                    pat.Weight = item.Weight;
                    pat.priority = item.ParcelPriority;
                    BO.CustomerToList customerTo = GetAllCustomer().First(cus => cus.ID == item.SenderID);
                    pat.CustomerInParcel.ID = customerTo.ID;
                    pat.CustomerInParcel.CustomerName = customerTo.Name;
                    pat.Situation = item.ParcelCondition;
                    boCustomer.PackagesToCustomer.Add(pat);
                }
            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }

            return boCustomer;
        }
        public IEnumerable<BO.CustomerToList> GetAllCustomer()
        {
            //אססור להשתשמש בגט פרסל
            return from c in dalLayer.printCustomer()
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
        }
        public void AddCustomer(BO.Customer customer)
        {

            //Add DO.Customer            
            IDAL.DO.Customer customerDO = new IDAL.DO.Customer();
            customer.CopyPropertiesTo(customerDO);
            customerDO.Latitude = customer.Location.Latitude;
            customerDO.Longitude = customer.Location.Longitude;
            try
            {
                dalLayer.AddCustomer(customerDO);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(customerDO.ID, "Customer", "Customer ID is illegal", ex);
            }
        }
        public void UpdateCustomer(int id,string name,string phone)
        {

            //Update DO.Customer            
            IDAL.DO.Customer CustomerDO = new IDAL.DO.Customer();
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
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(CustomerDO.ID, "Customer", "Student ID is illegal", ex);
            }

        }
    }
}
