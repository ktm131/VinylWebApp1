using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinylWebApp1.Models;

namespace VinylWebApp1.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class VinylReservationsController : Controller
    {
        private readonly VinylContext _context;

        public VinylReservationsController(VinylContext context)
        {
            _context = context;
        }

        // GET: VinylReservations
        public async Task<IActionResult> Index()
        {
            var vinylContext = _context.Reservations.Include(v => v.DeliveryType).Include(v => v.Vinyl);
            return View(await vinylContext.ToListAsync());
        }

        // GET: VinylReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinylReservation = await _context.Reservations
                .Include(v => v.DeliveryType)
                .Include(v => v.Vinyl)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vinylReservation == null)
            {
                return NotFound();
            }

            return View(vinylReservation);
        }

        // GET: VinylReservations/Create
        public IActionResult Create()
        {
            ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryTypes, "Id", "Name");
            ViewData["VinylId"] = new SelectList(_context.Vinyls, "Id", "Name");
            return View();
        }

        // POST: VinylReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VinylId,UserId,DeliveryTypeId,ReservationDate,ReturnDate,Status")] VinylReservation vinylReservation)
        {
            if (vinylReservation != null)
            {
                if (vinylReservation.ReservationDate > vinylReservation.ReturnDate)
                {
                    ModelState.AddModelError("ReservationDate", "Data wypożyczenia jest późniejsza od daty zwrotu");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(vinylReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryTypes, "Id", "Display", vinylReservation.DeliveryTypeId);
            ViewData["VinylId"] = new SelectList(_context.Vinyls, "Id", "Name", vinylReservation.VinylId);
            return View(vinylReservation);
        }

        // GET: VinylReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinylReservation = await _context.Reservations.FindAsync(id);
            if (vinylReservation == null)
            {
                return NotFound();
            }
            ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryTypes, "Id", "Name", vinylReservation.DeliveryTypeId);
            ViewData["VinylId"] = new SelectList(_context.Vinyls, "Id", "Name", vinylReservation.VinylId);
            return View(vinylReservation);
        }

        // POST: VinylReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VinylId,UserId,DeliveryTypeId,ReservationDate,ReturnDate,Status")] VinylReservation vinylReservation)
        {
            if (id != vinylReservation.Id)
            {
                return NotFound();
            }

            if (vinylReservation != null)
            {
                if (vinylReservation.ReservationDate > vinylReservation.ReturnDate)
                {
                    ModelState.AddModelError("ReservationDate", "Data wypożyczenia jest późniejsza od daty zwrotu");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vinylReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VinylReservationExists(vinylReservation.Id))
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
            ViewData["DeliveryTypeId"] = new SelectList(_context.DeliveryTypes, "Id", "Display", vinylReservation.DeliveryTypeId);
            ViewData["VinylId"] = new SelectList(_context.Vinyls, "Id", "Name", vinylReservation.VinylId);
            return View(vinylReservation);
        }

        // GET: VinylReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vinylReservation = await _context.Reservations
                .Include(v => v.DeliveryType)
                .Include(v => v.Vinyl)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vinylReservation == null)
            {
                return NotFound();
            }

            return View(vinylReservation);
        }

        // POST: VinylReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vinylReservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(vinylReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VinylReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
