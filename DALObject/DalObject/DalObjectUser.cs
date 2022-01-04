using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;

namespace Dal
{
    partial class DalObject : DalApi.IDal
    {
        public User GetUser(int id)
        {
            if (!CheckUser(id))
                throw new MissingIdException(id, "User");

            User d = DataSource.users.Find(par => par.Id == id);
            return d;
        }
        public bool CheckUser(int id)
        {
            return DataSource.users.Any(par => par.Id == id);
        }
        public void UpdUser(User tmp)
        {
            int count = DataSource.users.RemoveAll(par => tmp.Id == par.Id);

            if (count == 0)
                throw new MissingIdException(tmp.Id, "User");

            DataSource.users.Add(tmp);
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddUser(User tmp)
        {
            if (CheckUser(tmp.Id))
                throw new DuplicateIdException(tmp.Id, "User");
            DataSource.users.Add(tmp);
        }
        public IEnumerable<User> GetAllUser(Predicate<User> predicate = null)
        {
            if (predicate != null)
            {
                return from b in DataSource.users
                       where predicate(b)
                       select b;
            }
            return from b in DataSource.users
                   select b;
        }
    }
}
