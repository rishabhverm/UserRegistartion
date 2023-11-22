using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.bussiness.Entity
{
    public  class Admin
    {
            [Key]
            public int AdminId { get; set; }
            public string AdminName { get; set; }
            public string Password { get; set; }
    }
}
