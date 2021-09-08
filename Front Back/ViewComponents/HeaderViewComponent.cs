using Front_Back.DAL;
using Front_Back.Models;
using Front_Back.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front_Back.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string basket = HttpContext.Request.Cookies["basket"];

            BasketVM basketVM = new BasketVM
            {
                BasketItemVMs = new List<BasketItemVM>(),
                TotalPrice=0,
                Count   =0
            };

            if (basket != null)
            {
                List<CookieItemVM> cookieItemVMs = JsonConvert.DeserializeObject<List<CookieItemVM>>(basket);

                foreach (var item in cookieItemVMs)
                {
                    Product product = _context.Products.FirstOrDefault(p => p.Id == item.Id);

                  if(product != null)
                    {
                        BasketItemVM basketItemVM = new BasketItemVM
                        {
                            Product = product,
                            Count = item.Count
                        };
                        basketVM.BasketItemVMs.Add(basketItemVM);
                        basketVM.TotalPrice += (decimal)(item.Count * product.Price);
                        basketVM.Count++;
                    }
                }
            
            }

            return View(await Task.FromResult(basketVM));
        }

    }
}