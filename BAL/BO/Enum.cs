using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO
{
    public enum WeightCategories { light, normal, heavy }
    //קל, רגיל, כבד
    public enum Priorities { low, normal, hight }
    //נמוך, רגיל, גבוה
    public  enum Situations {Created, associated, collected, provided}
    //נוצר, שייך, נאסף, סיפק
    public enum DroneConditions { maintenance ,Available, delivery, /*charging*/}
    //תחזוקה, זמינה, משלוח, טעינה
    public enum Deleted { Uninitialized, False, True }

    public enum numbers { zero, one, two, Three, four, five, seven, eight, nine, ten }

}
