using Front_Back.DAL;
using Front_Back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front_Back.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        public AppDbContext _context { get; }

        public ProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync( int take )
        {
            var model =  _context.Products.Include(p => p.Images)
                .Where(p => p.IsDeleted == false).Take(take);
            return View(await Task.FromResult(model));
        var model1 = _context.Categories.Include(c => c.Products).Where(c => c.IsDeleted == false);
            return View(await Task.FromResult(model1));
            
        }


   
    }
    }

