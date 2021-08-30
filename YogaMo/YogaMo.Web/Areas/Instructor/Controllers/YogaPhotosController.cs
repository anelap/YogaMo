using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YogaMo.Web.Controllers;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    public class YogaPhotosController : BaseController
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public YogaPhotosController(_150222Context context, IMapper mapper, IWebHostEnvironment hostEnvironment) : base(context, mapper)
        {
            this.webHostEnvironment = hostEnvironment;
        }

        // GET: Instructor/YogaPhotos
        public async Task<IActionResult> Index()
        {
            return View(await _context.YogaPhoto.ToListAsync());
        }

        // GET: Instructor/YogaPhotos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructor/YogaPhotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile PhotoFileName)
        {
            YogaPhoto yogaPhoto = new YogaPhoto();

            if (ModelState.IsValid && PhotoFileName != null)
            {

                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + PhotoFileName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    PhotoFileName.CopyTo(fileStream);

                    yogaPhoto.PhotoFileName = uniqueFileName;
                }

                _context.Add(yogaPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(yogaPhoto);
        }

        // GET: Instructor/YogaPhotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaPhoto = await _context.YogaPhoto.FindAsync(id);
            if (yogaPhoto == null)
            {
                return NotFound();
            }
            return View(yogaPhoto);
        }

        // POST: Instructor/YogaPhotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile PhotoFileName)
        {
            YogaPhoto yogaPhoto = _context.YogaPhoto.Find(id);

            if (ModelState.IsValid)
            {
                try
                {
                    if (PhotoFileName != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

                        var oldImageName = yogaPhoto.PhotoFileName;
                        if(oldImageName != null)
                        {
                            string oldImageFilePath = Path.Combine(uploadsFolder, oldImageName);
                            if (System.IO.File.Exists(oldImageFilePath))
                            {
                                System.IO.File.Delete(oldImageFilePath);
                            }
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + PhotoFileName.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            PhotoFileName.CopyTo(fileStream);

                            yogaPhoto.PhotoFileName = uniqueFileName;
                        }
                    }

                    _context.Update(yogaPhoto);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YogaPhotoExists(yogaPhoto.Id))
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
            return View(yogaPhoto);
        }

        // GET: Instructor/YogaPhotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaPhoto = await _context.YogaPhoto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yogaPhoto == null)
            {
                return NotFound();
            }

            return View(yogaPhoto);
        }

        // POST: Instructor/YogaPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yogaPhoto = await _context.YogaPhoto.FindAsync(id);

            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

            var oldImageName = yogaPhoto.PhotoFileName;
            if(oldImageName != null)
            {
                string oldImageFilePath = Path.Combine(uploadsFolder, oldImageName);

                if (System.IO.File.Exists(oldImageFilePath))
                {
                    System.IO.File.Delete(oldImageFilePath);
                }
            }

            _context.YogaPhoto.Remove(yogaPhoto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YogaPhotoExists(int id)
        {
            return _context.YogaPhoto.Any(e => e.Id == id);
        }
    }
}
