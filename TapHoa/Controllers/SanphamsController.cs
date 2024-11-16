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
    public class SanphamsController : Controller
    {
        private readonly TaphoaContext _context;

        public SanphamsController(TaphoaContext context)
        {
            _context = context;
        }

        // GET: Sanphams
        public async Task<IActionResult> Index()
        {
            var taphoaContext = _context.Sanphams.Include(s => s.MakmNavigation).Include(s => s.MaloaispNavigation).Include(s => s.ManccNavigation).Include(s => s.MathNavigation);
            return View(await taphoaContext.ToListAsync());
        }

        // GET: Sanphams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams
                .Include(s => s.MakmNavigation)
                .Include(s => s.MaloaispNavigation)
                .Include(s => s.ManccNavigation)
                .Include(s => s.MathNavigation)
                .FirstOrDefaultAsync(m => m.Masp == id);

            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }


        // GET: Sanphams/Create
        public IActionResult Create()
        {
            ViewData["Makm"] = new SelectList(_context.Khuyenmais, "Makm", "Makm");
            ViewData["Maloaisp"] = new SelectList(_context.Loaisps, "Maloaisp", "Maloaisp");
            ViewData["Mancc"] = new SelectList(_context.Nhacungcaps, "Mancc", "Mancc");
            ViewData["Math"] = new SelectList(_context.Thuonghieus, "Mathuonghieu", "Mathuonghieu");
            return View();
        }

        // POST: Sanphams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Masp,Maloaisp,Makm,Mancc,Math,Tensp,Mota,Soluong,Gia,Hinhanh,Hinhanh1,Hinhanh2,Hinhanh3")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanpham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Makm"] = new SelectList(_context.Khuyenmais, "Makm", "Makm", sanpham.Makm);
            ViewData["Maloaisp"] = new SelectList(_context.Loaisps, "Maloaisp", "Maloaisp", sanpham.Maloaisp);
            ViewData["Mancc"] = new SelectList(_context.Nhacungcaps, "Mancc", "Mancc", sanpham.Mancc);
            ViewData["Math"] = new SelectList(_context.Thuonghieus, "Mathuonghieu", "Mathuonghieu", sanpham.Math);
            return View(sanpham);
        }

        // GET: Sanphams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            ViewData["Makm"] = new SelectList(_context.Khuyenmais, "Makm", "Makm", sanpham.Makm);
            ViewData["Maloaisp"] = new SelectList(_context.Loaisps, "Maloaisp", "Maloaisp", sanpham.Maloaisp);
            ViewData["Mancc"] = new SelectList(_context.Nhacungcaps, "Mancc", "Mancc", sanpham.Mancc);
            ViewData["Math"] = new SelectList(_context.Thuonghieus, "Mathuonghieu", "Mathuonghieu", sanpham.Math);
            return View(sanpham);
        }

        // POST: Sanphams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Masp,Maloaisp,Makm,Mancc,Math,Tensp,Mota,Soluong,Gia,Hinhanh,Hinhanh1,Hinhanh2,Hinhanh3")] Sanpham sanpham)
        {
            if (id != sanpham.Masp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanpham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanphamExists(sanpham.Masp))
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
            ViewData["Makm"] = new SelectList(_context.Khuyenmais, "Makm", "Makm", sanpham.Makm);
            ViewData["Maloaisp"] = new SelectList(_context.Loaisps, "Maloaisp", "Maloaisp", sanpham.Maloaisp);
            ViewData["Mancc"] = new SelectList(_context.Nhacungcaps, "Mancc", "Mancc", sanpham.Mancc);
            ViewData["Math"] = new SelectList(_context.Thuonghieus, "Mathuonghieu", "Mathuonghieu", sanpham.Math);
            return View(sanpham);
        }

        // GET: Sanphams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams
                .Include(s => s.MakmNavigation)
                .Include(s => s.MaloaispNavigation)
                .Include(s => s.ManccNavigation)
                .Include(s => s.MathNavigation)
                .FirstOrDefaultAsync(m => m.Masp == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // POST: Sanphams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham != null)
            {
                _context.Sanphams.Remove(sanpham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanphamExists(int id)
        {
            return _context.Sanphams.Any(e => e.Masp == id);
        }
        public async Task<IActionResult> Danhsachsp()
        {
            // Fetch the list of products with related data
            var taphoaContext = await _context.Sanphams
                .Include(s => s.MakmNavigation)      // Include related Makm
                .Include(s => s.MaloaispNavigation)   // Include related Maloaisp
                .Include(s => s.ManccNavigation)      // Include related Mancc
                .Include(s => s.MathNavigation)       // Include related Math
                .ToListAsync(); // Asynchronously get the list of Sanpham

            // Return the view with the product list
            return View(taphoaContext);
        }
        [HttpGet]
      
        public IActionResult Search(string keyword)
        {
            var products = string.IsNullOrEmpty(keyword)
                ? new List<Sanpham>() // Nếu không có từ khóa, trả về danh sách trống
                : _context.Sanphams
                          .Where(sp => sp.Tensp.Contains(keyword))
                          .ToList();

            return PartialView("_ProductList", products); // Trả về PartialView
        }

        [HttpGet]

        

    }
}
