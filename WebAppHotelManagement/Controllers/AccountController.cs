// Form verilerini alır, doğrular, veritabanında kontrol eder
// ve sonuç olarak kullanıcıyı yönlendirir veya hata mesajları ile aynı view'ı tekrar gösterir.
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebAppHotelManagement.Models;
using WebAppHotelManagement.ViewModel;

namespace WebAppHotelManagement.Controllers
{
    public class AccountController : Controller
    {
        private HotelDBEntities1 db = new HotelDBEntities1();

        //Erisim saglanir demek
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //ValidateAntiForgeryToken'ın amacı guvenlik.
        //Sunucu, bir form oluşturulduğunda benzersiz bir anti-forgery token (doğrulama token'ı) oluşturur ve bu token'ı formun içine ekler.
        //Form gönderildiğinde, sunucu bu token'ı doğrular ve token geçerliyse isteği işler.
        //PasswordHash ve passwordSalt güvenli bir şekilde şifre saklamak için kullanılır.
        //return View(model): Eğer model doğrulama kurallarına uymuyorsa,
        //aynı view'a model ile birlikte geri döner ve kullanıcıya hata mesajlarını gösterir.

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                
                //Veriler user nesnesine kaydedilir
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = "User",
                    IsActive = true
                };

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login"); //Basrili ise Login yonlendirilir
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt):
        //VerifyPasswordHash metodu, kullanıcının girdiği şifreyi veritabanındaki PasswordHash ve PasswordSalt değerleri ile karşılaştırır.
        //Eğer şifreler uyuşmazsa false döner.
        // SingleOrDefault belirtilen koşula uyan tek bir öğeyi döndürür.
        // Eğer belirtilen koşula uyan öğe yoksa, null döner. Eğer birden fazla öğe bulunursa, bir hata fırlatır.
        //FormsAuthentication.SetAuthCookie kimlik bilgilerini sakalr bir sonraki giris icin
        //retrunView hata yapıldıgında aynı sayfaya gosterilmesini saglıyor

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Email == model.Email); //u fromdaki, model veri tabanı
                if (user == null || !VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View();
                }

                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;   //rastgele deger, her şifre için farklı bir salt oluşturur.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    //herhangi bir byte farklıysa, şifre yanlış demektir ve false döner.
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}

