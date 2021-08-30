using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YogaMo.Web.Areas.Administrator.ViewModels;
using YogaMo.Web.Helpers;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorization(isClient: true)]
    public class InstructorsController : Controller
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;

        public InstructorsController(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Administrator/Instructors
        public async Task<IActionResult> Index(string SearchInstructor)
        {
            ViewBag.SearchInstructor = SearchInstructor;
            if (SearchInstructor != null)
                SearchInstructor = SearchInstructor.ToLower();

            return View(await
                _context.Instructor
                .Where(x => x.FirstName.ToLower().Contains(SearchInstructor) || x.LastName.ToLower().Contains(SearchInstructor)
                || (x.FirstName.ToLower() + " " + x.LastName.ToLower()).Contains(SearchInstructor)
                || (x.LastName.ToLower() + " " + x.FirstName.ToLower()).Contains(SearchInstructor)
                || SearchInstructor == null)
                .Where(x => x.Status == true)
                .ToListAsync());
        }

    }
}
