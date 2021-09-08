using Front_Back.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front_Back.Controllers
{
    public class ProductController : Controller
    {
        public AppDbContext _context { get; }
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public  async  Task <IActionResult> Index()
        {
            ViewBag.Product = _context.Products.Where(p => p.IsDeleted == false).Count();
          
            return View ();
        }
        public async Task<IActionResult> LoadMore(int take=8,int skip=12)
        {
            var model = await _context.Products.Include(p => p.Images).Where(p => p.IsDeleted == false).OrderByDescending(p=>p.Id).Skip(skip).Take(take).ToListAsync();

            return PartialView("_productPartial",model);
            
            //return Json( _context.Products.Include(p=>p.Images).Select(p=>new { Id=p.Id,
            //    Name=p.Name,Price=p.Price,Image=p.Images.FirstOrDefault().Image.Skip(12).Take(8).ToList());
        }
    }
}
