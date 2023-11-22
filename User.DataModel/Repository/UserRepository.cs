using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user.bussiness.Abstartion;
using user.bussiness.Entity;
using User.DataModel.Data;

namespace User.DataModel.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly UserDbContext _dbcontext;

        public UserRepository(UserDbContext userDbContext)
        {
            _dbcontext = userDbContext;
        }
        public async Task<Users> RegisterUser(Users users)
        {
            await _dbcontext.users.AddAsync(users);
            _dbcontext.SaveChanges();
            return users;
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await _dbcontext.users.ToListAsync();
        }

        public async Task<Users> GetUserById(int id)
        {
            return await _dbcontext.users.Where(e => e.UserId == id).FirstOrDefaultAsync();
        }
    }
}
