using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppHotelManagement.Models;
using WebAppHotelManagement.ViewModel;

namespace WebAppHotelManagement.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private HotelDBEntities1 objHotelDbEntities;
        public BookingController()
        {
            objHotelDbEntities = new HotelDBEntities1();
        }

        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfRooms = (from objRoom in objHotelDbEntities.Rooms
                                               where objRoom.BookingStatusId == 2
                                               select new SelectListItem()
                                               {
                                                   Text = objRoom.RoomNumber,
                                                   Value = objRoom.RoomId.ToString()
                                               }
                                               ).ToList();
            return View(objBookingViewModel);
        }

        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}