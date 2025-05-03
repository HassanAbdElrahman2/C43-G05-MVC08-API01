using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.IdentityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager):ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
            => Ok(await _serviceManager.AuthenticationService.LoginAsync(loginDto));
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        => Ok(await _serviceManager.AuthenticationService.RegisterAsync(registerDto));
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
            => Ok(await _serviceManager.AuthenticationService.CheckEmailAsync(Email));
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.AuthenticationService.CheckEmailAsync(email!));
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!));
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(email,addressDto));
        }





    }
}
