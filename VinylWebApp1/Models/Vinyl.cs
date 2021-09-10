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
        [Display(Name = "Opis")]
        public string Description { get; set; }
        [Display(Name = "Zdjęcie")]
        public string Photo { get; set; }
        [Display(Name = "Kategoria")]
        public string Category  { get; set; }
        [Range(1887,2021)]
        [Display(Name = "Rok")]
        public int Year { get; set; }

        public ICollection<VinylReservation> VinylReservations { get; set; }
    }
}
