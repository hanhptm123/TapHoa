using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;
using X.PagedList.Extensions;

namespace TapHoa.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/KhuyenMai")]
    public class KhuyenMaiController : Controller
    {
        private readonly TaphoaContext _context;
        public  KhuyenMaiController(TaphoaContext context)
        {
            _context = context;
        }
        [Route("danhmuckhuyenmai")]
        public IActionResult DanhMucKhuyenMai(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstkm = _context.Khuyenmais.AsNoTracking().OrderBy(x => x.Phantramgiam).ToPagedList(pageNumber, pageSize);
            return View(lstkm);
        }
        [Route("ThemKM")]
        [HttpGet]
        public IActionResult ThemKM()
        {
            return View();
        }
        [Route("ThemKM")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemKM(Khuyenmai khuyenMai)
        {
            if (ModelState.IsValid)
            {
                _context.Khuyenmais.Add(khuyenMai);
                _context.SaveChanges();
                return RedirectToAction("DanhMucKhuyenMai");
            }
            return View(khuyenMai);
        }
        [Route("SuaKm")]
        [HttpGet]
        public IActionResult SuaKm(int Makm)
        {
            var khuyenMai = _context.Khuyenmais.Find(Makm);
            return View(khuyenMai);
        }
        [Route("SuaKm")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKm(Khuyenmai khuyenMai)
        {
            if (ModelState.IsValid)
            {
                _context.Update(khuyenMai);
                _context.SaveChanges();
                return RedirectToAction("DanhMucKhuyenMai");
            }
            return View(khuyenMai);
        }
        [Route("XoaKm")]
[HttpGet]
public IActionResult XoaKm(int Makm)
{
    var khuyenMai = _context.Khuyenmais.Find(Makm);

    if (khuyenMai == null)
    {
        return NotFound();
    }

    var hasRelatedRecords = _context.Sanphams.Any(ct => ct.Makm == Makm);
    if (hasRelatedRecords)
    {
        TempData["ErrorMessage"] = "Không thể xóa khuyến mãi vì có bản ghi liên quan.";
        return RedirectToAction("DanhMucKhuyenMai");
    }

    _context.Khuyenmais.Remove(khuyenMai);
    _context.SaveChanges();

    return RedirectToAction("DanhMucKhuyenMai");
}


    }
}
