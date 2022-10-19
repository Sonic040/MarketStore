using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MarketStore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MarketStore.Controllers
{
    public class Store1Controller : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Store1Controller(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Store1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Store1s.ToListAsync());
        }

        // GET: Store1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store1 = await _context.Store1s
                .FirstOrDefaultAsync(m => m.Storeid == id);
            if (store1 == null)
            {
                return NotFound();
            }

            return View(store1);
        }

        // GET: Store1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Store1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Storeid,Namestore,Img")] Store1 store1)
        {
            if (ModelState.IsValid)
            {
                if (store1.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + store1.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await store1.ImageFile.CopyToAsync(fileStream);
                    }
                    store1.Img = fileName;
                    _context.Add(store1);
                }
                _context.Add(store1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store1);
        }

        // GET: Store1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store1 = await _context.Store1s.FindAsync(id);
            if (store1 == null)
            {
                return NotFound();
            }
            return View(store1);
        }

        // POST: Store1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Storeid,Namestore,Img")] Store1 store1)
        {
            if (id != store1.Storeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (store1.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + store1.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await store1.ImageFile.CopyToAsync(fileStream);
                        }

                        store1.Img = fileName;
                    }
                    _context.Update(store1); await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Store1Exists((int)store1.Storeid))
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
            return View(store1);
        }

        // GET: Store1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var store1 = await _context.Store1s
                .FirstOrDefaultAsync(m => m.Storeid == id);
            if (store1 == null)
            {
                return NotFound();
            }

            return View(store1);
        }

        // POST: Store1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var store1 = await _context.Store1s.FindAsync(id);
            _context.Store1s.Remove(store1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Store1Exists(decimal id)
        {
            return _context.Store1s.Any(e => e.Storeid == id);
        }
    }
}
