using MyWebApp.Dto;
using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApp.Repository
{
    public interface IProductRepository
    {
        List<Products> GetAllProducts();
        Products GetProductById(Guid Id);
        void AddProduct(Products model);
        void EditProduct(Products model);
        void DeleteProduct(Guid Id);
    }
}
