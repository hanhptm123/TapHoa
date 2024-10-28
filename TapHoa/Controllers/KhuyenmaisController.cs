using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;

namespace TapHoa.Controllers
{
    public class KhuyenmaisController : Controller
    {
        private readonly TaphoaContext _context;

        public KhuyenmaisController(TaphoaContext context)
        {
            _context = context;
        }

        // GET: Khuyenmais
        public async Task<IActionResult> Index()
        {
            return View(await _context.Khuyenmais.ToListAsync());
        }

        // GET: Khuyenmais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenmai = await _context.Khuyenmais
                .FirstOrDefaultAsync(m => m.Makm == id);
            if (khuyenmai == null)
            {
                return NotFound();
            }

            return View(khuyenmai);
        }

        // GET: Khuyenmais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Khuyenmais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Makm,Phantramgiam")] Khuyenmai khuyenmai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khuyenmai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khuyenmai);
        }

        // GET: Khuyenmais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenmai = await _context.Khuyenmais.FindAsync(id);
            if (khuyenmai == null)
            {
                return NotFound();
            }
            return View(khuyenmai);
        }

        // POST: Khuyenmais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Makm,Phantramgiam")] Khuyenmai khuyenmai)
        {
            if (id != khuyenmai.Makm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khuyenmai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhuyenmaiExists(khuyenmai.Makm))
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
            return View(khuyenmai);
        }

        // GET: Khuyenmais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khuyenmai = await _context.Khuyenmais
                .FirstOrDefaultAsync(m => m.Makm == id);
            if (khuyenmai == null)
            {
                return NotFound();
            }

            return View(khuyenmai);
        }

        // POST: Khuyenmais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khuyenmai = await _context.Khuyenmais.FindAsync(id);
            if (khuyenmai != null)
            {
                _context.Khuyenmais.Remove(khuyenmai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhuyenmaiExists(int id)
        {
            return _context.Khuyenmais.Any(e => e.Makm == id);
        }
    }
}
