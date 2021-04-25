using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Dto;
using MyWebApp.Models;
using MyWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddOrEdit(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return View();
            }
            else
            {
                var model = _repository.GetProductById(Id);
                return View(model);
            }
        }
        [Route("[Controller]")]
        public IActionResult Search(string Search)
        {
            if (string.IsNullOrEmpty(Search))
            {
                return View("AllProduct");
            }
            else
            {
                var result = _repository.GetAllProducts().Where(i => i.Name.Contains(Search) || i.Category == Search).ToList();
                if (result.Count <= 0)
                {
                    return View("AllProduct");
                }
                else
                {
                    return View("AllProduct", result);
                }
            }
        }
        public IActionResult AllProduct()
        {
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(Products model)
        {
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }
            _repository.AddProduct(model);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult Edit(Products model)
        {
            _repository.EditProduct(model);
            return RedirectToAction("Detail", "Product", new { Id = model.Id });
        }
        public IActionResult Detail(Guid Id)
        {
            var model = _repository.GetProductById(Id);
            return View(model);
        }
        [Authorize]
        public IActionResult Delete(Guid Id)
        {
            _repository.DeleteProduct(Id);
            return RedirectToAction("Index", "Home");
        }

    }
}
