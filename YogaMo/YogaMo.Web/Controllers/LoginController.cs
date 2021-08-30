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
    public class LoginController : BaseController
    {
        public LoginController(_150222Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM VM)
        {
            var administrator = _context.Administrator.Where(x => x.Username == VM.Username).FirstOrDefault();
            var instructor = _context.Instructor.Where(x => x.Username == VM.Username).FirstOrDefault();
            var client = _context.Client.Where(x => x.Username == VM.Username).FirstOrDefault();

            if(administrator != null && PasswordHelper.VerifyPassword(administrator.PasswordHash, administrator.PasswordSalt, VM.Password))
            {
                HttpContext.SetLoggedInAdministrator(administrator, VM.RememberMe);
            }
            else if(instructor != null && PasswordHelper.VerifyPassword(instructor.PasswordHash, instructor.PasswordSalt, VM.Password))
            {
                HttpContext.SetLoggedInstructor(instructor, VM.RememberMe);
            }
            else if(client != null && PasswordHelper.VerifyPassword(client.PasswordHash, client.PasswordSalt, VM.Password))
            {
                HttpContext.SetLoggedInClient(client, VM.RememberMe);
            }
            else
            {
                TempData["error_message"] = "The username or password incorrect!";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Home");
        }
    }
}