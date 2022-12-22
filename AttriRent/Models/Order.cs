using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttriRent.Models
{
    internal class Order
    {
        [Key]
        public int id { get; set; }
        [MaxLength(200)]
        public string? description { get; set; }
        public DateTime order_start_day { get; set; }
        public DateTime order_end_day { get; set; }
        public bool activity_status { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; } = null!;
        [ForeignKey("attribute_id")]
        public Attribute Attribute { get; set; } = null!;
    }
}
