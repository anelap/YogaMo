using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YogaMo.Web.Helpers;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorization(isClient: true)]
    public class YogaClassesController : Controller
    {
        private readonly _150222Context _context;

        public YogaClassesController(_150222Context context)
        {
            _context = context;
        }

        // GET: Client/YogaClasses
        public async Task<IActionResult> Index()
        {
            var list = await _context.YogaClass
                .Include(y => y.Yoga)
                .Include(y => y.YogaHall)
                .Include(x=>x.Instructor)
                .OrderBy(x=>x.Day).ThenBy(x=>x.TimeFrom)
                .ToListAsync();
            return View(list);
        }

    }
}
