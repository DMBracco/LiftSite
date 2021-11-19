using LiftSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiftSite.Domain.IRepository
{
    public interface IUserRepository
    {
        public bool CreateUser(User data);
        public bool EditUser(User data);
        public bool DeleteUser(int id);
        public IEnumerable<User> GetListUser();
        public User GetUser(int id);
        public User LoginUser(User data);
    }
}
