using MyWebApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext contaxt)
        {
            _context = contaxt;
        }
        public void AddProduct(Products model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }
        public void DeleteProduct(Guid Id)
        {
            var model = GetProductById(Id);
            _context.Remove(model);
            _context.SaveChanges();
        }
        public void EditProduct(Products model)
        {
            var product = GetProductById(model.Id);
            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Category = model.Category;


            _context.SaveChanges();
        }
        public List<Products> GetAllProducts()
        {
            return _context.Products.OrderBy(i => i.Name).Take(10).ToList();
        }
        public Products GetProductById(Guid Id)
        {
            return _context.Products.FirstOrDefault(i => i.Id == Id);
        }
        public List<Products> Paging(int? pageNumber)
        {
            var take = 10;
            var skip = (int)(pageNumber - 1)*10;
            return _context.Products.OrderBy(i => i.Name).Skip(skip).Take(take).ToList();
        }
    }
}