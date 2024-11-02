using Microsoft.AspNetCore.Mvc;
using TapHoa.Data;

namespace TapHoa.Models
{
    public class Categories : ViewComponent
    {
        private readonly TaphoaContext _context;

        public Categories(TaphoaContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Loaisps.ToList());
        }
    }
}