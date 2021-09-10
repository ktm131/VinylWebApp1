using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VinylWebApp1.Models
{
    public class Vinyl
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Category  { get; set; }
        [Range(1887,2021)]
        public int Year { get; set; }

        public ICollection<VinylReservation> VinylReservations { get; set; }
    }
}
