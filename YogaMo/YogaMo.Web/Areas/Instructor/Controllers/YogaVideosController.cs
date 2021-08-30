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
    public class YogaVideosController : BaseController
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public YogaVideosController(_150222Context context, IMapper mapper, IWebHostEnvironment hostEnvironment) : base(context, mapper)
        {
            this.webHostEnvironment = hostEnvironment;
        }


        // GET: Instructor/YogaVideos
        public async Task<IActionResult> Index()
        {
            return View(await _context.YogaVideo.ToListAsync());
        }


        // GET: Instructor/YogaVideos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructor/YogaVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YoutubeID")] YogaVideo yogaVideo, IFormFile ThumbnailFileName)
        {
            if (ModelState.IsValid && ThumbnailFileName != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "thumbs");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ThumbnailFileName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ThumbnailFileName.CopyTo(fileStream);

                    yogaVideo.ThumbnailFileName = uniqueFileName;
                }

                _context.Add(yogaVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(yogaVideo);
        }

        // GET: Instructor/YogaVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaVideo = await _context.YogaVideo.FindAsync(id);
            if (yogaVideo == null)
            {
                return NotFound();
            }
            return View(yogaVideo);
        }

        // POST: Instructor/YogaVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,YoutubeID")] YogaVideo yogaVideo, IFormFile ThumbnailFileName)
        {
            if (id != yogaVideo.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {

                    var existingYogaVideo = _context.YogaVideo.Where(x=>x.Id == id).AsNoTracking().FirstOrDefault();
                    if (ThumbnailFileName != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "thumbs");

                        var oldImageName = existingYogaVideo.ThumbnailFileName;
                        if(oldImageName != null)
                        {
                            string oldImageFilePath = Path.Combine(uploadsFolder, oldImageName);
                            if (System.IO.File.Exists(oldImageFilePath))
                            {
                                System.IO.File.Delete(oldImageFilePath);
                            }
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + ThumbnailFileName.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            ThumbnailFileName.CopyTo(fileStream);

                            yogaVideo.ThumbnailFileName = uniqueFileName;
                        }
                    }
                    else
                        yogaVideo.ThumbnailFileName = existingYogaVideo.ThumbnailFileName;

                    _context.Update(yogaVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YogaVideoExists(yogaVideo.Id))
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
            return View(yogaVideo);
        }

        // GET: Instructor/YogaVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaVideo = await _context.YogaVideo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yogaVideo == null)
            {
                return NotFound();
            }

            return View(yogaVideo);
        }

        // POST: Instructor/YogaVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yogaVideo = await _context.YogaVideo.FindAsync(id);

            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");

            var oldImageName = yogaVideo.ThumbnailFileName;
            if (oldImageName != null)
            {
                string oldImageFilePath = Path.Combine(uploadsFolder, oldImageName);

                if (System.IO.File.Exists(oldImageFilePath))
                {
                    System.IO.File.Delete(oldImageFilePath);
                }
            }

            _context.YogaVideo.Remove(yogaVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YogaVideoExists(int id)
        {
            return _context.YogaVideo.Any(e => e.Id == id);
        }
    }
}
