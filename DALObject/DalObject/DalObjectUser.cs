﻿using System;
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
            User d = DataSource.users.Find(par => par.Id == id);
            if (!CheckUser(id) && d.Deleted==false)
                throw new MissingIdException(id, "User");
            if (!CheckUser(id) && d.Deleted == true)
                throw new EntityHasBeenDeleted(id, "The user no longer exists in the system");
            return d;
        }
        public bool CheckUser(int id)
        {
            return DataSource.users.Any(par => par.Id == id && par.Deleted==false);
        }
        public void UpdUser(User tmp)
        {
            int count = DataSource.users.RemoveAll(par => tmp.Id == par.Id && par.Deleted == false);
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
                       where predicate(b) && b.Deleted == false
                       select b;
            }
            return from b in DataSource.users
                   where b.Deleted == false
                   select b;
        }
        public void DeleteUser(int uID)
        {
            int index1 = DataSource.users.FindIndex(x => x.Id == uID);
            User cs = DataSource.users[index1];
            if (cs.Deleted == true)
                throw new EntityHasBeenDeleted(uID, "This User has already been deleted");
            cs.Deleted = true;
            DataSource.users[index1] = cs;
        } 
    }
}
