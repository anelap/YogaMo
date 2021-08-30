using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly _150222Context _context;
        protected readonly IMapper _mapper;

        public BaseController(_150222Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}