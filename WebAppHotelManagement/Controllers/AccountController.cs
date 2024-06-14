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

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

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

                return RedirectToAction("Login");
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Email == model.Email);
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
                passwordSalt = hmac.Key;
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
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}

