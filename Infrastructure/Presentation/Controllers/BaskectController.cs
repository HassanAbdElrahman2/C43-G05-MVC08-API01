using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.BasketModelDto;
namespace Presentation.Controllers
{
   
    public class BaskectController (IServiceManager _serviceManager):ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string Key) => Ok(await _serviceManager.BasketService.GetBasketAsync(Key));
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)=> Ok(await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket));
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>>DeleteBasket(string Key)
        {
            var Result = await _serviceManager.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }

    }
}
