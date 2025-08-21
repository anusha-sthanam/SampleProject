using BusinessEntities;
using Core.Services.Products;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(ICreateProductService createProductService, IGetProductService getProductService, IDeleteProductService deleteProductService, IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _getProductService = getProductService;
            _deleteProductService = deleteProductService;
            _updateProductService = updateProductService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            if (model == null || productId == Guid.Empty || model.Price <=0 || string.IsNullOrWhiteSpace(model.Name) || model.Quantity<0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data provided.");
            }
            try
            {
                // Check if a product with the same productId already exists
                var existingProduct = _getProductService.GetProductById(productId);
                if (existingProduct != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, $"This product with the productId:{productId} already exists.");
                }
                var product = _createProductService.Create(productId, model.Name, model.Description, model.Price, model.Quantity);
                return Found(new ProductData(product));
            }
            catch (ArgumentNullException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message); 
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while creating the Product", ex);
            }
        }

        [HttpPost]
        [Route("{id:guid}/update")]
        public HttpResponseMessage UpdateProduct(Guid id, [FromBody] ProductModel model)
        {
            if (model == null || id == Guid.Empty || model.Price <= 0 || string.IsNullOrWhiteSpace(model.Name) || model.Quantity < 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data provided.");
            }
            try
            {
                var product = _getProductService.GetProductById(id);
                if (product == null)
                return DoesNotExist();
                _updateProductService.Update(product, model.Name, model.Description, model.Price, model.Quantity);
                return Found(new ProductData(product));
            }
            catch (ArgumentNullException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while updating the Product", ex);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public HttpResponseMessage Delete(Guid id)
        {
            var product = _getProductService.GetProductById(id);
            if (product == null)
                return DoesNotExist();

            _deleteProductService.Delete(product);
            return Found($"Product with {id} is deleted successfully");
        }

        [HttpGet]
        [Route("{id:guid}")]
        public HttpResponseMessage GetProductById(Guid id)
        {
            var product = _getProductService.GetProductById(id);
            if (product == null)
                return DoesNotExist();
            return Found(new ProductData(product));
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetProducts([FromUri] string name = null, [FromUri] string description = null, [FromUri] decimal? price = null, [FromUri] int? quantity = null)
        {
            var products = _getProductService.GetProducts(name, description, price, quantity);
            if (products == null || !products.Any())
                return DoesNotExist();
            return Found(products.Select(p => new ProductData(p)));
        }

    }
}