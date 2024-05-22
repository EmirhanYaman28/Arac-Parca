using System;
using System.IO; // Eklenen satır
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using AracParca.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Authorization;
using AracParca.Data;


namespace AracParca.Controllers
{
    public class ParcaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public ParcaController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;

        }


        public IActionResult Index()
        {
            IEnumerable<Parca> parca = _context.Parcas;
            return View(parca);
        }




        public IActionResult Görüntüle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var krs = _context.Parcas.Find(id);
            if (krs == null)
            {
                return NotFound();
            }
            return View(krs);
        }




        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewParca par)

        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (par.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + par.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    par.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Parca newProduct = new Parca
                {
                    ModelAralik = par.ModelAralik,
                    ArabaMarka = par.ArabaMarka,
                    ArabaModel = par.ArabaModel,
                    Bolum = par.Bolum,
                    Fiyatı = par.Fiyatı,
                    PhotoPath = uniqueFileName

                };


                _context.Parcas.Add(newProduct);
                _context.SaveChanges();
                TempData["SuccessMsg"] = par.Bolum + " bölümdeki ürün , ürün listesine eklendi";
                return RedirectToAction("Index");
            }

            return View();
        }



        public IActionResult Edit(int? id)
        {
            var parca = _context.Parcas.Find(id);

            CreateViewParca createViewProduct = new CreateViewParca
            {
                ModelAralik = parca.ModelAralik,
                ArabaMarka = parca.ArabaMarka,
                ArabaModel = parca.ArabaModel,
                Bolum = parca.Bolum,
                Fiyatı = parca.Fiyatı,
                PhotoFileName = parca.PhotoPath
            };

            if (parca == null)
            {
                return NotFound();
            }
            return View(createViewProduct);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateViewParca parca)
        {
            if (id != parca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = parca.PhotoFileName;
                if (parca.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + parca.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    parca.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Parca updateProduct = new Parca
                {
                    Id = parca.Id,
                    ModelAralik = parca.ModelAralik,
                    ArabaMarka = parca.ArabaMarka,
                    ArabaModel = parca.ArabaModel,
                    Bolum = parca.Bolum,
                    Fiyatı = parca.Fiyatı,
                    PhotoPath = uniqueFileName
                };

                _context.Parcas.Update(updateProduct);
                _context.SaveChanges();
                TempData["SuccessMsg"] = updateProduct.Bolum + " bölümündeki ürün, güncellendi";
                return RedirectToAction("Index");
            }
            return View(parca);
        }






        public IActionResult Delete(int? id)
        {
            var product = _context.Parcas.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var product = _context.Parcas.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Parcas.Remove(product);
            _context.SaveChanges();
            TempData["SuccessMsg"] = product.Bolum + " bölümündeki ürün, ürün listesinden silindi";
            return RedirectToAction("Index");
        }




    }
}
