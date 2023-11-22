using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using user.bussiness.Abstartion;
using user.bussiness.Entity;
using User.DataModel.Data;

namespace User.DataModel.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserDbContext _dbContext;

        public AdminRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin> LoggingAdmin(Admin admin)
        {
           
            var existingAdmin = await _dbContext.admins
                .Where(a => a.AdminName == admin.AdminName && a.Password == admin.Password)
                .FirstOrDefaultAsync();
                return existingAdmin;
        }

        public async Task<Admin> RegisterAdmin(Admin admin)
        {
            
            if (_dbContext.admins.Any(a => a.AdminName == admin.AdminName))
            {
                return null;
            }

            await _dbContext.admins.AddAsync(admin);
            await _dbContext.SaveChangesAsync();

            return admin;
        }
    }
}
