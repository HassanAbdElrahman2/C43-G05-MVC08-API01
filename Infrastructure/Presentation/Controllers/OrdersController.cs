using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager):ApiBaseController
    {
       
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto)
            => Ok(await _serviceManager.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken()));
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetAllDeliveryMethods()
             => Ok(await _serviceManager.OrderService.GetDeliveryMethodsAsync());


       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
            => Ok(await _serviceManager.OrderService.GetAllOrdersAsync(GetEmailFromToken()));

       
        [HttpGet("{Id:guid}")]
        public async Task <ActionResult<OrderToReturnDto>> GetOrderById(Guid Id)
            => Ok(await _serviceManager.OrderService.GetOrderByIdAsync(Id));

    }
}
