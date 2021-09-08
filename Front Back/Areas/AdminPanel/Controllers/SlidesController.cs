using Front_Back.DAL;
using Front_Back.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Front_Back.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SlidesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
       
        public SlidesController(AppDbContext context, IWebHostEnvironment env) {

            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Slides);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(Slide  slide)
        {

            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid)
                if(!slide.Photo.ContentType.Contains("image/"))
            {
                    ModelState.AddModelError("Photo","Please select image format");
                return View();
            }
            if (slide.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Please select image must be less than 200kbb");
                return View();
            }
            string fileName = Guid.NewGuid().ToString() + slide.Photo.FileName;
            string resultPath = Path.Combine(_env.WebRootPath, "img", fileName);
            using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await slide.Photo.CopyToAsync(fileStream);
            }
            Slide newSlide = new Slide
            {
                Image = fileName
            };

            _context.Add(newSlide);
            await _context.SaveChangesAsync();

         return RedirectToAction(nameof(Index));
        }

    }

    
}
