using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL;
using DalObject;
namespace IBL.BL
{
    class BL
    {
        IDAL.IDal dalLayer = new DalObject.DalObject();
        List<Drone_to_list> dronesToList = new List<Drone_to_list>();
        private readonly DroneConditions delivery;

        public double zero { get; set; }
        public double light { get; set; }
        public double normal { get; set; }
        public double heavy { get; set; }
        public double full { get; set; }
        public BL() 
        {
            zero = dalLayer.RequestPowerConsumptionByDrone()[0];
            light = dalLayer.RequestPowerConsumptionByDrone()[1];
            normal = dalLayer.RequestPowerConsumptionByDrone()[2];
            heavy = dalLayer.RequestPowerConsumptionByDrone()[3];
            full = dalLayer.RequestPowerConsumptionByDrone()[4];
            List<Drone> TMPdrone = new List<Drone>();
            List<Parcel> TMPparcel = new List<Parcel>();
            int i = 0;
            foreach (var parcel in dalLayer.printParcel())
            {
                TMPparcel[i].Delivered= parcel.Delivered;
                TMPparcel[i].ID = parcel.ID;
                TMPparcel[i].PickedUp = parcel.PickedUp;
                TMPparcel[i].Priority = (Priorities)parcel.priority;
                TMPparcel[i].Requested = parcel.Requested;
                TMPparcel[i].Scheduled = parcel.Scheduled;
                TMPparcel[i].Weight = (WeightCategories)parcel.Weight;
                i++;
            }
            i = 0;
            foreach(var drone in dalLayer.printDrone())
            {
                TMPdrone[i].BatteryStatus = drone.BatteryStatus;
                TMPdrone[i].ID = drone.ID;
                TMPdrone[i].Model = drone.Model;
                TMPdrone[i].MaxWeight = (WeightCategories)drone.MaxWeight;
                i++;
            }
            /*foreach (var drone in dalLayer.printDrone())
            {
                int index = TMPparcel.FindIndex(x=>x.DroneId == drone.ID);
                int index1 = dronesToList.FindIndex(x => x.ID == drone.ID);
                if (TMPparcel[index].PickedUp > DateTime.Now) 
                {
                    Drone p = drone;
                    p.Conditions = delivery;
                    TMPdrone[index1] = p;
                }
            }*/
        }
    }
}