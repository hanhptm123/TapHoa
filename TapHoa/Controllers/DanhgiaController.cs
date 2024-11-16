using Microsoft.AspNetCore.Mvc;
using TapHoa.Data;
using Microsoft.EntityFrameworkCore;

namespace TapHoa.Controllers
{
    public class DanhgiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DanhgiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Danhgia/Index
        public IActionResult Index()
        {
            // Lấy tất cả các đánh giá
            var danhgiaList = _context.Danhgium.Include(d => d.MakhNavigation).Include(d => d.MaspNavigation).ToList();
            return View(danhgiaList);
        }

        // GET: Danhgia/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var danhgia = await _context.Danhgium
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Madg == id);
            if (danhgia == null)
            {
                return NotFound();
            }

            return View(danhgia);
        }

        // GET: Danhgia/Create
        public IActionResult Create(int masp)
        {
            var userId = User.Identity.Name; // Lấy ID người dùng hiện tại
            var khachHang = _context.Khachhang.FirstOrDefault(kh => kh.Email == userId); // Giả sử dùng Email để tìm Khách hàng

            // Kiểm tra xem khách hàng có mua sản phẩm này chưa
            var donHang = _context.Dondathang
                .FirstOrDefault(dh => dh.Makh == khachHang?.Makh && dh.Mattddh == 1); // Kiểm tra trạng thái đơn hàng đã thanh toán (1 = thanh toán)

            if (donHang == null)
            {
                TempData["ErrorMessage"] = "Bạn phải mua sản phẩm này trước khi đánh giá!";
                return RedirectToAction("Index", "Sanpham"); // Chuyển hướng đến trang sản phẩm
            }

            // Nếu khách hàng đã mua sản phẩm, cho phép tạo đánh giá
            ViewBag.Masp = masp;
            return View();
        }

        // POST: Danhgia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Danhgia danhgia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhgia);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Sanpham", new { id = danhgia.Masp });
            }
            return View(danhgia);
        }

        // GET: Danhgia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhgia = await _context.Danhgium.FindAsync(id);
            if (danhgia == null)
            {
                return NotFound();
            }
            return View(danhgia);
        }

        // POST: Danhgia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Danhgia danhgia)
        {
            if (id != danhgia.Madg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhgia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Danhgium.Any(e => e.Madg == danhgia.Madg))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(danhgia);
        }

        // GET: Danhgia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhgia = await _context.Danhgium
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Madg == id);
            if (danhgia == null)
            {
                return NotFound();
            }

            return View(danhgia);
        }

        // POST: Danhgia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var danhgia = await _context.Danhgium.FindAsync(id);
            if (danhgia != null)
            {
                _context.Danhgium.Remove(danhgia);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
