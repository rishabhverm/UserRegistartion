using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.bussiness.DTO
{
    public class AdminDTO
    {
        [Required]
        public string AdminName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
