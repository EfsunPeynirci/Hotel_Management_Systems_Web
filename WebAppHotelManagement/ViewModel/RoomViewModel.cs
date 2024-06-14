﻿//RoomViewmodel sayfasinda amacimiz frontend kismindan yani kullanicidan verileri almak ve islemek
//Ornegin kullanici Add Room butonuna basip yazdiktan sonra veriler RoomViewModel ile okunur ve alinir.
//Daha sonrasinda ise RoomViewModel'daki veriler RoomController'a gonderilir.
//RoomController'da bu veriler RoomViewModel nesnesi ile alinir. RoomViewModel objRoomViewModel = new RoomViewModel();

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppHotelManagement.ViewModel
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }


        //Display yazmasaydık web sitesinde add room dedikten sonra acilan sayfada RoomNo bitisik gozukecekti.
        //Display ile ayri ayri yazilmis oldu.
        [Display(Name = "Room No.")]
        [Required(ErrorMessage = "Room number is required.")]
        public string RoomNumber { get; set; }


        [Display(Name = "Room Image")]
        public string RoomImage { get; set; }


        [Display(Name = "Room Price")]
        [Required(ErrorMessage = "Room Price is required.")]
        [Range(25, 10000, ErrorMessage = "Room price should be equal and greater then {0}")]
        public decimal RoomPrice { get; set; }


        [Display(Name = "Booking Status")]
        [Required(ErrorMessage = "Booking Status is required.")]
        public int BookingStatusId { get; set; }


        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "Room type is required.")]
        public int RoomTypeId { get; set; }


        [Display(Name = "Room Capacity")]
        [Required(ErrorMessage = "Room capacity is required.")]
        [Range(1, 8, ErrorMessage = "Room Capacity should be equal and greater then {1}")]
        public int RoomCapacity { get; set; }


        public HttpPostedFileBase Image { get; set; }


        [Display(Name = "Room Description")]
        [Required(ErrorMessage = "Room description is required.")]
        public string RoomDescription { get; set; }


        public List<SelectListItem> ListOfBookingStatus { get; set; }
        public List<SelectListItem> ListOfRoomType { get; set; }

    }
}