using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    public class YogaHallsController : Controller
    {
        private readonly _150222Context _context;

        public YogaHallsController(_150222Context context)
        {
            _context = context;
        }

        // GET: Instructor/YogaHalls
        public async Task<IActionResult> Index()
        {
            var _150222Context = _context.YogaHall;
            return View(await _150222Context.ToListAsync());
        }

        // GET: Instructor/YogaHalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructor/YogaHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YogaHallId,Name")] YogaHall yogaHall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yogaHall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yogaHall);
        }

        // GET: Instructor/YogaHalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaHall = await _context.YogaHall.FindAsync(id);
            if (yogaHall == null)
            {
                return NotFound();
            }
            return View(yogaHall);
        }

        // POST: Instructor/YogaHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YogaHallId,Name")] YogaHall yogaHall)
        {
            if (id != yogaHall.YogaHallId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yogaHall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YogaHallExists(yogaHall.YogaHallId))
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
            return View(yogaHall);
        }

        // GET: Instructor/YogaHalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaHall = await _context.YogaHall
                .FirstOrDefaultAsync(m => m.YogaHallId == id);
            if (yogaHall == null)
            {
                return NotFound();
            }

            return View(yogaHall);
        }

        // POST: Instructor/YogaHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yogaHall = await _context.YogaHall.FindAsync(id);
            _context.YogaHall.Remove(yogaHall);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YogaHallExists(int id)
        {
            return _context.YogaHall.Any(e => e.YogaHallId == id);
        }
    }
}
