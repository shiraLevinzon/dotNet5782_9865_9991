using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public IEnumerable<ParcelAtCustomer> PackagesFromCustomer { get; set; }
        public IEnumerable<ParcelAtCustomer> PackagesToCustomer { get; set; }
        public Deleted Deleted { get => Deleted; set => Deleted = (Deleted)0; }
    }
}
