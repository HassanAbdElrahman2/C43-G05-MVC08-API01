using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.BasketModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("/api")]
    public class BaskectController (IServiceManager _serviceManager):ControllerBase
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
