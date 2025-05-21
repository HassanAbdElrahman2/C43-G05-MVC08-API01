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
    public class OrdersController(IServiceManager _serviceManager):ApiBaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto)
            => Ok(await _serviceManager.OrderService.CreateOrder(orderDto, GetEmailFromToken()));

        
    }
}
