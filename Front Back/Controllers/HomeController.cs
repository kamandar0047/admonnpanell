using Front_Back.DAL;
using Front_Back.Models;
using Front_Back.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Front_Back.Controllers
{
    public class HomeController : Controller
    {
        public AppDbContext _context { get; }
        public HomeController (AppDbContext context)
        {
            _context = context;
        }
            public  async Task<IActionResult> Index()
        {
            HomeVm homeVM = new HomeVm
            {
                Slides = await _context.Slides.ToListAsync(),
                Introduction = await _context.Introduction.FirstOrDefaultAsync(),
               Categories =await _context.Categories.Where(c => c.IsDeleted == false).ToListAsync(),
               Products=await _context.Products.ToListAsync()
            };
          //  HttpContext.Session.SetString("name", "Kamandar");
          //  //Response.Cookies.Append("surname", "Muradali");
          //  List<BasketProduct> baskets = new List<BasketProduct>
          // {
          //      new BasketProduct{Id=1,Count=2},
          //       new BasketProduct{Id=2,Count=3},
          //       new BasketProduct{Id=3,Count=4},
          //         new BasketProduct{Id=4,Count=5}
          // };
          //Response.Cookies.Append("basket", JsonSerializer.Serialize(baskets));
            return View(homeVM);
        
        }
        public IActionResult AddBasket(int? id)
        {

            Product product = _context.Products.FirstOrDefault(p => p.Id == id);
            if ( product==null) return NotFound();

            string basket = HttpContext.Request.Cookies["basket"];

          

            if (basket == null)

            {

                List<CookieItemVM> products = new List<CookieItemVM>();

                products.Add(new CookieItemVM
                {
                    Id = product.Id,
                    Count = 1
                });
                string basketStr = JsonConvert.SerializeObject(products);
                HttpContext.Response.Cookies.Append("basket", basketStr);
            }
            else
            {

                List<CookieItemVM> products = JsonConvert.DeserializeObject<List<CookieItemVM>>(basket);

                CookieItemVM cookieItem = products.FirstOrDefault(p => p.Id == product.Id);
               if(cookieItem == null) {
                    products.Add(new CookieItemVM
                    {
                        Id = product.Id,
                        Count = 1
                    });
                }
                else
                {
                    cookieItem.Count++;
                }

                string basketStr = JsonConvert.SerializeObject(products);
                HttpContext.Response.Cookies.Append("basket", basketStr);
           

            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ShowBasket()
        {
            return Content(HttpContext.Request.Cookies["basket"]);
        }
    }
}
