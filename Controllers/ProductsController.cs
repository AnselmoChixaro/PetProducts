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
    public class ProductsController : Controller
    {
        private readonly ProductsContext _context;

        public ProductsController(ProductsContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string sortOrder, string nameSearch, int? pageNumber)
        {
            ViewData["SortOrder"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParam"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["CategorySortParam"] = sortOrder == "category" ? "category_desc" : "category";
            ViewData["Filter"] = nameSearch;

            var products = from p in _context.Products select p;

            if(!String.IsNullOrEmpty(nameSearch))
            {
                pageNumber = 1;
                var proc = _context.Products.FromSqlRaw($"SelectProductByName {nameSearch}");
                await proc.ForEachAsync(p => p.Category = _context.Category.Where(c => c.ID == p.CategoryID).FirstOrDefault() );
                return View( proc );
            }

            ViewData["CurrentSearch"] = nameSearch;

            switch (sortOrder)
            {
                case "name_desc": products = products.OrderByDescending(p => p.Name);break;
                case "price": products = products.OrderBy(p => p.Price); break;
                case "price_desc": products = products.OrderByDescending(p => p.Price); break;
                case "category": products = products.OrderBy(c => c.Category); break;
                case "category_desc": products = products.OrderByDescending(c => c.Category); break;
                default: products = products.OrderBy(p => p.Name);break;
                    
            }

            int pageSize = 5;
            return View(products.AsNoTracking().Include( c => c.Category ).AsEnumerable());
            //return View(await products.AsNoTracking().ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            PopulateBrandsDropDown();
            PopulateCategoryDropDown();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CategoryID,Price,ProductBrandID")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists " +
                                         "Contact your administrator.");
            }
            PopulateBrandsDropDown(product.ProductBrandID);
            PopulateCategoryDropDown(product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            PopulateBrandsDropDown(product.ProductBrandID);
            PopulateCategoryDropDown(product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Price")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            var prodToUpdate = await _context.Products.FirstOrDefaultAsync(p => p.ID == product.ID);

            if (await TryUpdateModelAsync<Product>(prodToUpdate, "",
                p => p.Name,
                p => p.Price,
                p => p.CategoryID,
                p => p.ProductBrandID
                ))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch ( DbUpdateException )
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "Contact your administrator.");
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateBrandsDropDown(product.ProductBrandID);
            PopulateCategoryDropDown(product.CategoryID);
            return View(prodToUpdate);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            if(saveChangesError.GetValueOrDefault())
            {
                ViewData["Error Message"]= "Delete failed. Try again, and if the problem persists Contact your system administrator.";
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }

        private void PopulateBrandsDropDown(object selectedBrand = null)
        {
            var brandQuerie = from d in _context.Brands
                                 orderby d.Brand
                                 select d;

            ViewBag.Brands = new SelectList(brandQuerie.AsNoTracking(), "ID", "Brand", brandQuerie);
        }

        private void PopulateCategoryDropDown(object selectedCategory = null)
        {
            var categoryQuerie = from d in _context.Categories
                                 orderby d.CategoryName
                                 select d;

            ViewBag.CategoryID = new SelectList(categoryQuerie.AsNoTracking(), "ID", "CategoryName", selectedCategory);
        }
    }
}
