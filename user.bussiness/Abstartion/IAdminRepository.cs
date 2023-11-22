using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user.bussiness.Entity;

namespace user.bussiness.Abstartion
{
    public interface IAdminRepository
    {
        public Task<Admin> RegisterAdmin(Admin admin);
        public Task<Admin> LoggingAdmin(Admin admin);
    }
}
