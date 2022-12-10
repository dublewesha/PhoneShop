using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneShop.Models
{
    public class Phone
    {
        [Column(Order = 0)]
        public int Id { get; set; }
        [Column(Order = 1)]
        [Required]
        public string Name { get; set; }
        [Column(Order = 2)]
        public string PhotoPath { get; set; }
        [Column(Order = 3)]
        public string PhotoPath1 { get; set; }
        [Column(Order = 4)]
        public string PhotoPath2 { get; set; }
        [Column(Order = 5)]
        [Required]
        public int Capacity { get; set; }
        [Column(Order = 6)]
        [Required]
        public string Color { get; set; }
        [Column(Order = 7)]
        [Required]
        public decimal Price { get; set; }

    }
}
