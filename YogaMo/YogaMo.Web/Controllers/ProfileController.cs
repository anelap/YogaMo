using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaMo.Web.Helpers;
using YogaMo.Web.ViewModels;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Controllers
{
    [Authorization(isClient: true, isInstructor: true, isAdmin: true)]
    public class ProfileController : BaseController
    {
        public ProfileController(_150222Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public IActionResult Index()
        {
            var Client = HttpContext.GetLoggedInClient();
            var Instructor = HttpContext.GetLoggedInInstructor();
            var Administrator = HttpContext.GetLoggedInAdministrator();

            var VM = new ProfileVM();

            if (Client != null)
            {
                _mapper.Map(Client, VM);
                ViewData["Cities"] = new SelectList(_context.City, "CityId", "Name");

            }
            else if (Instructor != null)
            {
                _mapper.Map(Instructor, VM);
            }
            else if (Administrator != null)
            {
                _mapper.Map(Administrator, VM);
            }

            return View(VM);
        }

        [HttpPost]
        public IActionResult UpdateProfile(ProfileVM VM)
        {
            var Client = HttpContext.GetLoggedInClient();
            var Instructor = HttpContext.GetLoggedInInstructor();
            var Administrator = HttpContext.GetLoggedInAdministrator();


            if (Client != null)
            {
                if (_context.Client.Any(x => x.Username == VM.Username && x.ClientId != Client.ClientId))
                {
                    ModelState.AddModelError("Username", "Username is already in use.");
                }
                if (_context.Client.Any(x => x.Email == VM.Email && x.ClientId != Client.ClientId))
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                }

                if (ModelState.IsValid)
                {
                    var entity = _context.Client.Find(Client.ClientId);

                    if (!string.IsNullOrEmpty(VM.Password))
                    {
                        entity.PasswordSalt = WebAPI.Helpers.PasswordHelper.GenerateSalt();
                        entity.PasswordHash = WebAPI.Helpers.PasswordHelper.GenerateHash(entity.PasswordSalt, VM.Password);
                    }

                    _mapper.Map(VM, entity);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["Cities"] = new SelectList(_context.City, "CityId", "Name");
                }

            }
            else if (Instructor != null)
            {

                if (_context.Instructor.Any(x => x.Username == VM.Username && x.InstructorId != Instructor.InstructorId))
                {
                    ModelState.AddModelError("Username", "Username is already in use.");
                }
                if (_context.Instructor.Any(x => x.Email == VM.Email && x.InstructorId != Instructor.InstructorId))
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                }

                if (ModelState.IsValid)
                {

                    var entity = _context.Instructor.Find(Instructor.InstructorId);

                    if (!string.IsNullOrEmpty(VM.Password))
                    {
                        entity.PasswordSalt = WebAPI.Helpers.PasswordHelper.GenerateSalt();
                        entity.PasswordHash = WebAPI.Helpers.PasswordHelper.GenerateHash(entity.PasswordSalt, VM.Password);
                    }

                    _mapper.Map(VM, entity);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            else if (Administrator != null)
            {

                if (_context.Administrator.Any(x => x.Username == VM.Username && x.AdministratorId != Administrator.AdministratorId))
                {
                    ModelState.AddModelError("Username", "Username is already in use.");
                }
                if (_context.Administrator.Any(x => x.Email == VM.Email && x.AdministratorId != Administrator.AdministratorId))
                {
                    ModelState.AddModelError("Email", "Email is already in use.");
                }

                if (ModelState.IsValid)
                {

                    var entity = _context.Administrator.Find(Administrator.AdministratorId);

                    if (!string.IsNullOrEmpty(VM.Password))
                    {
                        entity.PasswordSalt = WebAPI.Helpers.PasswordHelper.GenerateSalt();
                        entity.PasswordHash = WebAPI.Helpers.PasswordHelper.GenerateHash(entity.PasswordSalt, VM.Password);
                    }

                    _mapper.Map(VM, entity);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View("Index", VM);
        }

        [HttpPost]
        public IActionResult UpdatePhoto(IFormFile ProfilePicture)
        {
            var Client = HttpContext.GetLoggedInClient();
            var Instructor = HttpContext.GetLoggedInInstructor();
            var Administrator = HttpContext.GetLoggedInAdministrator();


            if (Client != null)
            {
                var entity = _context.Client.Find(Client.ClientId);

                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    entity.ProfilePicture = ImageHelper.ConvertUploadToByteArray(ProfilePicture);
                }
                else
                    entity.ProfilePicture = null;
            }
            else if (Instructor != null)
            {
                var entity = _context.Instructor.Find(Instructor.InstructorId);

                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    entity.ProfilePicture = ImageHelper.ConvertUploadToByteArray(ProfilePicture);
                }
                else
                    entity.ProfilePicture = null;
            }
            else if (Administrator != null)
            {
                var entity = _context.Instructor.Find(Administrator.AdministratorId);

                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    entity.ProfilePicture = ImageHelper.ConvertUploadToByteArray(ProfilePicture);
                }
                else
                    entity.ProfilePicture = null;
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
