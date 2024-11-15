using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;

namespace TapHoa.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/phieunhap")]
    public class PhieuNhapController : Controller
    {
        private readonly TaphoaContext _context;

        public PhieuNhapController(TaphoaContext context)
        {
            _context = context;
        }

        private void LoadViewBags(int? selectedManv = null)
        {
            ViewBag.ManvList = new SelectList(_context.Nhanviens, "Manv", "Tennv", selectedManv);
            ViewBag.MaspList = new SelectList(_context.Sanphams, "Masp", "Tensp");
            ViewBag.ProductPrices = _context.Sanphams.ToDictionary(p => p.Masp, p => p.Gia);
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var phieunhaps = _context.Phieunhaps.Include(p => p.ManvNavigation);
            return View(await phieunhaps.ToListAsync());
        }

        [Route("Create")]
        public IActionResult Create()
        {
            LoadViewBags();
            var phieuNhap = new Phieunhap
            {
                Ngaynhap = DateTime.Now
            };
            return View(phieuNhap);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Phieunhap phieunhap, List<Ctphieunhap> ctPhieuNhaps)
        {
            if (ModelState.IsValid)
            {
                phieunhap.Tongtien = phieunhap.Tongtien;

                foreach (var ct in ctPhieuNhaps)
                {
                    var product = await _context.Sanphams.FindAsync(ct.Masp);
                    if (product != null)
                    {
                        ct.Thanhtien = ct.Soluong * product.Gia;
                        ct.Mapn = phieunhap.Mapn;
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Không tìm thấy sản phẩm với ID {ct.Masp}.");
                    }
                }

                _context.Phieunhaps.Add(phieunhap);
                await _context.SaveChangesAsync();

                foreach (var ct in ctPhieuNhaps)
                {
                    ct.Mapn = phieunhap.Mapn;
                }

                _context.Ctphieunhaps.AddRange(ctPhieuNhaps);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadViewBags(phieunhap.Manv);
            return View(phieunhap);
        }

        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieunhap = await _context.Phieunhaps
                .Include(p => p.Ctphieunhaps)
                .FirstOrDefaultAsync(m => m.Mapn == id);

            if (phieunhap == null)
            {
                return NotFound();
            }

            LoadViewBags(phieunhap.Manv);
            ViewBag.CtPhieuNhaps = phieunhap.Ctphieunhaps.ToList();

            return View(phieunhap);
        }

        [HttpPost]
        [Route("Edit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Phieunhap phieunhap, List<Ctphieunhap> ctPhieuNhaps)
        {
            if (id != phieunhap.Mapn)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieunhap);
                    await _context.SaveChangesAsync();

                    var existingCtPhieuNhaps = await _context.Ctphieunhaps.Where(c => c.Mapn == phieunhap.Mapn).ToListAsync();
                    _context.Ctphieunhaps.RemoveRange(existingCtPhieuNhaps);

                    foreach (var ct in ctPhieuNhaps)
                    {
                        var product = await _context.Sanphams.FindAsync(ct.Masp);
                        if (product != null)
                        {
                            ct.Thanhtien = ct.Soluong * product.Gia;
                            ct.Mapn = phieunhap.Mapn;
                            _context.Ctphieunhaps.Add(ct);
                        }
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieunhapExists(phieunhap.Mapn))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            LoadViewBags(phieunhap.Manv);
            ViewBag.CtPhieuNhaps = ctPhieuNhaps;
            return View(phieunhap);
        }

        [Route("Delete/{id?}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieunhap = await _context.Phieunhaps
                .Include(p => p.ManvNavigation)
                .FirstOrDefaultAsync(m => m.Mapn == id);
            if (phieunhap == null)
            {
                return NotFound();
            }

            return View(phieunhap);
        }

        [HttpPost, ActionName("Delete")]
        [Route("Delete/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phieunhap = await _context.Phieunhaps.FindAsync(id);
            if (phieunhap != null)
            {
                _context.Phieunhaps.Remove(phieunhap);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PhieunhapExists(int id)
        {
            return _context.Phieunhaps.Any(e => e.Mapn == id);
        }
    }
}
