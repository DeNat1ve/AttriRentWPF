using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttriRent.Models
{
    internal class User
    {
        [Key]
        public int id { get; set; }
        [MaxLength(20)]
        public string name { get; set; } = null!;
        [MaxLength(40)]
        public string email { get; set; } = null!;
        [MaxLength(50)]
        public string password { get; set; } = null!;
        public int user_role { get; set; }

        public List<Order>? orders { get; set; }
    }
}
