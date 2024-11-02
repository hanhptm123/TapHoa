using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;

namespace TapHoa.Controllers
{
    public class PhieuNhapController : Controller
    {
        private readonly TaphoaContext _context;

        public PhieuNhapController(TaphoaContext context)
        {
            _context = context;
        }

        // GET: Phieunhap
        public async Task<IActionResult> Index()
        {
            var phieunhaps = _context.Phieunhaps.Include(p => p.ManvNavigation);
            return View(await phieunhaps.ToListAsync());
        }

        // GET: Phieunhap/Create
        public IActionResult Create()
        {
            ViewBag.ManvList = new SelectList(_context.Nhanviens, "Manv", "Tennv");
            ViewBag.MaspList = new SelectList(_context.Sanphams, "Masp", "Tensp"); // List of products
            ViewBag.ProductPrices = _context.Sanphams.ToDictionary(p => p.Masp, p => p.Gia); // Dictionary of product prices
            var phieuNhap = new Phieunhap
            {
                Ngaynhap = DateTime.Now // Set the current date
            };
            return View(phieuNhap);
        }

        // POST: Phieunhap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Phieunhap phieunhap, List<Ctphieunhap> ctPhieuNhaps)
        {
            if (ModelState.IsValid)
            {
                // Initialize the total amount
                phieunhap.Tongtien = 0;

                // Calculate total and prepare details
                foreach (var ct in ctPhieuNhaps)
                {
                    // Find the product by its ID
                    var product = await _context.Sanphams.FindAsync(ct.Masp);
                    if (product != null)
                    {
                        // Calculate total price for this detail
                        ct.Thanhtien = ct.Soluong * product.Gia;

                        // Accumulate total amount
                        phieunhap.Tongtien += ct.Thanhtien;

                        // Set Mapn to the new Phieunhap's ID once it's saved
                        ct.Mapn = phieunhap.Mapn;
                    }
                    else
                    {
                        // Handle the case where the product is not found (optional)
                        ModelState.AddModelError("", $"Product with ID {ct.Masp} not found.");
                    }
                }

                // Add Phieunhap to the context
                _context.Phieunhaps.Add(phieunhap);
                await _context.SaveChangesAsync(); // Save Phieunhap first

                // Now that we have the Mapn, add the details to the context
                foreach (var ct in ctPhieuNhaps)
                {
                    ct.Mapn = phieunhap.Mapn; // Link the detail to the created Phieunhap
                    _context.Ctphieunhaps.Add(ct); // Add detail
                }
                await _context.SaveChangesAsync(); // Save Ctphieunhap records

                return RedirectToAction(nameof(Index));
            }

            // Populate ViewBag again in case validation fails
            ViewBag.ManvList = new SelectList(_context.Nhanviens, "Manv", "Tennv", phieunhap.Manv);
            ViewBag.MaspList = new SelectList(_context.Sanphams, "Masp", "Tensp");
            ViewBag.ProductPrices = _context.Sanphams.ToDictionary(p => p.Masp, p => p.Gia); // Repopulate product prices
            return View(phieunhap);
        }

        // GET: Phieunhap/Edit/{id}
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieunhap = await _context.Phieunhaps
                .Include(p => p.Ctphieunhaps) // Include related Ctphieunhap
                .FirstOrDefaultAsync(m => m.Mapn == id);

            if (phieunhap == null)
            {
                return NotFound();
            }

            // Populate ViewBag for dropdowns
            ViewBag.ManvList = new SelectList(_context.Nhanviens, "Manv", "TenNhanvien", phieunhap.Manv);
            ViewBag.MaspList = new SelectList(_context.Sanphams, "Masp", "Tensp");
            ViewBag.ProductPrices = _context.Sanphams.ToDictionary(p => p.Masp, p => p.Gia); // Product prices

            // Prepare details for Ctphieunhap
            ViewBag.CtPhieuNhaps = phieunhap.Ctphieunhaps.ToList();

            return View(phieunhap);
        }

        // POST: Phieunhap/Edit/{id}
        [HttpPost]
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
                    // Update the Phieunhap in the context
                    _context.Update(phieunhap);
                    await _context.SaveChangesAsync(); // Save updated Phieunhap

                    // Clear existing Ctphieunhap entries if you want to replace them
                    var existingCtPhieuNhaps = await _context.Ctphieunhaps.Where(c => c.Mapn == phieunhap.Mapn).ToListAsync();
                    _context.Ctphieunhaps.RemoveRange(existingCtPhieuNhaps);

                    // Add updated details
                    foreach (var ct in ctPhieuNhaps)
                    {
                        var product = await _context.Sanphams.FindAsync(ct.Masp);
                        if (product != null)
                        {
                            // Calculate the total price for this detail
                            ct.Thanhtien = ct.Soluong * product.Gia;
                            ct.Mapn = phieunhap.Mapn; // Set the reference to the updated Phieunhap
                            _context.Ctphieunhaps.Add(ct); // Add the detail
                        }
                    }

                    await _context.SaveChangesAsync(); // Save updated Ctphieunhap records

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
                        throw; // Re-throw the exception for global handling
                    }
                }
            }

            // Repopulate ViewBag in case of validation failure
            ViewBag.ManvList = new SelectList(_context.Nhanviens, "Manv", "TenNhanvien", phieunhap.Manv);
            ViewBag.CtPhieuNhaps = ctPhieuNhaps; // Repopulate the Ctphieunhap details
            return View(phieunhap);
        }

        // GET: Phieunhap/Delete/{id}
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

        // POST: Phieunhap/Delete/{id}
        [HttpPost, ActionName("Delete")]
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
