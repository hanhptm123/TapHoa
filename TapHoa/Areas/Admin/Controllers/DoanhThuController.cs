using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapHoa.Data;
using TapHoa.Models;
using System.Linq;

namespace TapHoa.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/doanhthu")]
    public class DoanhThuController : Controller
    {
        private readonly TaphoaContext _context;

        public DoanhThuController(TaphoaContext context)
        {
            _context = context;
        }

        // Trang danh sách báo cáo doanh thu
        [Route("danhsach")]
        public IActionResult DanhSach()
        {
            return View();
        }

        // Báo cáo doanh thu theo ngày
        [Route("baocaongay")]
        public IActionResult BaoCaoNgay(DateTime? ngay)
        {
            ngay ??= DateTime.Today;

            var doanhThuNgay = _context.Hoadons
                .Where(hd => hd.Ngaylap.Date == ngay.Value.Date)
                .GroupBy(hd => hd.Ngaylap.Date)
                .Select(g => new 
                {
                    Ngay = g.Key,
                    TongDoanhThu = g.Sum(hd => hd.Tongtien)
                }).FirstOrDefault();

            ViewBag.Ngay = ngay.Value;
            ViewBag.DoanhThu = doanhThuNgay?.TongDoanhThu ?? 0;

            return View();
        }

        // Báo cáo doanh thu theo tháng
        [Route("baocaothang")]
        public IActionResult BaoCaoThang(int? thang, int? nam)
        {
            thang ??= DateTime.Today.Month;
            nam ??= DateTime.Today.Year;

            var doanhThuThang = _context.Hoadons
                .Where(hd => hd.Ngaylap.Month == thang && hd.Ngaylap.Year == nam)
                .GroupBy(hd => new { hd.Ngaylap.Month, hd.Ngaylap.Year })
                .Select(g => new 
                {
                    Thang = g.Key.Month,
                    Nam = g.Key.Year,
                    TongDoanhThu = g.Sum(hd => hd.Tongtien)
                }).FirstOrDefault();

            ViewBag.Thang = thang.Value;
            ViewBag.Nam = nam.Value;
            ViewBag.DoanhThu = doanhThuThang?.TongDoanhThu ?? 0;

            return View();
        }

        // Báo cáo doanh thu theo năm
        [Route("baocaonam")]
        public IActionResult BaoCaoNam(int? nam)
        {
            nam ??= DateTime.Today.Year;

            var doanhThuNam = _context.Hoadons
                .Where(hd => hd.Ngaylap.Year == nam)
                .GroupBy(hd => hd.Ngaylap.Year)
                .Select(g => new 
                {
                    Nam = g.Key,
                    TongDoanhThu = g.Sum(hd => hd.Tongtien)
                }).FirstOrDefault();

            ViewBag.Nam = nam.Value;
            ViewBag.DoanhThu = doanhThuNam?.TongDoanhThu ?? 0;

            return View();
        }
    }
}
