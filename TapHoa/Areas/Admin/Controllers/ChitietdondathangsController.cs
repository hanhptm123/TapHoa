using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;

namespace TapHoa.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChitietdondathangsController : Controller
    {
        private readonly TaphoaContext _context;

        public ChitietdondathangsController(TaphoaContext context)
        {
            _context = context;
        }

        // GET: Admin/Chitietdondathangs
        public async Task<IActionResult> Index()
        {
            var taphoaContext = _context.Chitietdondathangs.Include(c => c.MaddhNavigation).Include(c => c.MaspNavigation);
            return View(await taphoaContext.ToListAsync());
        }

        // GET: Admin/Chitietdondathangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chitietdondathang = await _context.Chitietdondathangs
                .Include(c => c.MaddhNavigation)
                .Include(c => c.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Masp == id);
            if (chitietdondathang == null)
            {
                return NotFound();
            }

            return View(chitietdondathang);
        }

        // GET: Admin/Chitietdondathangs/Create
        public IActionResult Create()
        {
            ViewData["Maddh"] = new SelectList(_context.Dondathangs, "Maddh", "Maddh");
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp");
            return View();
        }

        // POST: Admin/Chitietdondathangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Masp,Maddh,Soluong,Thanhtien")] Chitietdondathang chitietdondathang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chitietdondathang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Maddh"] = new SelectList(_context.Dondathangs, "Maddh", "Maddh", chitietdondathang.Maddh);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp", chitietdondathang.Masp);
            return View(chitietdondathang);
        }

        // GET: Admin/Chitietdondathangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chitietdondathang = await _context.Chitietdondathangs.FindAsync(id);
            if (chitietdondathang == null)
            {
                return NotFound();
            }
            ViewData["Maddh"] = new SelectList(_context.Dondathangs, "Maddh", "Maddh", chitietdondathang.Maddh);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp", chitietdondathang.Masp);
            return View(chitietdondathang);
        }

        // POST: Admin/Chitietdondathangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Masp,Maddh,Soluong,Thanhtien")] Chitietdondathang chitietdondathang)
        {
            if (id != chitietdondathang.Masp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chitietdondathang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChitietdondathangExists(chitietdondathang.Masp))
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
            ViewData["Maddh"] = new SelectList(_context.Dondathangs, "Maddh", "Maddh", chitietdondathang.Maddh);
            ViewData["Masp"] = new SelectList(_context.Sanphams, "Masp", "Masp", chitietdondathang.Masp);
            return View(chitietdondathang);
        }

        // GET: Admin/Chitietdondathangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chitietdondathang = await _context.Chitietdondathangs
                .Include(c => c.MaddhNavigation)
                .Include(c => c.MaspNavigation)
                .FirstOrDefaultAsync(m => m.Masp == id);
            if (chitietdondathang == null)
            {
                return NotFound();
            }

            return View(chitietdondathang);
        }

        // POST: Admin/Chitietdondathangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chitietdondathang = await _context.Chitietdondathangs.FindAsync(id);
            if (chitietdondathang != null)
            {
                _context.Chitietdondathangs.Remove(chitietdondathang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChitietdondathangExists(int id)
        {
            return _context.Chitietdondathangs.Any(e => e.Masp == id);
        }
    }
}
