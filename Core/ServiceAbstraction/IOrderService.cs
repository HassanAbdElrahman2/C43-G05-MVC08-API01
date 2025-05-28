using Shared.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email);
        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id);
    }
}
