using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TapHoa.Interfaces;
using TapHoa.Models;
using TapHoa.Data; 

namespace TapHoa.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private IHttpContextAccessor _httpContextAccessor;

        public LoginController(IUserService userService, IAddressService addressService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _addressService = addressService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Authenticate(model.Username, model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("Role", user.Role.ToString());
                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                _userService.Register(model);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (int.TryParse(userIdString, out int userId))
            {
                var user = _userService.GetUserById(userId);
                if (user != null)
                {
                    return View(user);
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Profile(User model)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdateUserProfile(model);
                return RedirectToAction("Profile");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DiaChi()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (int.TryParse(userIdString, out int userId))
            {
                var addresses = _addressService.GetAddressesByUserId(userId);
                return View(addresses);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult AddDiaChi([FromBody] SoDiaChi newAddress)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (int.TryParse(userIdString, out int userId))
            {
                newAddress.UserId = userId;
                _addressService.AddAddress(newAddress);
                return RedirectToAction(nameof(DiaChi));
            }
            return RedirectToAction("Login");
        }
    }
}
