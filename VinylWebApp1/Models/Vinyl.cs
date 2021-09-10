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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Category  { get; set; }
        public int Year { get; set; }

        public ICollection<VinylReservation> VinylReservations { get; set; }
    }
}
