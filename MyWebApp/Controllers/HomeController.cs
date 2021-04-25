using Microsoft.AspNetCore.Mvc;
using MyWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _repository;
        public HomeController(IProductRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index(int? pageNumber, int? previous, int? next)
        {
            var model = Paging(pageNumber, previous, next);
            return View(model);
        }
        [NonAction]
        public List<Dto.Products> Paging(int? pageNumber, int? previous, int? next)
        {
            if (!pageNumber.HasValue && !previous.HasValue && !next.HasValue)
            {
                return _repository.GetAllProducts();
            }
            else
            {
                if (next.HasValue)
                {
                    pageNumber += 1;
                }
                else if (previous.HasValue)
                {
                    if (pageNumber > 1)
                    {
                        pageNumber -= 1;
                    }
                }
                ViewData["pageNumber"] = pageNumber;
                return _repository.Paging(pageNumber);
            }
        }
    }
}
