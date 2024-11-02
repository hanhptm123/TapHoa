using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;

namespace TapHoa.Controllers
{
    public class DondathangController : Controller
    {
        private readonly TaphoaContext _context;

        public DondathangController(TaphoaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dondathangs = await _context.Dondathangs
                .Include(d => d.Chitietdondathangs)
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaptvcNavigation)
                .Include(d => d.MattddhNavigation)
                .ToListAsync();
            return View(dondathangs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dondathang = await _context.Dondathangs
                .Include(d => d.Chitietdondathangs)
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaptvcNavigation)
                .Include(d => d.MattddhNavigation)
                .FirstOrDefaultAsync(m => m.Maddh == id);

            if (dondathang == null)
            {
                return NotFound();
            }

            return View(dondathang);
        }

        public async Task<IActionResult> EditStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dondathang = await _context.Dondathangs
                .Include(d => d.MattddhNavigation)  // Include trạng thái đơn hàng để hiển thị thông tin hiện tại
                .FirstOrDefaultAsync(d => d.Maddh == id);

            if (dondathang == null)
            {
                return NotFound();
            }

            // Use ViewBag to pass the status list to the view
            ViewBag.TrangThaiList = new SelectList(_context.Trangthaidondathangs, "Mattddh", "Tenttddh");
            return View(dondathang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatus(int id, [Bind("Maddh,Mattddh")] Dondathang dondathang)
        {
            if (id != dondathang.Maddh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingOrder = await _context.Dondathangs.FindAsync(id);
                    if (existingOrder != null)
                    {
                        existingOrder.Mattddh = dondathang.Mattddh;
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DondathangExists(dondathang.Maddh))
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

            // Reassign the status list to ViewBag in case of validation failure
            ViewBag.TrangThaiList = new SelectList(_context.Trangthaidondathangs, "Mattddh", "Tenttddh", dondathang.Mattddh);
            return View(dondathang);
        }

        private bool DondathangExists(int id)
        {
            return _context.Dondathangs.Any(e => e.Maddh == id);
        }
    }
}
