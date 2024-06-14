//Bu sayfada backend kisimlar yapilir.
//Kullanici verileri girdikten sonra bunu ViewModel klasorunun altindaki RoomViewModel alir.
//Controller bu verileri RoomViewModel'ını cagirrak alir.
//Yani RoomViewModel objRoomViewModel = new RoomViewModel(); kodunu yazarak objRoomViewModel nesnesi olusturuldu.
//Gerekli islemler yapildiktan sonra Entity Framework araciligi ile veri tabanina kaydedilir.
//EntityFramework dedigimiz kisim RoomViewModel objRoomViewModel = new RoomViewModel();
//Ayrica bu kisimda View ve Room klasorunun altinda yazdigimiz Index dosyasinda formData olusturmustuk.
//Bu formDta'nın success oldugunda verecegi mesaji ise JSON yapisina donusturerek yaptik. Bunu da burada yazdik.


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppHotelManagement.Models;
using WebAppHotelManagement.ViewModel;

namespace WebAppHotelManagement.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private HotelDBEntities1 objHotelDbEntities;
        public RoomController()
        {
            objHotelDbEntities = new HotelDBEntities1();
        }

        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objHotelDbEntities.BookingStatus
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.BookingStatus,
                                                        Value = obj.BookingStatusId.ToString(),
                                                    }).ToList();

            objRoomViewModel.ListOfRoomType = (from obj in objHotelDbEntities.RoomTypes
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoomTypeName,
                                                   Value = obj.RoomTypeId.ToString(),
                                               }).ToList();
            return View(objRoomViewModel);
        }


        //HttpPost ile suncuya gönderiyoruz verileri, o da veri tabanına 
        //ActionResult ile cesitli eylem sonuclari donebilir. JsonResult, ViewResult vb.
        //RoomId 0 ise oda eklenecek demek
        [HttpPost]
        public ActionResult SaveRoom(RoomViewModel objRoomViewModel)
        {
            string message = string.Empty;
            string ImageUniqueName = String.Empty;
            string ActualImageName = String.Empty;

            
            if (objRoomViewModel.RoomId == 0)
            {
                //Resim yukleme ve benzersiz ad olusturma
                if (objRoomViewModel.Image != null)
                {
                    ImageUniqueName = Guid.NewGuid().ToString();
                    ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                    objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));
                }

                //Oda kaydi yapip veri tabanina ekleme
                Room objRoom = new Room()
                {
                    RoomNumber = objRoomViewModel.RoomNumber,
                    RoomDescription = objRoomViewModel.RoomDescription,
                    RoomPrice = objRoomViewModel.RoomPrice,
                    BookingStatusId = objRoomViewModel.BookingStatusId,
                    IsActive = true,
                    RoomImage = ActualImageName,
                    RoomCapacity = objRoomViewModel.RoomCapacity,
                    RoomTypeId = objRoomViewModel.RoomTypeId
                };

                objHotelDbEntities.Rooms.Add(objRoom);
                message = "Added.";
            }

            //Burada mevcut oda guncelleniyor demek. Oda Id 0 degil.
            else
            {
                Room objRoom = objHotelDbEntities.Rooms.Single(model => model.RoomId == objRoomViewModel.RoomId);
                //Resim guncellenmesi
                if (objRoomViewModel.Image != null)
                {
                    ImageUniqueName = Guid.NewGuid().ToString();
                    ActualImageName = ImageUniqueName + Path.GetExtension(objRoomViewModel.Image.FileName);
                    objRoomViewModel.Image.SaveAs(Server.MapPath("~/RoomImages/" + ActualImageName));
                    objRoom.RoomImage = ActualImageName;
                }
                //Oda bilgilerinin guncellenmesi
                objRoom.RoomNumber = objRoomViewModel.RoomNumber;
                objRoom.RoomDescription = objRoomViewModel.RoomDescription;
                objRoom.RoomPrice = objRoomViewModel.RoomPrice;
                objRoom.BookingStatusId = objRoomViewModel.BookingStatusId;
                objRoom.IsActive = true;
                objRoom.RoomCapacity = objRoomViewModel.RoomCapacity;
                objRoom.RoomTypeId = objRoomViewModel.RoomTypeId;
                message = "Updated.";
            }

            objHotelDbEntities.SaveChanges();
            return Json(new { message = $"Room Successfully {message}", success = true }, JsonRequestBehavior.AllowGet);
        }

        //Oda verileir alir ve partialview doner.
        //objHotelDbEntities.Rooms tablosundan objRoom adında bir değişkenle başlayarak sorgu başlatılır.
        //join'in ilkini detayli aciklayalim.Once sunu bil ki join ile birlestirme yapiyoruz.
        //objBooking in objHotelDbEntities.BookingStatus ile BookingStatus tablosu icin objBooking adli bir degisken olusturuldu.
        //on objRoom.BookingStatusId equals objBooking.BookingStatusId ile objRoom yani Rooms tablosundaki BookingStatusId ile
        //objBooking yani BookingStatus tablosundaki BookingStatusId birlestirildi
        //Rooms tablosundaki RoomTypeId ile RoomTypes tablosundaki RoomTypeId eşleştirilir ve objRoomType değişkeni oluşturulur.
        //where ile isActive true olanlar filtrelenir
        //Rooms, BookingStatus ve RoomTypes tablolarından alınan veriler, RoomDetailsViewModel adlı view modele dönüştürülür.
        //toList ile listOfRoomDetailsViewModels adli degiskene atanir.
        public PartialViewResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailsViewModels =
                (from objRoom in objHotelDbEntities.Rooms
                 join objBooking in objHotelDbEntities.BookingStatus on objRoom.BookingStatusId equals objBooking.BookingStatusId
                 join objRoomType in objHotelDbEntities.RoomTypes on objRoom.RoomTypeId equals objRoomType.RoomTypeId
                 where objRoom.IsActive == true
                 select new RoomDetailsViewModel()
                 {
                     RoomNumber = objRoom.RoomNumber,
                     RoomDescription = objRoom.RoomDescription,
                     RoomCapacity = objRoom.RoomCapacity,
                     RoomPrice = objRoom.RoomPrice,
                     BookingStatus = objBooking.BookingStatus,
                     RoomType = objRoomType.RoomTypeName,
                     RoomImage = objRoom.RoomImage,
                     RoomId = objRoom.RoomId,
                 }).ToList();
            return PartialView("_RoomDetailsPartial", listOfRoomDetailsViewModels);
        }

        //model Rooms tablosundaki her bir satiri ifade der.
        //Single ifadesi sadece bir ifade olmali. Olmamasi veya birden fazla durumunda hata olusur.
        //Rooms tablosundaki roomId ile web sayfasindaki roomId ifadesi eslestirilir.
        //Eger eslestirme olup tek ise roomId'si ile beraber o satidakiler JSON yapisina donusturulur.
        //HttpGet ile sunucdan istiyoruz. Suncuda veri tabanından verileri alir. Burada da edit de sunucudan istedik.
        [HttpGet]
        public JsonResult EditRoomDetails(int roomId)
        {
            var result = objHotelDbEntities.Rooms.Single(model => model.RoomId == roomId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Veri tabaninda isActive false yapilir. 
        [HttpGet]
        public JsonResult DeleteRoomDetails(int roomId)
        {
            Room objRoom = objHotelDbEntities.Rooms.Single(model => model.RoomId == roomId);
            objRoom.IsActive = false;
            objHotelDbEntities.SaveChanges();
            return Json(new { message = "Record Successfully Deleted", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
