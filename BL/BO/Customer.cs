using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL.BO
{
    class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location ParcelLocation { get; set; }
        public List<Parcel> PackagesFromCustomer { get; set; }
        public List<Parcel> PackagesToCustomer { get; set; }

    }
}
