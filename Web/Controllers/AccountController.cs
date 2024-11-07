using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Data.Contexts;
using Web.Data.Models.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private DBContext _db;

        public AccountController(DBContext db)
        {
            _db = db;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public IActionResult Register(User user)
        {

            if (ModelState.IsValid)
            {
                User newUser = new User();

                newUser.UserName = user.UserName;
                newUser.Email = user.Email;
                newUser.Password = user.Password;
                newUser.IsAdmin = false;

                _db.Users.Add(newUser);
                _db.SaveChanges();

                return Redirect("/");
            }

            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var oldUser = _db.Users.FirstOrDefault(u => u.Email == user.Email);

                if (oldUser != null)
                {
                    if (oldUser.Password == user.Password)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,oldUser.UserName),
                            new Claim(ClaimTypes.Email,oldUser.Email),
                            new Claim(ClaimTypes.NameIdentifier,oldUser.Id.ToString())
                        };

                        var identity = new ClaimsIdentity(claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };

                         HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identity),
                            properties);

                        return Redirect("/");
                    }

                    ViewBag.result = "کلمه عبور اشتباه است";
                }
                ViewBag.result = "کاربری یافت نشد!";
            }

            return Redirect("/");
        }

    }
}
