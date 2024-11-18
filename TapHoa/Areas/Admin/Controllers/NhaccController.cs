using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;
using X.PagedList.Extensions;

namespace TapHoa.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/Nhacc")]
    public class NhaccController : Controller
    {
        private readonly TaphoaContext _context;
        public NhaccController(TaphoaContext context)
        {
            _context = context;
        }
        [Route("danhmucnhacc")]
        public IActionResult DanhMucNhacc(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstncc = _context.Nhacungcaps.AsNoTracking().OrderBy(x => x.Tenncc).ToPagedList(pageNumber, pageSize);
            return View(lstncc);
        }
        [Route("ThemNcc")]
        [HttpGet]
        public IActionResult ThemNcc()
        {
            return View();
        }
        [Route("ThemNcc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNcc(Nhacungcap nhaCc)
        {
            if (ModelState.IsValid)
            {
                _context.Nhacungcaps.Add(nhaCc);
                _context.SaveChanges();
                return RedirectToAction("DanhMucNhacc");
            }
            return View(nhaCc);
        }
        [Route("SuaNhacc")]
        [HttpGet]
        public IActionResult SuaNhacc(int Mancc)
        {
            var nhaCc = _context.Nhacungcaps.Find(Mancc);
            return View(nhaCc);
        }
        [Route("SuaNhacc")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhacc(Nhacungcap nhaCc)
        {
            if (ModelState.IsValid)
            {
                _context.Update(nhaCc);
                _context.SaveChanges();
                return RedirectToAction("DanhMucNhacc");
            }
            return View(nhaCc);
        }
        [Route("XoaNhacc")]
        [HttpGet]
        public IActionResult XoaNhacc(int Mancc)
        {
            var nhaCc = _context.Nhacungcaps.Find(Mancc);

            if (nhaCc == null)
            {
                return NotFound();
            }

            var hasRelatedRecords = _context.Sanphams.Any(ct => ct.Mancc == Mancc);
            if (hasRelatedRecords)
            {
                TempData["ErrorMessage"] = "Không thể xóa nhà cung cấp vì có bản ghi liên quan.";
                return RedirectToAction("DanhMucNhacc");
            }

            _context.Nhacungcaps.Remove(nhaCc);
            _context.SaveChanges();

            return RedirectToAction("DanhMucNhacc");
        }
    }
}
