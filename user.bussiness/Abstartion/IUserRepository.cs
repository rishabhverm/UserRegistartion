using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user.bussiness.Entity;

namespace user.bussiness.Abstartion
{
    public interface IUserRepository
    {
        public Task<IEnumerable<Users>> GetAllUsers();
        public Task<Users> GetUserById(int id);
        public Task<Users> RegisterUser(Users users);
    }
}
