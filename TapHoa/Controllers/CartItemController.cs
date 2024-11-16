using Microsoft.AspNetCore.Mvc;
using TapHoa.Data;
using TapHoa.Helpers;
using TapHoa.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TapHoa.Controllers
{
    public class CartItemController : Controller
    {
        private readonly TaphoaContext _context;

        public CartItemController(TaphoaContext context)
        {
            _context = context;
        }

        public List<Cartitem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<Cartitem>>("GioHang");
                return data ?? new List<Cartitem>();
            }
        }

        private void UpdateCartSummary()
        {
            var cartItems = Carts;
            ViewBag.CartItemCount = cartItems.Sum(item => item.Soluong); // Total items
            ViewBag.TotalCartAmount = cartItems.Sum(item => item.Giasaugiam * item.Soluong); // Total amount
        }

        [HttpPost]
        public JsonResult AddToCart(int id, int soluong = 1)
        {
            var giohang = Carts;
            var sanpham = _context.Sanphams
                .Include(p => p.MakmNavigation)
                .SingleOrDefault(p => p.Masp == id);

            if (sanpham == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại." });
            }

            if (sanpham.Soluong < soluong)
            {
                return Json(new { success = false, message = $"Số lượng trong kho còn lại là {sanpham.Soluong}. Vui lòng chọn số lượng nhỏ hơn hoặc bằng số này." });
            }

            var item = giohang.SingleOrDefault(p => p.Masanpham == id);
            double giaSauGiam = (double)sanpham.Gia;
            decimal discountPercentage = (decimal)(sanpham.MakmNavigation?.Phantramgiam ?? 0);

            if (discountPercentage > 0)
            {
                giaSauGiam *= (1 - (double)discountPercentage / 100.0);
            }

            if (item == null)
            {
                item = new Cartitem
                {
                    Masanpham = id,
                    Tensanpham = sanpham.Tensp,
                    Giasaugiam = giaSauGiam,
                    Soluong = soluong,
                    Hinh = sanpham.Hinhanh,
                    Discount = discountPercentage,
                    Giagoc = (double)sanpham.Gia
                };
                giohang.Add(item);
            }
            else
            {
                item.Soluong += soluong;
            }

            HttpContext.Session.Set("GioHang", giohang);
            UpdateCartSummary(); // Update cart summary
            return Json(new { success = true, message = "Sản phẩm đã được thêm vào giỏ hàng." });
        }
        public IActionResult RemoveFromCart(int MASP)
        {
            var giohang = Carts;
            var item = giohang.SingleOrDefault(p => p.Masanpham == MASP);

            if (item != null)
            {
                giohang.Remove(item);
                HttpContext.Session.Set("GioHang", giohang);
                UpdateCartSummary(); // Update cart summary
            }
            else
            {
                TempData["ErrorMessage"] = "Product not found.";
            }

            return RedirectToAction("Index", "CartItem");
        }

        public IActionResult Index()
        {
            var cartItems = Carts;
            UpdateCartSummary(); // Update cart summary
            return View(cartItems);
        }
    }
}