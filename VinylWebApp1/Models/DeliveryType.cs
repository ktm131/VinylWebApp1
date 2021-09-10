using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VinylWebApp1.Models
{
    public class DeliveryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Days { get; set; }
        public decimal Price { get; set; }

        public ICollection<VinylReservation> VinylReservations { get; set; }
    }
}
