using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VinylWebApp1.Models
{
    public class DeliveryType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Days { get; set; }
        [Range(0,double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<VinylReservation> VinylReservations { get; set; }

        [NotMapped]
        public string Display { get => $"{Name} ({Price} zł.)"; }
    }
}
