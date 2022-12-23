using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttriRent.Models
{
    [Table("user_password")]
    internal class UserPassword
    {
        public int id { get; set; }
        public string password { get; set; } = null!;
        [ForeignKey("user_id")]
        public User user { get; set; }
    }
}
