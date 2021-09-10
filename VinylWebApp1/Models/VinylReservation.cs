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
        [Display(Name = "Płyta")]
        public int VinylId { get; set; }
        [Display(Name = "Użytkownik")]
        public string UserId { get; set; }
        [Range(0,int.MaxValue)]
        [Display(Name = "Rodzaj dostawy")]
        public int DeliveryTypeId { get; set; }
        [Display(Name = "Data rezerwacji")]
        public DateTime ReservationDate { get; set; }
        [Display(Name = "Data zwrotu")]
        public DateTime ReturnDate { get; set; }
        [Display(Name = "Status")]
        public Status Status { get; set; }

        public Vinyl Vinyl { get; set; }
        public DeliveryType DeliveryType { get; set; }
        
        [NotMapped]
        public decimal Price { get => (decimal)((ReturnDate > DateTime.MinValue ? ReturnDate : DateTime.Now) - ReservationDate).TotalDays * 0.7m + (DeliveryType != null ? DeliveryType.Price : 0); }
    }

    public enum Status
    {
        [Display(Name = "Zarezerwowano")]
        Reserved,
        [Display(Name = "Wysłano")]
        Sent,
        [Display(Name = "Zwrócono")]
        Returned,
        [Display(Name = "Anulowano")]
        Cancelled
    }

}
