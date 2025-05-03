using DomainLayer.Exceptions.IdentityExceptions;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.IdentityDto;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceImplementation.IdentityService
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration configuration ) : IAuthenticationService
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
                    Token= await CreatTokenAsync(User)
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
                return new UserDto() { DisplayName = User.DispalayName, Email = User.Email, Token = await CreatTokenAsync(User) };
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);
            }

        }

        private  async Task<string> CreatTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Name,user.UserName!),
                new Claim(ClaimTypes.NameIdentifier,user.Id!)
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecretKey = configuration.GetSection("JWTOptions")["SecurityKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var crads=new SigningCredentials(Key,SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken
                (
                issuer: configuration.GetSection("JWTOptions")["Issuer"],
                audience: configuration.GetSection("JWTOptions")["Audience"],
                claims:Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: crads
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
