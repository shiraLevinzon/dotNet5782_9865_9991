using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int ID { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight{ get; set; }
           

            public override string ToString()
            {
                return string.Format("ID: {0}\t Model:{1}\tMaxWeight:{2}\t BatteryStatus:{3}\t DroneCondition:{4}\t", ID,Model, MaxWeight,BatteryStatus,DroneCondition);
            }
        }
    }
}
