using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IBL.BO
{
    public enum WeightCategories { light, normal, heavy }
    public enum Priorities { low, normal, hight }
    public  enum Situations {Created, associated, collected, provided}
    public enum ParcelConditions {Defined, associated, collected, provided }
    public enum DroneConditions { maintenance ,Available,delivery,charging}
    //תחזוקה=maintenance
}
