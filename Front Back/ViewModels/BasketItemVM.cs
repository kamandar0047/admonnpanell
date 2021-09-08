using Front_Back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front_Back.ViewModels
{
    public class BasketItemVM
    {
        public  Product Product { get; set; }
        public decimal Count { get; set; }
    }
}
