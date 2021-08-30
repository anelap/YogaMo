using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YogaMo.Web.Areas.Instructor.ViewModels;
using YogaMo.Web.Helpers;
using YogaMo.WebAPI.Database;

namespace YogaMo.Web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorization(isInstructor: true)]
    public class YogaClassesController : Controller
    {
        private readonly _150222Context _context;
        private readonly IMapper mapper;

        public YogaClassesController(_150222Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Instructor/YogaClasses
        public async Task<IActionResult> Index()
        {
            var _150222Context = _context.YogaClass.Include(y => y.Yoga);

            Dictionary<string, int> DaysOfWeek = new Dictionary<string, int>
            {
                ["Monday"] = 0,
                ["Tuesday"] = 1,
                ["Wednesday"] = 2,
                ["Thursday"] = 3,
                ["Friday"] = 4,
                ["Saturday"] = 5,
                ["Sunday"] = 6
            };

            var list = await _150222Context
                .Include(x=>x.Instructor)
                .Include(x=>x.YogaHall)
                .ToListAsync();

            list = list
                .OrderBy(x => DaysOfWeek[x.Day])
                .ThenBy(x=> x.TimeFrom)
                .ToList();

            return View(list);
        }

        // GET: Instructor/YogaClasses/Create
        public IActionResult Create()
        {
            var Instructor = HttpContext.GetLoggedInInstructor();

            ViewData["YogaId"] = new SelectList(_context.Yoga, "YogaId", "Name");
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "FullName", Instructor.InstructorId);
            ViewData["YogaHallId"] = new SelectList(_context.YogaHall, "YogaHallId", "Name");

            var ListStringDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(x => x.ToString());
            ViewData["DaysOfWeek"] = new SelectList(ListStringDaysOfWeek, "Day");

            return View();
        }

        // POST: Instructor/YogaClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("YogaClassId,YogaId,YogaHallId,Day,TimeFrom,TimeTo")] YogaClassCreateVM yogaClass)
        {
            var Instructor = HttpContext.GetLoggedInInstructor();

            if (ModelState.IsValid)
            {
                var entry = new YogaClass
                {
                    Day = yogaClass.Day,
                    TimeFrom = yogaClass.TimeFrom,
                    YogaId = yogaClass.YogaId,
                    InstructorId = Instructor.InstructorId,
                    YogaHallId = yogaClass.YogaHallId
                };
                _context.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["YogaId"] = new SelectList(_context.Yoga, "YogaId", "Name", yogaClass.YogaId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "FullName", yogaClass.InstructorId);
            ViewData["YogaHallId"] = new SelectList(_context.YogaHall, "YogaHallId", "Name");

            var ListStringDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(x => x.ToString());
            ViewData["DaysOfWeek"] = new SelectList(ListStringDaysOfWeek, "Day");

            return View(yogaClass);
        }

        // GET: Instructor/YogaClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaClass = await _context.YogaClass.FindAsync(id);
            if (yogaClass == null)
            {
                return NotFound();
            }
            ViewData["YogaId"] = new SelectList(_context.Yoga, "YogaId", "Name", yogaClass.YogaId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "FullName", yogaClass.InstructorId);
            ViewData["YogaHallId"] = new SelectList(_context.YogaHall, "YogaHallId", "Name", yogaClass.YogaHallId);


            var ListStringDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString()
            });
            ViewData["DaysOfWeek"] = new SelectList(ListStringDaysOfWeek, "Value", "Text", yogaClass.Day);

            var mappedYogaClass = mapper.Map<YogaClassCreateVM>(yogaClass);

            return View(mappedYogaClass);
        }

        // POST: Instructor/YogaClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("YogaClassId,YogaId,YogaHallId,Day,TimeFrom,TimeTo")] YogaClassCreateVM yogaClass)
        {
            if (id != yogaClass.YogaClassId)
            {
                return NotFound();
            }

            var Instructor = HttpContext.GetLoggedInInstructor();
            if (ModelState.IsValid)
            {
                try
                {
                    var entry = new YogaClass
                    {
                        YogaClassId = yogaClass.YogaClassId,
                        YogaId = yogaClass.YogaId,
                        Day = yogaClass.Day,
                        TimeFrom = yogaClass.TimeFrom,
                        TimeTo = yogaClass.TimeTo,
                        InstructorId = Instructor.InstructorId,
                        YogaHallId = yogaClass.YogaHallId
                    };
                    _context.Update(entry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YogaClassExists(yogaClass.YogaClassId))
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
            ViewData["YogaId"] = new SelectList(_context.Yoga, "YogaId", "Name", yogaClass.YogaId);
            ViewData["InstructorId"] = new SelectList(_context.Instructor, "InstructorId", "FullName", yogaClass.InstructorId);
            ViewData["YogaHallId"] = new SelectList(_context.YogaHall, "YogaHallId", "Name", yogaClass.YogaHallId);

            var ListStringDaysOfWeek = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(x => x.ToString());
            ViewData["DaysOfWeek"] = new SelectList(ListStringDaysOfWeek, "Day", null, yogaClass.Day);

            return View(yogaClass);
        }

        // GET: Instructor/YogaClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yogaClass = await _context.YogaClass
                .Include(y => y.Yoga)
                .FirstOrDefaultAsync(m => m.YogaClassId == id);
            if (yogaClass == null)
            {
                return NotFound();
            }

            return View(yogaClass);
        }

        // POST: Instructor/YogaClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yogaClass = await _context.YogaClass.FindAsync(id);
            _context.YogaClass.Remove(yogaClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YogaClassExists(int id)
        {
            return _context.YogaClass.Any(e => e.YogaClassId == id);
        }
    }
}
