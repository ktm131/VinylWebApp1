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
    public class DeliveryTypesController : Controller
    {
        private readonly VinylContext _context;

        public DeliveryTypesController(VinylContext context)
        {
            _context = context;
        }

        // GET: DeliveryTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryTypes.ToListAsync());
        }

        // GET: DeliveryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            return View(deliveryType);
        }

        // GET: DeliveryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Days,Price")] DeliveryType deliveryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryType);
        }

        // GET: DeliveryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryTypes.FindAsync(id);
            if (deliveryType == null)
            {
                return NotFound();
            }
            return View(deliveryType);
        }

        // POST: DeliveryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Days,Price")] DeliveryType deliveryType)
        {
            if (id != deliveryType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryTypeExists(deliveryType.Id))
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
            return View(deliveryType);
        }

        // GET: DeliveryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            return View(deliveryType);
        }

        // POST: DeliveryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryType = await _context.DeliveryTypes.FindAsync(id);
            _context.DeliveryTypes.Remove(deliveryType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryTypeExists(int id)
        {
            return _context.DeliveryTypes.Any(e => e.Id == id);
        }
    }
}
