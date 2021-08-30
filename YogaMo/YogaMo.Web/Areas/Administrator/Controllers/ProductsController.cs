using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YogaMo.Web.Areas.Administrator.ViewModels;
using YogaMo.Web.Controllers;
using YogaMo.Web.Helpers;
using YogaMo.WebAPI.Database;
using static YogaMo.Web.Areas.Administrator.ViewModels.ProductsIndexVM;

namespace YogaMo.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductsController : BaseController
    {
        public ProductsController(_150222Context context, IMapper mapper) : base(context, mapper)
        {
        }

        // GET: Administrator/Products
        public async Task<IActionResult> Index(int CategoryId)
        {
            var query = _context.Product.Include(p => p.Category).AsQueryable();
            if(CategoryId != 0)
            {
                query = query.Where(x => x.CategoryId == CategoryId);
            }

            var list = await query.ToListAsync();

            var VM = new ProductsIndexVM
            {
                CategoryId = CategoryId,
                Rows = _mapper.Map<List<ProductsVM>>(list)
            };

            ViewData["Categories"] = new SelectList(_context.Category, "CategoryId", "Name");

            return View(VM);
        }

        // GET: Administrator/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            return View();
        }

        // POST: Administrator/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CategoryId,QuantityStock,Price,Description,Color")] Product product, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null && Photo.Length > 0)
                {
                    product.Photo = ImageHelper.ConvertUploadToByteArray(Photo);
                }

                product.Status = true;

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Administrator/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Administrator/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,CategoryId,QuantityStock,Price,Status,Description,Color")] Product product, IFormFile Photo, bool RemoveProfilePicture)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = _context.Product.Find(id);

                    if (Photo != null && Photo.Length > 0)
                    {
                        product.Photo = ImageHelper.ConvertUploadToByteArray(Photo);
                    }
                    else if (RemoveProfilePicture)
                    {
                        product.Photo = null;
                    }
                    else
                    {
                        product.Photo = entity.Photo;
                    }

                    _mapper.Map(product, entity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
