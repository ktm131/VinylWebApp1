using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VinylWebApp1.Models
{
    public class VinylReservation
    {
        public int Id { get; set; }
        [Range(0, int.MaxValue)]
        public int VinylId { get; set; }
        public string UserId { get; set; }
        [Range(0,int.MaxValue)]
        public int DeliveryTypeId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Status Status { get; set; }

        public Vinyl Vinyl { get; set; }
        public DeliveryType DeliveryType { get; set; }
        
        [NotMapped]
        public decimal Price { get => (decimal)((ReturnDate > DateTime.MinValue ? ReturnDate : DateTime.Now) - ReservationDate).TotalDays * 0.7m + (DeliveryType != null ? DeliveryType.Price : 0); }
    }

    public enum Status
    {
        Reserved,
        Sent,
        Returned,
        Cancelled
    }

}
