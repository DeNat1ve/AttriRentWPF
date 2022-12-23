using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttriRent.Models
{
    [Table("user_role")]
    internal class UserRole
    {
        public int id { get; set; }
        public int role { get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
    }
}
