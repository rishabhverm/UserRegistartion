using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.bussiness.DTO
{
    public class UserDTO
    {
        [Required, MaxLength(100)]
        public string UserName { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Region { get; set; } = string.Empty;
    }
}
