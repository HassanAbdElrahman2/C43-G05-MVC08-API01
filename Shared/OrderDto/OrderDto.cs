using Shared.IdentityDto;

namespace Shared.OrderDto
{
    public class OrderDto
    {
        public string BasketId { get; set; } = default!;
        public int DevliveryMethodId { get; set; }
        public AddressDto Address { get; set; } = default!;
    }
}
