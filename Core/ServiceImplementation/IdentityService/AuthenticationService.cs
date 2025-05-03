using DomainLayer.Exceptions.IdentityExceptions;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.IdentityDto;
using System.Data;

namespace ServiceImplementation.IdentityService
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
           // Check If Email Exists 
           var User=await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);

            // Check Password
            var IsPasswordVaild =await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (IsPasswordVaild)
                return new UserDto()
                {
                    DisplayName = User.DispalayName,
                    Email = User.Email,
                    Token=CreatTokenAsync(User)
                };

            else
                throw new UnauthorizedException();

        }


        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var User = new ApplicationUser()
            {
                DispalayName = registerDto.Email,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName
            };
            var Result = await _userManager.CreateAsync(User, registerDto.Password);
            if (Result.Succeeded)
                return new UserDto() { DisplayName = User.DispalayName, Email = User.Email, Token = CreatTokenAsync(User) };
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }

        private static string CreatTokenAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
