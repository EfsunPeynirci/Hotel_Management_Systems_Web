using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppHotelManagement.ViewModel
{
    public class BookingViewModel
    {
        [Required(ErrorMessage = "Customer Name is required.")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Customer Address is required.")]
        [Display(Name = "Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Booking From")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Booking From is required.")]
        public DateTime BookingFrom { get; set; }

        [Display(Name = "Booking To")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Booking To is required.")]
        public DateTime BookingTo { get; set; }

        [Display(Name = "Assign Room")]
        [Required(ErrorMessage = "Assign Room is required.")]
        public int AssignRoomId { get; set; }

        [Display(Name = "Number of Members")]
        [Required(ErrorMessage = "Number of Members is required.")]
        public int NumberOfMembers { get; set; }

        public IEnumerable<SelectListItem> ListOfRooms { get; set; }
    }
}