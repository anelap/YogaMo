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

namespace YogaMo.Web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorization(isInstructor: true)]
    public class ClientsController : Controller
    {
        private readonly _150222Context _context;
        private readonly IMapper _mapper;

        public ClientsController(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Administrator/Clients
        public async Task<IActionResult> Index(string SearchClient)
        {
            ViewBag.SearchClient = SearchClient;
            if(SearchClient != null)
                SearchClient = SearchClient.ToLower();

            return View(await 
                _context.Client
                .Include(x=>x.City.Country)
                .Where(x => x.FirstName.ToLower().Contains(SearchClient) || x.LastName.ToLower().Contains(SearchClient) 
                || (x.FirstName.ToLower() + " " + x.LastName.ToLower()).Contains(SearchClient)
                || (x.LastName.ToLower() + " " + x.FirstName.ToLower()).Contains(SearchClient)
                || x.City.Name.ToLower().Contains(SearchClient)
                || x.City.Country.Name.ToLower().Contains(SearchClient) || SearchClient == null)
                .ToListAsync());
        }

    }
}
