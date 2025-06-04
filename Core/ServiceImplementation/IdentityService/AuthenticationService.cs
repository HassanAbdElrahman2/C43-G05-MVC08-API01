using AutoMapper;
using DomainLayer.Exceptions.IdentityExceptions;
using DomainLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration configuration,IMapper _mapper ) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            return User is not null;
        }
        public async Task<UserDto> GetCurrentUserAsync(string Email)
        {
            var User =await  _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserDto() { DisplayName = User.DispalayName, Email = User.Email, Token = await CreatTokenAsync(User) };
        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {
            var User= await _userManager.Users
                .Include(x => x.Address).FirstOrDefaultAsync(U=>U.Email==Email)??throw new UserNotFoundException(Email);
            
                return _mapper.Map<Address, AddressDto>(User.Address);
           
        }
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string Email, AddressDto addressDto)
        {
            var User = await _userManager.Users
                 .Include(x => x.Address).FirstOrDefaultAsync(U => U.Email == Email) ?? throw new UserNotFoundException(Email);
            if (User.Address is not null)
            {
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
                User.Address.Street = addressDto.Street;
            }
            else
            {
                User.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            await _userManager.UpdateAsync(User);
            return _mapper.Map<Address, AddressDto>(User.Address);
        }
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
                DispalayName = registerDto.DisplayName,
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
