using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BL
{
    partial class BL : BlApi.IBL
    {

        public User GetUser(int id)
        {
            User user = new User();
            try
            {
                DO.User u = dalLayer.GetUser(id);
                user.Id = u.Id;
                user.Name = u.Name;
                user.Password = u.Password;
                user.Phone = u.Phone;
            }
            catch (DO.MissingIdException ex)
            {
             throw new BO.MissingIdException(ex.ID, ex.EntityName, "this id isnt existe");
            }
            return user;
        }

        public void UpdUser(User tmp)
        {
            DO.User user = new DO.User();

            try
            {
                user.Id = tmp.Id;
                user.Name = tmp.Name;
                user.Password = tmp.Password;
                user.Phone = tmp.Phone; dalLayer.UpdUser(user);
            }
            catch (DO.MissingIdException ex)
            {

                throw new BO.MissingIdException(ex.ID, ex.EntityName, "this id isnt existe");
            }
        }
        /// <summary>
        /// Functions Add a new field to one of the lists
        /// </summary>
        /// <param name="tmp"></param>
        public void AddUser(User tmp)
        {
            DO.User user = new DO.User();

            try
            {
                user.Id = tmp.Id;
                user.Name = tmp.Name;
                user.Password = tmp.Password;
                user.Phone = tmp.Phone;
                dalLayer.AddUser(user);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName, "this id isnt correct");
            }
        }
        public IEnumerable<User> GetAllUser(Predicate<User> predicate = null)
        {
            IEnumerable<BO.User> UserLists = from UserDO in dalLayer.GetAllUser()
                                             select GetUser(UserDO.Id);
            if (predicate == null)
                return UserLists;
            return UserLists.Where(p => predicate(p));
        }

    }
}
