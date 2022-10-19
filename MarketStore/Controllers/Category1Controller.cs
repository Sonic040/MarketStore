using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketStore.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace MarketStore.Controllers
{
    public class Category1Controller : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Category1Controller(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Category1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category1s.ToListAsync());
        }

        // GET: Category1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category1 = await _context.Category1s
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category1 == null)
            {
                return NotFound();
            }

            return View(category1);
        }

        // GET: Category1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Image,ImageFile")] Category1 category1 )
        {
            if (ModelState.IsValid)
            {
                if (category1.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + category1.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using(var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await category1.ImageFile.CopyToAsync(fileStream);
                    }
                    category1.Image = fileName;
                    _context.Add(category1);
                }
                _context.Add(category1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category1);
        }

        // GET: Category1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category1 = await _context.Category1s.FindAsync(id);
            if (category1 == null)
            {
                return NotFound();
            }
            return View(category1);
        }

        // POST: Category1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("CategoryId,Name,Image,ImageFile")] Category1 category1)
        {
            if (id != category1.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (category1.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath; 
                        string fileName = Guid.NewGuid().ToString() + "_" + category1.ImageFile.FileName; 
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await category1.ImageFile.CopyToAsync(fileStream);
                        }
                        category1.Image = fileName;
                    }
                    _context.Update(category1); await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Category1Exists((int)category1.CategoryId))
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
            return View(category1);

    }

    // GET: Category1/Delete/5
    public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category1 = await _context.Category1s
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category1 == null)
            {
                return NotFound();
            }

            return View(category1);
        }

        // POST: Category1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var category1 = await _context.Category1s.FindAsync(id);
            _context.Category1s.Remove(category1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Category1Exists(decimal id)
        {
            return _context.Category1s.Any(e => e.CategoryId == id);
        }
    }
}
