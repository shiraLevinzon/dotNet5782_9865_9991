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
            catch (DO.EntityHasBeenDeleted ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName, "this parcel isnt existe anymore in the system");
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
                user.Deleted = tmp.Deleted;
                user.Phone = tmp.Phone; dalLayer.UpdUser(user);
            }
            catch (DO.MissingIdException ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName, "this id isnt existe");
            }
            catch (DO.EntityHasBeenDeleted ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName, "this parcel isnt existe anymore in the system");
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
                user.Deleted = tmp.Deleted;
                dalLayer.AddUser(user);
            }
            catch (DO.DuplicateIdException ex)
            {
                throw new BO.DuplicateIdException(ex.ID, ex.EntityName, "this id isnt correct");
            }
            catch (DO.EntityHasBeenDeleted ex)
            {
                throw new BO.MissingIdException(ex.ID, ex.EntityName, "this parcel isnt existe anymore in the system");
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
        public void DeleteUser(int id)
        {
            try
            {
                DeleteCustomer(id);
                dalLayer.DeleteUser(id);
            }
            catch(EntityHasBeenDeleted ex)
            {
                throw new BO.EntityHasBeenDeleted(id,"users" ,"this user has been deleted already",ex);
            }
        }
    }
}
