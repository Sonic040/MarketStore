using MarketStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketStore.Controllers
{
    public class EditUserController : Controller
    {
        private readonly ModelContext _context;

        public EditUserController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user1 = await _context.User1s.FindAsync(id);
            if (user1 == null)
            {
                return NotFound();
            }
            ViewData["Homeid"] = new SelectList(_context.Homes, "Homeid", "Homeid", user1.Homeid);
            ViewData["Roleid"] = new SelectList(_context.Role1s, "Roleid", "Roleid", user1.Roleid);
            return View(user1);
        }

        // POST: User1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fname,Lname,Email,Password,ImagePath,Roleid,Homeid")] User1 user1)
        {
            if (id != user1.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!User1Exists(user1.Userid))
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
            ViewData["Homeid"] = new SelectList(_context.Homes, "Homeid", "Homeid", user1.Homeid);
            ViewData["Roleid"] = new SelectList(_context.Role1s, "Roleid", "Roleid", user1.Roleid);
            return View(user1);
        }

        private bool User1Exists(decimal userid)
        {
            throw new NotImplementedException();
        }
    }
}
