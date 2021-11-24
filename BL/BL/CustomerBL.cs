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
                boCustomer.Location.Latitude = doCustomer.Latitude;
                boCustomer.Location.Longitude = doCustomer.Longitude;
                boCustomer.PackagesFromCustomer = GetAllParcels().Where(par => par.Sender.ID == boCustomer.ID).ToList();
                boCustomer.PackagesToCustomer = GetAllParcels().Where(par => par.Receiver.ID == boCustomer.ID).ToList();

            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }

            return boCustomer;
        }
        public IEnumerable<BO.Customer> GetAllCustome()
        {
            return from CustomerDO in dalLayer.printCustomer()
                   orderby CustomerDO.ID//מיון לפי תז
                   select GetCustomer(CustomerDO.ID);
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
        public void UpdateCustomer(BO.Customer customer)
        {

            //Update DO.Customer            
            IDAL.DO.Customer CustomerDO = new IDAL.DO.Customer();
            customer.CopyPropertiesTo(CustomerDO);
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
