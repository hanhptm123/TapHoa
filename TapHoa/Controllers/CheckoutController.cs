using Microsoft.AspNetCore.Mvc;
using TapHoa.Data;
using TapHoa.Helpers;
using TapHoa.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;


namespace TapHoa.Controllers
{
    [Route("Checkout")]
    public class CheckoutController : Controller
    {
        private readonly TaphoaContext _context;

        public CheckoutController(TaphoaContext context)
        {
            _context = context;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var giohang = HttpContext.Session.Get<List<Cartitem>>("GioHang") ?? new List<Cartitem>();

            ViewBag.PaymentMethods = _context.Phuongthucthanhtoans.ToList();
            ViewBag.ShippingOptions = _context.Phuongthucvanchuyens.ToList();

            return View(giohang);
        }

        [HttpPost]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder(string tenkh, string diachi, string sdt, int maptvc, int maptth)
        {

            var matk = User.FindFirst("Matk")?.Value;
            if (string.IsNullOrEmpty(matk))
            {
                return Unauthorized(); 
            }

            int matkInt;
            if (!int.TryParse(matk, out matkInt))
            {
                return Unauthorized();
            }

            var taikhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(tk => tk.Matk == matkInt);

            if (taikhoan == null)
            {
                ModelState.AddModelError("", "Không tìm thấy tài khoản.");
                return RedirectToAction("Index", "Cart");
            }
            var khachhang = await _context.Khachhangs
                .FirstOrDefaultAsync(kh => kh.Matk == taikhoan.Matk);

            if (khachhang == null)
            {
                ModelState.AddModelError("", "Không tìm thấy thông tin khách hàng.");
                return RedirectToAction("Index", "Cart");
            }
            var giohang = HttpContext.Session.Get<List<Cartitem>>("GioHang");
            if (giohang == null || !giohang.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống.");
                return RedirectToAction("Index", "Cart");
            }
            var donDatHang = new Dondathang
            {
                Tenkh = tenkh,
                Diachi = diachi,
                Sdt = sdt,
                Ngaydat = DateTime.Now,
                Maptvc = maptvc,
                Tonggia = giohang.Sum(item => (decimal)item.Giasaugiam * item.Soluong),
                Makh = khachhang.Makh,
                Mattddh = 1 
            };

            _context.Dondathangs.Add(donDatHang);
            await _context.SaveChangesAsync();
            foreach (var item in giohang)
            {
                var chiTiet = new Chitietdondathang
                {
                    Maddh = donDatHang.Maddh,
                    Masp = item.Masanpham,
                    Soluong = item.Soluong,
                    Thanhtien = (decimal)item.Giasaugiam * item.Soluong
                };
                _context.Chitietdondathangs.Add(chiTiet);
            }
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("GioHang");
            return RedirectToAction("OrderConfirmation", new { id = donDatHang.Maddh });
        }

        [Route("OrderConfirmation/{id}")]
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            var donDatHang = await _context.Dondathangs
                .Include(d => d.Chitietdondathangs)
                .ThenInclude(c => c.MaspNavigation)
                .FirstOrDefaultAsync(d => d.Maddh == id);

            if (donDatHang == null)
            {
                return NotFound();
            }

            return View(donDatHang);
        }
    }
}
