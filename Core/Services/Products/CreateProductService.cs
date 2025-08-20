using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;

namespace Core.Services.Products
{
    [AutoRegister]
    public class CreateProductService : ICreateProductService
    {
        private readonly IUpdateProductService _updateProductService;
        private readonly IIdObjectFactory<Product> _productFactory;
        private readonly IProductRepository _productRepository;

        public CreateProductService(IIdObjectFactory<Product> productFactory, IProductRepository productRepository, IUpdateProductService updateProductService)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
            _updateProductService = updateProductService;
        }

        public Product Create(Guid id, string name, string description,decimal? price, int quantity)
        {
            var product = _productFactory.Create(id);
            _updateProductService.Update(product, name, description, price, quantity);
            _productRepository.Save(product);
            return product;
        }
    }
}