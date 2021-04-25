using Microsoft.AspNetCore.Mvc;
using MyWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Controllers
{
    public class HomeController:Controller
    {
        private readonly IProductRepository _repository;
        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var model=_repository.GetAllProducts();
            return View(model);
        }
        public IActionResult Test()
        {
            return View();
        }
       
    }
}
