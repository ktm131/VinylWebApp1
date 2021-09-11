using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinylWebApp1.Areas.Identity.Models;
using VinylWebApp1.Data;
using VinylWebApp1.Models;

namespace VinylWebApp1.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class User1Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public User1Controller(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: User1
        public async Task<IActionResult> Index()
        {
            var usersList = await _context.Users.Select(s => new User1() { Id = s.Id, Name = s.Email}).ToListAsync();

            foreach(var u in usersList)
            {
                var ident = await _userManager.FindByIdAsync(u.Id);
                u.IsAdmin = await _userManager.IsInRoleAsync(ident, "Administrator");
            }

            return View(usersList);
        }

        // GET: User1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user1 = await _userManager.FindByIdAsync(id);
            if (user1 == null)
            {
                return NotFound();
            }
            var ident = await _userManager.FindByIdAsync(user1.Id);
            bool isAdm = await _userManager.IsInRoleAsync(ident, "Administrator");
           
            return View(new User1() { Id = user1.Id, Name = user1.Email, IsAdmin = isAdm });
        }

        // GET: User1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,IsAdmin")] User1 user1)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser us = new ApplicationUser();
                us.Email = user1.Name;
                us.UserName = user1.Name;

                IdentityResult newUser = await _userManager.CreateAsync(us, user1.Password);


                if (user1.IsAdmin)
                {
                    var fuser = await _userManager.FindByNameAsync(user1.Name);
                    await _userManager.AddToRoleAsync(fuser, "Administrator");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(user1);
        }

        // GET: User1/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user1 = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user1 == null)
            {
                return NotFound();
            }

            return View(user1);
        }

        // POST: User1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
           
            var user1 = await _context.Users.FindAsync(id);
            if (await _userManager.IsInRoleAsync(user1, "Administrator"))
            {
                await _userManager.RemoveFromRoleAsync(user1, "Administrator");
            }
            _context.Users.Remove(user1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool User1Exists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
