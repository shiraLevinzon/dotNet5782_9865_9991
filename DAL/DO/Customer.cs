using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }

            public override string ToString()
            {
                return string.Format("ID: {0}\t Name:{1}\t Phone:{2}\t Longitude:{3}\t Latitude:{4}\t", ID, Name, Phone, Longitude, Latitude);
            }
        }
    }
   
}
