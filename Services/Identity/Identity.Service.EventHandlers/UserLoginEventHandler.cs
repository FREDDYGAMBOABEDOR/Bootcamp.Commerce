using Identity.Domain;
using Identity.Persistence.Database;
using Identity.Service.EventHandlers.Commands;
using Identity.Service.EventHandlers.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Service.EventHandlers
{
    public class UserLoginEventHandler : IRequestHandler<UserLoginCommand, IdentityAccess>
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IdentityDBContext _context;
        private readonly IConfiguration _configuration;

        public UserLoginEventHandler(SignInManager<ApplicationUser> signInManager, IdentityDBContext context, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task<IdentityAccess> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var result = new IdentityAccess();

            var user = await _context.Users.SingleAsync(x => x.Email == request.Email);

            var response = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if(response.Succeeded)
            {
                result.Succeeded = true;
                await BuildToken(user, result);
            }

            return result;
        }

        private async Task BuildToken(ApplicationUser _user, IdentityAccess _access)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
                new Claim(ClaimTypes.Email, _user.Email),
                new Claim(ClaimTypes.Name, _user.FirstName),
                new Claim(ClaimTypes.Surname, _user.LastName),
                new Claim("TokenCreated", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt", System.Globalization.DateTimeFormatInfo.InvariantInfo))
            };

            var roles = await _context.Roles.Where(x => x.UserRoles.Any(y => y.UserId == _user.Id)).ToListAsync();
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            _access.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
