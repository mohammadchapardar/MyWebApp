using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        public IActionResult Index(int? pageNumber,string search)
        {
            List<Products> model;
            if (pageNumber==null)
            {
                pageNumber = 1;
                ViewData["pageNumber"] = 1;
            }
            if (string.IsNullOrEmpty(search))
            {
                model = _repository.GetAllProducts().Skip((int)(pageNumber - 1) * 10).Take(10).ToList();
                ViewData["Count"] = _repository.GetAllProducts().Count;
                var count = decimal.Parse(ViewData["Count"].ToString());
                ViewData["pageCount"] = Math.Ceiling(count / 10);
                ViewData["pageNumber"] = pageNumber;
                var nextPage = pageNumber + 1;
                var nextPageModel = _repository.GetAllProducts().Skip((int)(nextPage - 1) * 10).Take(10).ToList();
                ViewData["isLastPage"] = "false";
                if (nextPageModel.Count == 0)
                    ViewData["isLastPage"] = "true";
            }
            else
            {
                var allProducts = _repository.GetAllProducts().Where(i => i.Category == search || i.Name.Contains(search)).ToList();
                ViewData["Count"] = allProducts.Count;
                var count = decimal.Parse(ViewData["Count"].ToString());
                ViewData["pageCount"] = Math.Ceiling(count / 10);
                ViewData["pageNumber"] = pageNumber;
                var nextPage = pageNumber + 1;
                var nextPageModel = allProducts.Skip((int)(nextPage - 1) * 10).Take(10).ToList();
                ViewData["isLastPage"] = "false";
                if (nextPageModel.Count == 0)
                    ViewData["isLastPage"] = "true";
                ViewData["Search"] = search;
                model = allProducts.Skip((int)(pageNumber - 1) * 10).Take(10).ToList();
            }
            return View(model);
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
        [Authorize]
        [HttpPost]
        public IActionResult Add(Products model)
        {
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
            }
            _repository.AddProduct(model);
            return View("Index");
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
            return View("Index");
        }

    }
}
