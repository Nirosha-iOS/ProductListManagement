using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductListManagement.Data;
using ProductListManagement.Models;

namespace ProductListManagement.Controllers
{
    public class BrandController : Controller
    {
        private readonly AppDBContect _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(AppDBContect dbContect, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContect;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Brand> brands = _dbContext.Brands.ToList();
            return View(brands);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Brand model)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                String newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\brand");
                var extension = Path.GetExtension(file[0].FileName);

                //delete old image
                var objectFromDB = _dbContext.Brands.FirstOrDefault(x => x.Id == model.Id);
               
                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                model.BrandLogo = @"\images\brand\" + newFileName + extension;
            }

            if (ModelState.IsValid)
            {
                _dbContext.Brands.Add(model);
                _dbContext.SaveChanges();
                TempData["success"] = "Record Created Successfully";
                return RedirectToAction("Index");

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            var brand = _dbContext.Brands.FirstOrDefault(b => b.Id == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);

        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var brand = _dbContext.Brands.FirstOrDefault(b => b.Id == id);
           
            return View(brand);
        }
        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                String newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\brand");
                var extension = Path.GetExtension(file[0].FileName);
               //delete old image
               var objectFromDB = _dbContext.Brands.AsNoTracking().FirstOrDefault(x=> x.Id == brand.Id);
                if (objectFromDB.BrandLogo != null) 
                {
                    var oldimagePath = Path.Combine(webRootPath, objectFromDB.BrandLogo.Trim('\\'));
                    if (System.IO.File.Exists(oldimagePath))
                    {
                        System.IO.File.Delete(oldimagePath);
                    }

                }


                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                brand.BrandLogo = @"\images\brand\" + newFileName + extension;
            }
            if(ModelState.IsValid)
            {
                var objectFromDB = _dbContext.Brands.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);
                objectFromDB.Name = brand.Name;
                objectFromDB.EstablisedYear = brand.EstablisedYear;
                objectFromDB.Headquarters = brand.Headquarters;
                objectFromDB.Founder = brand.Founder;
                objectFromDB.CEO = brand.CEO;
                objectFromDB.Industry = brand.Industry;
                objectFromDB.Description = brand.Description;
                objectFromDB.WebsiteURL = brand.WebsiteURL;
                objectFromDB.ParentCompany = brand.ParentCompany;
                if (brand.BrandLogo != null)
                {
                    objectFromDB.BrandLogo = brand.BrandLogo;
                }
                _dbContext.Brands.Update(objectFromDB);
                _dbContext.SaveChanges();
                TempData["success"] = "Record Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
       

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var deleteBrand = _dbContext.Brands.FirstOrDefault(x => x.Id == id);
            if (deleteBrand != null)
            {
                
            }
            return View(deleteBrand);
        }
        [HttpPost]
        public IActionResult Delete(Brand brand) 
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(brand.BrandLogo)) 
            {
                var objectFromDB = _dbContext.Brands.AsNoTracking().FirstOrDefault(x => x.Id == brand.Id);
                if (objectFromDB.BrandLogo != null)
                {
                    var oldimagePath = Path.Combine(webRootPath, objectFromDB.BrandLogo.Trim('\\'));
                    if (System.IO.File.Exists(oldimagePath))
                    {
                        System.IO.File.Delete(oldimagePath);
                    }

                }
            }

            _dbContext.Brands.Remove(brand);
            _dbContext.SaveChanges();

            TempData["success"] = "Record Deleted Successfully";
            return RedirectToAction("Index");
        } 

    }
}
