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

        // Property to get the current cart from session
        public List<Cartitem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<Cartitem>>("GioHang");
                return data ?? new List<Cartitem>();
            }
        }

        // Method to add items to the cart
        public IActionResult AddToCart(int id, int soluong = 1)
        {
            var giohang = Carts;
            var item = giohang.SingleOrDefault(p => p.Masanpham == id);

            if (item == null && soluong > 0)
            {
                var sanpham = _context.Sanphams
                    .Include(p => p.MakmNavigation) // Include the discount navigation property
                    .SingleOrDefault(p => p.Masp == id);

                if (sanpham != null)
                {
                    // Get the price and discount percentage
                    double giaSauGiam = (double)sanpham.Gia;
                    decimal discountPercentage = (decimal)(sanpham.MakmNavigation?.Phantramgiam ?? 0);
                    // Calculate the price after applying the discount
                    if (discountPercentage > 0)
                    {
                        giaSauGiam *= (1 - (double)discountPercentage / 100.0);
                    }

                    // Create a new cart item with the calculated values
                    item = new Cartitem
                    {
                        Masanpham = id,
                        Tensanpham = sanpham.Tensp,
                        Giasaugiam = giaSauGiam, // Price after discount
                        Soluong = soluong,
                        Hinh = sanpham.Hinhanh,
                        Discount = discountPercentage,
                        Giagoc = (double)sanpham.Gia// Store the discount percentage directly
                    };
                    giohang.Add(item);
                }
            }
            else if (item != null && soluong > 0)
            {
                item.Soluong += soluong; // Increment quantity if item exists
            }

            HttpContext.Session.Set("GioHang", giohang); // Update session
            return RedirectToAction("Index"); // Redirect to Index action
        }

        // Method to display the cart
        public IActionResult Index()
        {
            var cartItems = Carts;

            // Calculate total amount after applying discount for all items in the cart
            double totalAmount = cartItems.Sum(item => item.Giasaugiam * item.Soluong);

            ViewBag.TotalAmount = totalAmount; // Pass total amount to view

            return View(cartItems); // Return the view with cart items
        }
    }
}
