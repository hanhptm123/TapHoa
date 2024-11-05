using Microsoft.AspNetCore.Mvc;
using TapHoa.Data;
using TapHoa.Helpers;
using TapHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TapHoa.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly TaphoaContext _context;

        public CheckoutController(TaphoaContext context)
        {
            _context = context;
        }

        // Property to get the current cart from session
        public List<Cartitem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<Cartitem>>("GioHang");
                return data ?? new List<Cartitem>();
            }
        }

        // Method to display the checkout page
        public IActionResult Index()
        {
            var cartItems = Carts;
            double totalAmount = cartItems.Sum(item => item.Giasaugiam * item.Soluong);
            ViewBag.TotalAmount = totalAmount;

            // Get payment and shipping methods
            ViewBag.PhuongThucThanhToan = _context.Phuongthucthanhtoans.ToList();
            ViewBag.PhuongThucVanChuyen = _context.Phuongthucvanchuyens.ToList();

            return View(cartItems); // Show checkout page with cart items
        }

        // Method to process the checkout
        [HttpPost]
        public IActionResult PlaceOrder(string tenkh, string diachi, string sdt, int mapttt, int maptvc)
        {
            var cartItems = Carts;
            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "CartItem"); // Redirect if cart is empty
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Create new order (Dondathang)
                var dondathang = new Dondathang
                {

                    Tenkh = tenkh,
                    Diachi = diachi,
                    Sdt = sdt,
                    Ngaydat = DateTime.Now,
                    Tonggia = (decimal)cartItems.Sum(item => item.Giasaugiam * item.Soluong),
                    Makh = 1, // Assign user ID (assuming 1 for demo; update as needed)
                    Maptvc = maptvc, // Save the shipping method
                    Mattddh = 1 // Set the initial status of the order (e.g., 1 = pending)
                };

                _context.Dondathangs.Add(dondathang);
                _context.SaveChanges();

                // Add order details (Chitietdondathang) for each item in the cart
                foreach (var cartItem in cartItems)
                {
                    var chitiet = new Chitietdondathang
                    {
                        Maddh = dondathang.Maddh,
                        Masp = cartItem.Masanpham,
                        Soluong = cartItem.Soluong,
                        Thanhtien = (decimal)(cartItem.Giasaugiam * cartItem.Soluong)
                    };
                    _context.Chitietdondathangs.Add(chitiet);
                }

                _context.SaveChanges();

                // Create an invoice (Hoadon) with the selected payment method
                var hoadon = new Hoadon
                {
                    Maptvc = maptvc, // Lưu phương thức vận chuyển
                    Mapttt = mapttt, // Lưu phương thức thanh toán
                    Ngaythanhtoan = DateTime.Now,
                    Manv = 1 // Đặt ID nhân viên (giả sử là 1 cho bản demo; cập nhật khi cần)
                };

                _context.Hoadons.Add(hoadon);
                _context.SaveChanges();

                transaction.Commit();

                // Clear the cart after checkout
                HttpContext.Session.Remove("GioHang");

                return RedirectToAction("Success"); // Redirect to success page
            }
            catch
            {
                transaction.Rollback();
                return RedirectToAction("Index"); // If error, reload checkout page
            }
        }

        // Display success page after successful checkout
        public IActionResult Success()
        {
            return View(); // Show success page
        }
    }
}
