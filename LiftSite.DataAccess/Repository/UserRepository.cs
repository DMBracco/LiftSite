using LiftSite.Domain.Entities;
using LiftSite.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiftSite.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LiftSiteContext context;

        public UserRepository(LiftSiteContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            this.context = context;
        }

        public bool CreateUser(User data)
        {
            context.Users.Add(data);
            context.SaveChanges();
            return true;
        }
        public bool EditUser(User data)
        {
            context.Users.Update(data);
            context.SaveChanges();
            return true;
        }
        public bool DeleteUser(int id)
        {
            var q = context.Users.FirstOrDefault(p => p.Id == id);
            if (q != null)
            {
                context.Users.Remove(q);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public IEnumerable<User> GetListUser()
        {
            var data = context.Users;
            var result = data.ToArray();
            return result;
        }
        public User GetUser(int id)
        {
            var data = context.Users.FirstOrDefault(p => p.Id == id);
            return data;
        }
        public User LoginUser(User data)
        {
            var result = context.Users
                .FirstOrDefault(u => u.Email == data.Email && u.Password == data.Password);
            result.Role = context.Roles.FirstOrDefault(p => p.Id == result.RoleId);

            return result;
        }
    }
}
