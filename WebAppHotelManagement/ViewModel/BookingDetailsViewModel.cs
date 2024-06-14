using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppHotelManagement.ViewModel
{
    public class BookingDetailsViewModel
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public int AssignRoomId { get; set; }
        public int NoOfMembers { get; set; }
        public decimal TotalAmount { get; set; }
    }
}