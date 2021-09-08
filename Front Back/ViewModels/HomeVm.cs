using Front_Back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front_Back.ViewModels
{
    public class HomeVm
    {
        public List <Slide> Slides { get; set; }
        public Introduction Introduction { get; set; }

       
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; internal set; }
    }
}
