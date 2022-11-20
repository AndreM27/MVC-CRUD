using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Data;
using MVC_CRUD.Models;

namespace MVC_CRUD.Controllers
{
    public class UsersController : Controller
    {
        private readonly MVC_CRUDContext _context;
        //Dependency injection
        public UsersController(MVC_CRUDContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'MVC_CRUDContext.Users'  is null.");
        }

        

        // GET: Users Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Surname")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users Read
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users Update
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Surname")] Users users)
        {
            if (id != users.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.ID))
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
            return View(users);
        }

        // GET: Users Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'MVC_CRUDContext.Users'  is null.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
          return (_context.Users?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        
    }
}
