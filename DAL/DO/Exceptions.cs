/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.DO
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
}

*/