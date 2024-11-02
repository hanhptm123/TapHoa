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
    public class LoaispsController : Controller
    {
        private readonly TaphoaContext _context;

        public LoaispsController(TaphoaContext context)
        {
            _context = context;
        }

        // GET: Loaisps
        public async Task<IActionResult> Index()
        {
            return View(await _context.Loaisps.ToListAsync());
        }

        // GET: Loaisps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaisp = await _context.Loaisps
                .FirstOrDefaultAsync(m => m.Maloaisp == id);
            if (loaisp == null)
            {
                return NotFound();
            }

            return View(loaisp);
        }

        // GET: Loaisps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loaisps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Maloaisp,Tenloaisp")] Loaisp loaisp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaisp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaisp);
        }

        // GET: Loaisps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaisp = await _context.Loaisps.FindAsync(id);
            if (loaisp == null)
            {
                return NotFound();
            }
            return View(loaisp);
        }

        // POST: Loaisps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Maloaisp,Tenloaisp")] Loaisp loaisp)
        {
            if (id != loaisp.Maloaisp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaisp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaispExists(loaisp.Maloaisp))
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
            return View(loaisp);
        }

        // GET: Loaisps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaisp = await _context.Loaisps
                .FirstOrDefaultAsync(m => m.Maloaisp == id);
            if (loaisp == null)
            {
                return NotFound();
            }

            return View(loaisp);
        }

        // POST: Loaisps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaisp = await _context.Loaisps.FindAsync(id);
            if (loaisp != null)
            {
                _context.Loaisps.Remove(loaisp);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaispExists(int id)
        {
            return _context.Loaisps.Any(e => e.Maloaisp == id);
        }
    }
}
