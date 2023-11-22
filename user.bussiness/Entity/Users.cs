using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.bussiness.Entity
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string City  { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

    }
}
