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
using VinylWebApp1.Models;

namespace VinylWebApp1.Controllers
{
    [Authorize]
    public class UserVinylsController : Controller
    {
        private readonly VinylContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserVinylsController(VinylContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Vinyls1
        public async Task<IActionResult> Index()
        {
            var vinyls = await _context.Vinyls
                .Include(i => i.VinylReservations)
                .Where(w=>!w.VinylReservations.Any(a=>a.ReservationDate <= DateTime.Now && (a.ReturnDate == DateTime.MinValue || DateTime.Now < a.ReturnDate) && (a.Status == Status.Reserved || a.Status == Status.Sent)))
                .ToListAsync();

            return View(vinyls);
        }

        // GET: Vinyls1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinyl = await _context.Vinyls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vinyl == null)
            {
                return NotFound();
            }

            return View(vinyl);
        }

        public async Task<IActionResult> Reservations()
        {
            var user = await _userManager.GetUserAsync(User);
            var reservations = await _context.Reservations.Where(w=>w.UserId == user.Id).ToListAsync();
            return View(reservations);
        }

        public async Task<IActionResult> Reserve(int id)
        {
            ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryTypes, "Id", "Name");
            return View(new VinylReservation() { VinylId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve([Bind("VinylId,DeliveryTypeId,Return,Payment")] VinylReservation vinylReservation)
        {
            if (ModelState.IsValid)
            {
                vinylReservation.ReservationDate = DateTime.Now;
                vinylReservation.Status = Status.Reserved;
                var user = await _userManager.GetUserAsync(User);
                vinylReservation.UserId = user.Id;
                _context.Add(vinylReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vinylReservation);
        }



    }
}
