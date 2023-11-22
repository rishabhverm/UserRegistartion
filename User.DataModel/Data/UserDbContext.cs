using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user.bussiness.Entity;

namespace User.DataModel.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Users> users { get; set; }  
        public DbSet<Admin>admins { get; set; }
    }
}
