﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using System.Runtime.CompilerServices;
namespace Dal
{
    partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// Assign A Package To A Drone
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignPackageToDrone(int pID, int dID)
        {
            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID && x.Deleted== false);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID && x.Deleted == false);

            Parcel p = DataSource.parcels[index1];
            Drone d = DataSource.drones[index2];

            p.DroneId = dID;
            p.Scheduled = DateTime.Now;

            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;
        }
        /// <summary>
        /// Parcel Collection By A Drone
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ParcelCollectionByDrone(int pID, int dID)
        {

            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID && x.Deleted == false);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID && x.Deleted == false);


            Parcel p = DataSource.parcels[index1];
            Drone d = DataSource.drones[index2];

            p.PickedUp = DateTime.Now;
            d.MaxWeight = p.Weight;

            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;

        }
        /// <summary>
        /// Delivery Parcel To Customer
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="dID"></param>


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveryParcelToCustomer(int pID, int dID)
        {
            int index1 = DataSource.parcels.FindIndex(x => x.ID == pID && x.Deleted == false);
            int index2 = DataSource.drones.FindIndex(x => x.ID == dID && x.Deleted == false);

            Parcel p = DataSource.parcels[index1];
            Drone d = DataSource.drones[index2];

            p.Delivered = DateTime.Now;
            p.DroneId = 0;


            DataSource.parcels[index1] = p;
            DataSource.drones[index2] = d;

        }
    }
}
