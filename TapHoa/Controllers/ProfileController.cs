using Microsoft.AspNetCore.Mvc;
using TapHoa.Data;
using Microsoft.EntityFrameworkCore;

namespace TapHoa.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profile/Index
        public async Task<IActionResult> Index()
        {
            // Giả sử có phương thức để lấy ID của người dùng hiện tại
            var userId = GetCurrentUserId(); 
            var khachhang = await _context.Khachhangs.FirstOrDefaultAsync(kh => kh.Matk == userId);
            
            if (khachhang == null)
            {
                return NotFound("Không tìm thấy thông tin khách hàng.");
            }

            return View(khachhang);
        }

        // GET: Profile/Edit
        public async Task<IActionResult> Edit()
        {
            var userId = GetCurrentUserId(); 
            var khachhang = await _context.Khachhangs.FirstOrDefaultAsync(kh => kh.Matk == userId);

            if (khachhang == null)
            {
                return NotFound("Không tìm thấy thông tin khách hàng.");
            }

            return View(khachhang);
        }

        // POST: Profile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Tenkh,Email,Sdt,Diachi")] Khachhang khachhang)
        {
            var userId = GetCurrentUserId();
            var currentProfile = await _context.Khachhangs.FirstOrDefaultAsync(kh => kh.Matk == userId);

            if (currentProfile == null)
            {
                return NotFound("Không tìm thấy thông tin khách hàng.");
            }

            if (ModelState.IsValid)
            {
                currentProfile.Tenkh = khachhang.Tenkh;
                currentProfile.Email = khachhang.Email;
                currentProfile.Sdt = khachhang.Sdt;
                currentProfile.Diachi = khachhang.Diachi;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
                return RedirectToAction(nameof(Index));
            }

            return View(khachhang);
        }

        private int GetCurrentUserId()
        {
            // Logic để lấy ID của người dùng hiện tại, ví dụ lấy từ session hoặc claim.
            return int.Parse(User.FindFirst("UserId").Value);
        }
    }
}
