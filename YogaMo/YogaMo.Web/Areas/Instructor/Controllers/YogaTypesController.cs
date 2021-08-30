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
    public class YogaTypesController : Controller
    {
        private readonly _150222Context _context;

        public YogaTypesController(_150222Context context)
        {
            _context = context;
        }

        // GET: Instructor/YogaTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Yoga.ToListAsync());
        }

        // GET: Instructor/YogaTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instructor/YogaTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YogaId,Name,Description")] Yoga yoga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yoga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yoga);
        }

        // GET: Instructor/YogaTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yoga = await _context.Yoga.FindAsync(id);
            if (yoga == null)
            {
                return NotFound();
            }
            return View(yoga);
        }

        // POST: Instructor/YogaTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YogaId,Name,Description")] Yoga yoga)
        {
            if (id != yoga.YogaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yoga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YogaExists(yoga.YogaId))
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
            return View(yoga);
        }

        // GET: Instructor/YogaTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yoga = await _context.Yoga
                .FirstOrDefaultAsync(m => m.YogaId == id);
            if (yoga == null)
            {
                return NotFound();
            }

            return View(yoga);
        }

        // POST: Instructor/YogaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yoga = await _context.Yoga.FindAsync(id);
            _context.Yoga.Remove(yoga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YogaExists(int id)
        {
            return _context.Yoga.Any(e => e.YogaId == id);
        }
    }
}
