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
            double dis, batrry;
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
                                dis = bL.helpbasestation(droneToList,bL.GetBaseStations());

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

                        
                        break;
                    default:
                        break;
                }
                Thread.Sleep(1000);
            }
        }
    }
}
