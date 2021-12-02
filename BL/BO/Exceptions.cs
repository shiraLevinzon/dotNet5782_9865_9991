using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    [Serializable]
    public class MissingIdException : Exception
    {
        public int ID;

        public string EntityName;
        public MissingIdException(int id, string entity) : base() { ID = id; EntityName = entity; }
        public MissingIdException(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public MissingIdException(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - missing id: {ID}";
    }

    [Serializable]
    public class DuplicateIdException : Exception
    {
        public int ID;

        public string EntityName;
        public DuplicateIdException(int id, string entity) : base() { ID = id; EntityName = entity; }
        public DuplicateIdException(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public DuplicateIdException(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - duplicate id: {ID}";
    }
    [Serializable]
    public class ImproperMaintenanceCondition : Exception
    {
        public int ID;

        public string EntityName;
        public ImproperMaintenanceCondition(int id, string entity) : base() { ID = id; EntityName = entity; }
        public ImproperMaintenanceCondition(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public ImproperMaintenanceCondition(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - Drone: {ID} -mode is not available:";
    }
    [Serializable]
    public class PackageTimesException : Exception
    {
        public int ID;

        public string EntityName;
        public PackageTimesException(int id, string entity) : base() { ID = id; EntityName = entity; }
        public PackageTimesException(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public PackageTimesException(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - Drone: {ID} -mode is not available:";
    }
    public class TheDroneDnotShip : Exception
    {
        public int ID;

        public string EntityName;
        public TheDroneDnotShip(int id, string entity) : base() { ID = id; EntityName = entity; }
        public TheDroneDnotShip(int id, string entity, string message) :
            base(message)
        { ID = id; EntityName = entity; }
        public TheDroneDnotShip(int id, string entity, string message, Exception innerException) :
            base(message, innerException)
        { ID = id; EntityName = entity; }
        public override string ToString() => base.ToString() + $", {EntityName} - Drone: {ID} -mode is not available:";
    }
    

}
