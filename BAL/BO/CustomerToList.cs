using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerToList
    {
        public int ID { get; set; }
         public string Name { get; set; }
        public string Phone { get; set; }
          public int NumberofPackagesSentandDelivered { get; set; }
          public int NumberofPackagesSentButNotDelivered{ get; set; }
           public int NumberOfPackagesHeReceived{ get; set; }
          public int NumberofPackagesOnTheWayToCustomer{ get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
