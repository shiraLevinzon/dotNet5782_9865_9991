using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using DalApi;
using System.Runtime.CompilerServices;
namespace BL
{
     partial class BL : BlApi.IBL
     {
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Parcel GetParcel(int id)
        {
            BO.Parcel boParcel = new BO.Parcel();
            try
            {
                DO.Parcel doParcel = dalLayer.GetParcel(id);
                doParcel.CopyPropertiesTo(boParcel);
                boParcel.Sender = new BO.CustomerInParcel();
                boParcel.Receiver = new BO.CustomerInParcel();
                boParcel.Sender.ID = doParcel.SenderID;
                boParcel.Receiver.ID = doParcel.TargetID;
                boParcel.Sender.CustomerName = GetAllCustomer().First(cu => cu.ID == boParcel.Sender.ID).Name;
                boParcel.Receiver.CustomerName = GetAllCustomer().First(cu => cu.ID == boParcel.Receiver.ID).Name;

                if (boParcel.Scheduled!=DateTime.MinValue&& boParcel.Delivered == DateTime.MinValue)
                {
                    boParcel.DroneInParcel=new BO.DroneInParcel();
                    BO.Drone d= GetDrone(doParcel.DroneId);
                    boParcel.DroneInParcel.ID = d.ID;
                    boParcel.DroneInParcel.BatteryStatus = d.BatteryStatus;
                    boParcel.DroneInParcel.location = d.location;
                }
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName,"this id isnt existe");
            }
            catch(DO.EntityHasBeenDeleted ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName, "this parcel isnt existe anymore in the system");
            }
            return boParcel; 
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<BO.ParcelToList> GetAllParcels(Predicate<BO.ParcelToList> predicate = null,DateTime? date=null)
        {
            IEnumerable<DO.Parcel> parcels;
            if (date!=default)
            {
                parcels = dalLayer.GetAllParcels(par => par.Deleted == false && par.Requested.Day == date.Value.Day && par.Requested.Month == date.Value.Month && par.Requested.Year == date.Value.Year);
            }
            else
            {
               parcels = dalLayer.GetAllParcels();
            }
            IEnumerable<BO.ParcelToList> parcelToLists= from ParcelDO in parcels
                                                         select new BO.ParcelToList()
                                                         {
                                                             ID = ParcelDO.ID,
                                                             RecieverID = ParcelDO.TargetID,
                                                             SenderID = ParcelDO.SenderID,
                                                             ParcelPriority = (BO.Priorities)ParcelDO.priority,
                                                             Weight = (BO.WeightCategories)ParcelDO.Weight,
                                                             ParcelCondition = (BO.Situations)func(ParcelDO),
                                                         };

            if (predicate == null)
                return parcelToLists;
            return parcelToLists.Where(p => predicate(p));

            
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(BO.Parcel parcel)
        {
            //Add DO.Parcel            
            DO.Parcel ParcelDO = new DO.Parcel()
            {
                TargetID = parcel.Receiver.ID,
                SenderID = parcel.Sender.ID,
                Weight = (DO.WeightCategories)parcel.Weight,
                priority = (DO.Priorities)parcel.Priority,
                DroneId = 0,
                Deleted = parcel.Deleted,
                Requested = DateTime.Now,
                Scheduled = DateTime.MinValue,
                PickedUp = DateTime.MinValue,
                Delivered = DateTime.MinValue,
            };
            // הרחפן מאותחל ב-נאל
            try
            {
                dalLayer.AddParcel(ParcelDO);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ParcelDO.ID, "Parcel", "Student ID is illegal", ex);
            }
            catch (DO.EntityHasBeenDeleted ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName, "this parcel isnt existe anymore in the system");
            }
        }
        //public void UpdateParcel(BO.Parcel Parcel)
        //{

        //    //Update DO.BaseStation            
        //    IDAL.DO.Parcel ParcelDO = new IDAL.DO.Parcel();
        //    Parcel.CopyPropertiesTo(ParcelDO);
        //    try
        //    {
        //        dalLayer.UpdParcel(ParcelDO);
        //    }
        //    catch (IDAL.DO.DuplicateIdException ex)
        //    {
        //        throw new BO.DuplicateIdException(ParcelDO.ID, "Parcel", "Student ID is illegal", ex);
        //    }

        //}
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int func(DO.Parcel p)
        {
            int num = 0;
            if (p.Delivered != DateTime.MinValue)
                num = 3;
            else if (p.PickedUp != DateTime.MinValue)
                num = 2;
            else if (p.Scheduled != DateTime.MinValue)
                num = 1;
            else
                num = 0;
            return num;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            try
            {
                BO.Parcel p = GetParcel(id);
               if( dronesToList.Any(x => x.PackagNumberOnTransferred == p.ID))
                {
                    BO.DroneToList dro = dronesToList.Find(x => x.PackagNumberOnTransferred == p.ID);
                    dro.Conditions = (DroneConditions)1;
                }
                BO.Customer cust = GetCustomer(p.Sender.ID);
                cust.PackagesFromCustomer.ToList().RemoveAll(x => x.ID == p.ID);
                cust = GetCustomer(p.Receiver.ID);
                cust.PackagesToCustomer.ToList().RemoveAll(x => x.ID == p.ID);
                dalLayer.DeleteParcel(id);
            }
            catch (DO.EntityHasBeenDeleted ex)
            {
                throw new BO.EntityHasBeenDeleted(id, "Parcel", "This Parcel has already been deleted", ex);
            }
        }
    }
}
