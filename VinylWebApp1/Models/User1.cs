using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VinylWebApp1.Models
{
    public class User1
    {
        public string Id { get; set; }
        [Display(Name="Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
    }
}
