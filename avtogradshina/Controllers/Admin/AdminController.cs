using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using avtogradshina.Models.Admin;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using avtogradshina.Models;
using Microsoft.AspNetCore.Authorization;
using avtogradshina.Models.Pages;

namespace avtogradshina.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationContext context;
        private IDataRepository repository;
        private IHostingEnvironment _appEnvironment;
        private ICategoryRepository catRepository;

        public IActionResult Index(QueryOptions options)
        {
            return View(repository.GetProducts(options));
        }

        public AdminController( IDataRepository repo, ICategoryRepository catRepo,
        IHostingEnvironment appEnvironment, ApplicationContext ctx)
        {
            context = ctx;
            repository = repo;
            catRepository = catRepo;
            _appEnvironment = appEnvironment;
        }
       
        public IActionResult Create()
        {
            ViewBag.Categories = catRepository.Categories;
            ViewBag.CreateMode = true;
            return View("Editor", new Product());
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile uploadedFile)
        {
           
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                product.NameImage = uploadedFile.FileName;
                product.Path = path;
            }
            repository.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(long id)
        {
            ViewBag.Categories = catRepository.Categories;
            ViewBag.CreateMode = false;
            return View("Editor", repository.GetProduct(id));
        }

        //Загрузка файлов на сервер
        [HttpPost]
        public async Task <IActionResult> Edit(Product product, Product original, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                product.NameImage = uploadedFile.FileName;
                product.Path = path;
            }
            repository.UpdateProduct(product, original);
            return RedirectToAction(nameof(Index));
        }

       

        [HttpPost]
        public IActionResult Delete(long id)
        {
            repository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

