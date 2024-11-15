using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using TapHoa.Data;

namespace TapHoa.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/dondathang")]
    public class DondathangController : Controller
    {
        private readonly TaphoaContext _context;

        public DondathangController(TaphoaContext context)
        {
            _context = context;
        }

        [Route("Index")]
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

        [Route("Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var dondathang = await _context.Dondathangs
                .Include(d => d.Chitietdondathangs)
                .Include(d => d.MakhNavigation)
                .Include(d => d.MaptvcNavigation)
                .Include(d => d.MattddhNavigation)
                .FirstOrDefaultAsync(m => m.Maddh == id);

            if (dondathang == null) return NotFound();

            return View(dondathang);
        }

        [Route("EditStatus/{id?}")]
        public async Task<IActionResult> EditStatus(int? id)
        {
            if (id == null) return NotFound();

            var dondathang = await _context.Dondathangs
                .Include(d => d.MattddhNavigation)
                .FirstOrDefaultAsync(d => d.Maddh == id);

            if (dondathang == null) return NotFound();

            ViewBag.TrangThaiList = new SelectList(_context.Trangthaidondathangs, "Mattddh", "Tenttddh");
            return View(dondathang);
        }

        [HttpPost]
        [Route("EditStatus/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStatus(int id, int Mattddh)
        {
            var existingOrder = await _context.Dondathangs.FindAsync(id);
            if (existingOrder == null) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    existingOrder.Mattddh = Mattddh;
                    _context.Dondathangs.Update(existingOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DondathangExists(existingOrder.Maddh)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrangThaiList = new SelectList(_context.Trangthaidondathangs, "Mattddh", "Tenttddh", Mattddh);
            return View(existingOrder);
        }

        private bool DondathangExists(int id)
        {
            return _context.Dondathangs.Any(e => e.Maddh == id);
        }
    }
}
