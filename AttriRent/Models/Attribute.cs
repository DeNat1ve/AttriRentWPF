using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttriRent.Models
{
    internal class Attribute
    {
        [Key]
        public int id { get; set; }
        [MaxLength(40)]
        public string name { get; set; } = null!;
        public int price { get; set; }
        [MaxLength(200)]
        public string? description { get; set; }
        [MaxLength(500)]
        public string? image_path { get; set; }

        public List<Order>? orders { get; set; }
    }
}
