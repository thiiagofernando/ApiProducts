using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProdutCatalog.Models;
using ProdutCatalog.Repositories;
using ProdutCatalog.ViewModels.ProductViewModel;

namespace ProdutCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;
        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/products")]
        [ResponseCache(Duration=10)]
        [HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/products/{id}")]
        [ResponseCache(Location = ResponseCacheLocation.Client,Duration=10)]
        [HttpGet]
        public Product Get(int id)
        {
           return _repository.Get(id);
        }

        [Route("v1/pruducts")]
        [HttpPost]
        public ResultViewModel Post([FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Nao foi possível cadastrar o Produto",
                    Data = model.Notifications
                };

            var product = new Product();
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _repository.Save(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto Cadastrado com sucesso",
                Data = product
            };
        }

        [Route("v1/pruducts")]
        [HttpPut]
        public ResultViewModel Put([FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if(model.Invalid)
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Nao foi possível alterar o Produto",
                    Data = model.Notifications
                };

            var product = _repository.Get(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _repository.Update(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto Cadastrado com sucesso",
                Data = product
            };
        }
    }
}