using Front_Back.DAL;
using Front_Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front_Back.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        public AppDbContext _context { get; }
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            bool hasCategory = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (hasCategory)
            {
                ModelState.AddModelError("Name","This category already exist");
                return View();
            }
             _context.Categories.AddAsync(category);
             _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update (int? id)
        {
            if (id == null)
                return NotFound();
            var category = await  _context.Categories.
                FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
            if (category == null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Update(int? id,Category category)
        {
            if (id == null) return NotFound();
            if (id != category.Id) return BadRequest();
            var categoryDb =  _context.Categories
                .FirstOrDefault(c => c.Id==id && c.IsDeleted == false);
         
            if (category == null) return NotFound();
            bool hasCategory = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (hasCategory)
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }
           
            categoryDb.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); 
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();
            return View(category);
        
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)return NotFound();
            var category = await _context.Categories.FindAsync( id);
            if (category == null) return NotFound();
            if (category.IsDeleted)
            {
                category.IsDeleted = false;
            }
            else
            {
                category.IsDeleted = true;
            }
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
    }
}
