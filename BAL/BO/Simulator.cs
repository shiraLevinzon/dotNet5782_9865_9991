using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BO
{
    class Simulator
    {
       
        public Simulator(BL.BL bL,int droneId, Action reportProgress, Func<bool> IsTimeRun)
        {
            DalApi.IDal dal = DalApi.DalFactory.GetDal();
            BO.ParcelInTransfer parcelInTransfer;
            double dis, batrry;
            int stationId = 0;
            DroneToList droneToList = bL.GetAllDrones().First(dro => dro.ID == droneId);
            while (!IsTimeRun())
            {
                switch (droneToList.Conditions)
                {
                    case DroneConditions.Available:
                        try
                        {
                            bL.AssignPackageToDrone(droneId);
                            reportProgress();
                        }
                        catch
                        {
                            if (droneToList.BatteryStatus < 100)
                            {
                                batrry = droneToList.BatteryStatus;
                                stationId = bL.helpbasestation(droneToList,bL.GetBaseStations());
                                dis = bL.DistanceTo(bL.GetBaseStation(stationId).BaseStationLocation.Latitude, bL.GetBaseStation(stationId).BaseStationLocation.Longitude, droneToList.location.Latitude, droneToList.location.Longitude);
                                while (dis > 0)
                                {
                                    droneToList.BatteryStatus -= bL.free;
                                    reportProgress();
                                    dis -= 1;
                                    Thread.Sleep(500);
                                }

                                //The SendingDroneforCharging function checks the initial distance and calculates the
                                //battery accordingly and therefore the battery needs to be returned to the initial state.
                                droneToList.BatteryStatus = batrry;
                                bL.DroneToCharging(droneId);
                                reportProgress();
                            }
                        }
                        break;
                    case DroneConditions.maintenance:
                        try
                        {
                            while (droneToList.BatteryStatus < 100)
                            {
                                droneToList.BatteryStatus += bL.droneLoadingRate;
                                reportProgress();
                                Thread.Sleep(500);
                            }
                            TimeSpan t = new TimeSpan(100, 0, 0);
                            bL.ReleaseDroneFromCharging(droneId,t);
                            reportProgress();
                        }
                        catch
                        {
                            
                        }
                        break;
                    case DroneConditions.delivery:
                        try
                        {
                            batrry = droneToList.BatteryStatus;
                            parcelInTransfer=bL.GetDrone(droneId).PackageInTransfer;
                            dis = bL.DistanceTo(droneToList.location.Latitude, droneToList.location.Longitude,parcelInTransfer.Collection.Latitude,parcelInTransfer.Collection.Longitude);
                            while (dis > 0)
                            {
                                droneToList.BatteryStatus -= dal.RequestPowerConsumptionByDrone()[Convert.ToInt32(parcelInTransfer.Weight) + 1];
                                reportProgress();
                                dis -= 1;
                                Thread.Sleep(500);
                            }
                            droneToList.BatteryStatus = batrry;
                            bL.CollectParcelByDrone(droneId);
                            reportProgress();


                            dis = parcelInTransfer.distance;
                            while (dis > 0)
                            {
                                droneToList.BatteryStatus -= dal.RequestPowerConsumptionByDrone()[Convert.ToInt32(parcelInTransfer.Weight) + 1];
                                reportProgress();
                                dis -= 1;
                                Thread.Sleep(500);
                            }
                            droneToList.BatteryStatus = batrry;
                            bL.DeliveryOfPackageByDrone(droneId);
                            reportProgress();
                        }
                        catch
                        {
                            //החריגות שיכולות להופיע זה אם כבר המשלוח נעשה ולכן בכל מקרה הוא יעבור למצב פנוי 
                            //או שעשו משלוח לפני איסוף חבילה אבל במקרה פה זה לא הגיוני כי עשינו איסוף ואז משלוח לפי סדר הפעולות לעיל
                        }

                        break;
                    default:
                        break;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
