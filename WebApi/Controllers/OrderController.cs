using Core.Services.Orders;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(ICreateOrderService createOrderService, IDeleteOrderService deleteOrderService, IGetOrderService getOrderService, IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            if (model == null || orderId == Guid.Empty)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data provided.");
            }
            try
            {
                // Check if a order with the same orderId already exists
                var existingOrder = _getOrderService.GetOrderById(orderId);
                if (existingOrder != null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Conflict, $"This Order with the OrderId:{orderId} already exists.");
                }
                var order = _createOrderService.Create(orderId,model.UserOrderId, model.UserName, model.UserEmail, model.OrderDate, model.Products);
                return Found(new OrderData(order));
            }
            catch (InvalidOperationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while creating the Order", ex);
            }
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            if (model == null || orderId == Guid.Empty)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data provided.");
            }
            if (model.Products == null || !model.Products.Any())
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Order must contain at least one product.");
            }
            var order = _getOrderService.GetOrderById(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            try
            {
                _updateOrderService.Update(order, model.UserOrderId, model.UserName, model.UserEmail, model.OrderDate, model.Products, model.TotalPrice);
                return Found(new OrderData(order));
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while updating the Order", ex);
            }
        }

        [Route("{orderId:guid}")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid orderId)
        {
            var order = _getOrderService.GetOrderById(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _deleteOrderService.DeleteOrder(order);
            return Found($"{orderId} is deleted successfully");
        }

        [HttpGet]
        [Route("{orderId:guid}")]
        public HttpResponseMessage GetOrderById(Guid orderId)
        {
            var order = _getOrderService.GetOrderById(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            return Found(new OrderData(order));
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage List() { 
            
            try
            {
                var orders = _getOrderService.GetAllOrders();
                return Found(orders.Select(o => new OrderData(o)));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error occurred while retrieving the Orders", ex);
            }
        }
    }
}