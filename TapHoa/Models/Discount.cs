using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TapHoa.Data;

namespace TapHoa.Components
{
    public class DiscountViewComponent : ViewComponent
    {
        private readonly TaphoaContext _context;

        public DiscountViewComponent(TaphoaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy danh sách sản phẩm có giảm giá (không cần HasValue nếu Phantramgiam không phải là nullable)
            var sanphamsGiamGia = await _context.Sanphams
                .Where(sp => sp.MakmNavigation != null && sp.MakmNavigation.Phantramgiam > 0)
                .Include(sp => sp.MakmNavigation) // Bao gồm thông tin khuyến mãi
                .ToListAsync();

            return View(sanphamsGiamGia);
        }
    }
}
