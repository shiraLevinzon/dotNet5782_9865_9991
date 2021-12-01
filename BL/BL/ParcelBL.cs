using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BL
{
    public partial class BL : IBL
    {
        public BO.Parcel GetParcel(int id)
        {
            BO.Parcel boParcel = new BO.Parcel();
            try
            {
                IDAL.DO.Parcel doParcel = dalLayer.GetParcel(id);
                doParcel.CopyPropertiesTo(boParcel);
               IDAL.DO.Drone d  = dalLayer.GetDrone(doParcel.ID);
                d.CopyPropertiesTo(boParcel.DroneInParcel);
                //איך אני מוצאת את המיקום של הרחפן בחבילה (אין שדה של מיקום בדו דרון
            }
            catch (IDAL.DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName);
            }

            return boParcel; ;
        }
        public IEnumerable<BO.Parcel> GetAllParcels()
        {
            return from ParcelDO in dalLayer.printParcel()
                   orderby ParcelDO.ID//מיון לפי תז
                   select GetParcel(ParcelDO.ID);
        }
        public void AddParcel(BO.Parcel parcel)
        {
            //Add DO.Parcel            
            IDAL.DO.Parcel ParcelDO = new IDAL.DO.Parcel();
            parcel.Requested = DateTime.Now;
            parcel.Scheduled = DateTime.MinValue;
            parcel.PickedUp = DateTime.MinValue;
            parcel.Delivered = DateTime.MinValue;
            parcel.CopyPropertiesTo(ParcelDO);
           // הרחפן מאותחל ב-נאל
            try
            {
                dalLayer.AddParcel(ParcelDO);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ParcelDO.ID, "Parcel", "Student ID is illegal", ex);
            }
        }
        public void UpdateParcel(BO.Parcel Parcel)
        {

            //Update DO.BaseStation            
            IDAL.DO.Parcel ParcelDO = new IDAL.DO.Parcel();
            Parcel.CopyPropertiesTo(ParcelDO);
            try
            {
                dalLayer.UpdParcel(ParcelDO);
            }
            catch (IDAL.DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ParcelDO.ID, "Parcel", "Student ID is illegal", ex);
            }

        }

    }
}
