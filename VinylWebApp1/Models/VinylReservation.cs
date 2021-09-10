using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VinylWebApp1.Models
{
    public class VinylReservation
    {
        public int Id { get; set; }
        public int VinylId { get; set; }
        public string UserId { get; set; }
        public int DeliveryTypeId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Status Status { get; set; }

        public Vinyl Vinyl { get; set; }
        public DeliveryType DeliveryType { get; set; }
        
        [NotMapped]
        public decimal Price { get => (decimal)(ReservationDate - ReturnDate).TotalDays * 0.7m + DeliveryType.Price; }
    }

    public enum Status
    {
        Reserved,
        Sent,
        Returned,
        Cancelled
    }

}
