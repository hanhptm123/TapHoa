using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;
using X.PagedList;
using X.PagedList.Extensions;

namespace TapHoa.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        private readonly TaphoaContext _context;

        public HomeAdminController(TaphoaContext context)
        {
            _context = context;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value; 
            var lstsanpham = _context.Sanphams.AsNoTracking().OrderBy(x => x.Tensp).ToPagedList(pageNumber, pageSize);
            return View(lstsanpham);
        }
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.Mathuonghieu = new SelectList(_context.Thuonghieus.ToList(), "Mathuonghieu", "Thuonghieu");
            ViewBag.Mancc = new SelectList(_context.Nhacungcaps.ToList(), "Mancc", "Tenncc");
            ViewBag.Maloaisp = new SelectList(_context.Loaisps.ToList(), "Maloaisp", "Tenloaisp");
            ViewBag.Makm = new SelectList(_context.Khuyenmais.ToList(), "Makm", "Phantramgiam");
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(Sanpham sanPham)
        {
            if(ModelState.IsValid)
            {
                _context.Sanphams.Add(sanPham);
                _context.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int Masp)
        {
            ViewBag.Mathuonghieu = new SelectList(_context.Thuonghieus.ToList(), "Mathuonghieu", "Thuonghieu");
            ViewBag.Mancc = new SelectList(_context.Nhacungcaps.ToList(), "Mancc", "Tenncc");
            ViewBag.Maloaisp = new SelectList(_context.Loaisps.ToList(), "Maloaisp", "Tenloaisp");
            ViewBag.Makm = new SelectList(_context.Khuyenmais.ToList(), "Makm", "Phantramgiam");
            var sanPham = _context.Sanphams.Find(Masp);
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(Sanpham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Update(sanPham);
                _context.SaveChanges();
                return RedirectToAction("DanhMucSanPham","HomeAdmin");
            }
            return View(sanPham);
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(int Masp)
        {
            var sanPham = _context.Sanphams.Find(Masp);

            if (sanPham == null)
            {
                return NotFound();
            }

            var hasRelatedRecords = _context.Ctphieunhaps.Any(ct => ct.Masp == Masp);
            if (hasRelatedRecords)
            {
                TempData["ErrorMessage"] = "Không thể xóa sản phẩm vì có bản ghi liên quan.";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }

            _context.Sanphams.Remove(sanPham);
            _context.SaveChanges();

            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }
        [Route("danhmucloaisanpham")]
        public IActionResult DanhMucLoaiSanPham(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstloaisanpham = _context.Loaisps.AsNoTracking().OrderBy(x => x.Tenloaisp).ToPagedList(pageNumber, pageSize);
            return View(lstloaisanpham);
        }
        [Route("ThemLoai")]
        [HttpGet]
        public IActionResult ThemLoai()
        {
            return View();
        }

        [Route("ThemLoai")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoai(Loaisp lSanPham)
        {
            if (ModelState.IsValid)
            {
                var existingLoai = _context.Loaisps.FirstOrDefault(x => x.Tenloaisp == lSanPham.Tenloaisp);
                if (existingLoai != null)
                {
                    ModelState.AddModelError("Tenloaisp", "Loại sản phẩm này đã tồn tại.");
                    return View(lSanPham);
                }

                _context.Loaisps.Add(lSanPham);
                _context.SaveChanges();
                return RedirectToAction("DanhMucLoaiSanPham");
            }
            return View(lSanPham);
        }

    }
}
