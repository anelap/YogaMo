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

namespace YogaMo.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorization(isAdmin: true)]
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
                .ToListAsync());
        }


        // GET: Administrator/Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InstructorId,FirstName,LastName,Email,Phone,Username,Password,Title")] CreateInstructorVM instructor, IFormFile ProfilePicture)
        {
            if (ModelState.IsValid)
            {
                if(ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    instructor.ProfilePicture = ImageHelper.ConvertUploadToByteArray(ProfilePicture);
                }

                instructor.Status = true;

                var mappedEntity = _mapper.Map<WebAPI.Database.Instructor>(instructor);

                mappedEntity.PasswordSalt = WebAPI.Helpers.PasswordHelper.GenerateSalt();
                mappedEntity.PasswordHash = WebAPI.Helpers.PasswordHelper.GenerateHash(mappedEntity.PasswordSalt, instructor.Password);

                _context.Add(mappedEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Administrator/Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructor.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            var mappedInstructor = _mapper.Map<InstructorVM>(instructor);

            return View(mappedInstructor);
        }

        // POST: Administrator/Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstructorId,FirstName,LastName,Email,Phone,Username,Password,Status,Title,RemoveProfilePicture")] InstructorVM instructor, IFormFile ProfilePicture)
        {
            if (id != instructor.InstructorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var entity = _context.Instructor.Find(id);

                    if (ProfilePicture != null && ProfilePicture.Length > 0)
                    {
                        instructor.ProfilePicture = ImageHelper.ConvertUploadToByteArray(ProfilePicture);
                    }
                    else if(instructor.RemoveProfilePicture)
                    {
                        instructor.ProfilePicture = null;
                    }
                    else
                        instructor.ProfilePicture = entity.ProfilePicture;

                    if(!string.IsNullOrEmpty(instructor.Password))
                    {
                        entity.PasswordSalt = WebAPI.Helpers.PasswordHelper.GenerateSalt();
                        entity.PasswordHash = WebAPI.Helpers.PasswordHelper.GenerateHash(entity.PasswordSalt, instructor.Password);
                    }

                    _mapper.Map(instructor, entity);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.InstructorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Administrator/Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructor
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Administrator/Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructor.FindAsync(id);
            _context.Instructor.Remove(instructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructor.Any(e => e.InstructorId == id);
        }
    }
}
