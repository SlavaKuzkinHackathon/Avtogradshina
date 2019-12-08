using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using avtogradshina.Models;
using avtogradshina.Models.Admin;
using avtogradshina.Models.Pages;
using avtogradshina.Infrastructure;

namespace avtogradshina.Controllers
{
    public class HomeController : Controller
    {

        
        public IDataRepository productRepository;
        private ICategoryRepository categoryRepository;

        public HomeController(IDataRepository repo, ICategoryRepository catRepo)
        {
            productRepository = repo;
            categoryRepository = catRepo;
        }


        public IActionResult Index([FromQuery(Name = "options")]
                QueryOptions productOptions,
                QueryOptions catOptions,
                long category)
        {
            ViewBag.Categories = categoryRepository.GetCategories(catOptions);
            ViewBag.SelectedCategory = category;
            return View(productRepository.GetProducts(productOptions, category));
        }
        

        //modal
        public ActionResult Details(long id)
        {
            ViewBag.CreateMode = false;
            return View("Details", productRepository.GetProduct(id));
        }




        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
