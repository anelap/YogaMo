using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YogaMo.Web.Helpers;
using YogaMo.Web.ViewModels;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Helpers;

namespace YogaMo.Web.Controllers
{
    public class RegisterController : BaseController
    {
        public RegisterController(_150222Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index(RegisterVM VM)
        {
            ModelState.Clear();
            return View(VM);
        }

        [HttpPost]
        public IActionResult Register(RegisterVM VM)
        {
            if (_context.Client.Any(x => x.Username == VM.Username))
            {
                ModelState.AddModelError("Username", "Username is already in use.");
            }
            if (_context.Client.Any(x => x.Email == VM.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use.");
            }

            if(ModelState.IsValid)
            {
                var entity = _mapper.Map<Client>(VM);
                if (VM.CustomGender != null)
                    entity.Gender = VM.CustomGender;

                entity.PasswordSalt = PasswordHelper.GenerateSalt();
                entity.PasswordHash = PasswordHelper.GenerateHash(entity.PasswordSalt, VM.Password);

                _context.Client.Add(entity);
                _context.SaveChanges();

                HttpContext.SetLoggedInClient(entity, true);

                return Redirect("/Client/YogaClasses");
            }

            return View("Index", VM);
        }
    }
}