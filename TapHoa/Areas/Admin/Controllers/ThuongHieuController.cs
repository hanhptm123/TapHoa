using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;
using X.PagedList.Extensions;

namespace TapHoa.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/thuonghieu")]
    public class ThuongHieuController : Controller
    {
        private readonly TaphoaContext _context;
        public ThuongHieuController(TaphoaContext context)
        {
            _context = context;
        }
        [Route("danhmucthuonghieu")]
        public IActionResult DanhMucThuongHieu(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstTH = _context.Thuonghieus.AsNoTracking().OrderBy(x => x.Tenthuonghieu).ToPagedList(pageNumber, pageSize);
            return View(lstTH);
        }
        [Route("ThemTH")]
        [HttpGet]
        public IActionResult ThemTH()
        {
            return View();
        }
        [Route("ThemTH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTH(Thuonghieu TH)
        {
            if (ModelState.IsValid)
            {
                _context.Thuonghieus.Add(TH);
                _context.SaveChanges();
                return RedirectToAction("DanhMucThuongHieu");
            }
            return View(TH);
        }
        
        [Route("SuaTH")]
        [HttpGet]
        public IActionResult SuaTH(int Mathuonghieu)
        {
            var TH = _context.Thuonghieus.AsNoTracking().FirstOrDefault(th => th.Mathuonghieu == Mathuonghieu);
            return View(TH);
        }
        [Route("SuaTH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTH(Thuonghieu TH)
        {
            if (ModelState.IsValid)
            {
                _context.Update(TH);
                _context.SaveChanges();
                return RedirectToAction("DanhMucThuongHieu");
            }
            return View(TH);
        }
        [Route("XoaTH")]
        [HttpGet]
        public IActionResult XoaTH(int Mathuonghieu)
        {
            var TH = _context.Thuonghieus.Find(Mathuonghieu);

            if (TH == null)
            {
                return NotFound();
            }

            var hasRelatedRecords = _context.Sanphams.Any(ct => ct.Math == Mathuonghieu);
            if (hasRelatedRecords)
            {
                TempData["ErrorMessage"] = "Không thể xóa thương hiệu vì có bản ghi liên quan.";
                return RedirectToAction("DanhMucThuongHieu");
            }

            _context.Thuonghieus.Remove(TH);
            _context.SaveChanges();

            return RedirectToAction("DanhMucThuongHieu");
        }
    }
}
