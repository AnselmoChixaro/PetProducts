using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetProducts.Context;
using PetProducts.Models;

namespace PetProducts.Controllers
{
    public class ProductBrandsController : Controller
    {
        private readonly ProductsContext _context;

        public ProductBrandsController(ProductsContext context)
        {
            _context = context;
        }

        // GET: ProductBrands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // GET: ProductBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productBrand = await _context.Brands
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productBrand == null)
            {
                return NotFound();
            }

            return View(productBrand);
        }

        // GET: ProductBrands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Brand,Country")] ProductBrand productBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productBrand);
        }

        // GET: ProductBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productBrand = await _context.Brands.FindAsync(id);
            if (productBrand == null)
            {
                return NotFound();
            }
            return View(productBrand);
        }

        // POST: ProductBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Brand,Country")] ProductBrand productBrand)
        {
            if (id != productBrand.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductBrandExists(productBrand.ID))
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
            return View(productBrand);
        }

        // GET: ProductBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productBrand = await _context.Brands
                .FirstOrDefaultAsync(m => m.ID == id);
            if (productBrand == null)
            {
                return NotFound();
            }

            return View(productBrand);
        }

        // POST: ProductBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productBrand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(productBrand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductBrandExists(int id)
        {
            return _context.Brands.Any(e => e.ID == id);
        }
    }
}
