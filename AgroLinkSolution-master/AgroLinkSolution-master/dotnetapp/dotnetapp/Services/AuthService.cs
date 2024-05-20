using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnetapp.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            //var user1 = await context.Find(model.Email);

            var users = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            Console.WriteLine(string.Join(", ", user));

            if (user == null)
                return (0, "Invalid email");
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return (0, "Invalid password");

            var userRoles = await _userManager.GetRolesAsync(user);
            Console.WriteLine(string.Join(", ", userRoles));


            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(ClaimTypes.NameIdentifier, users.UserId.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return (1, token);
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            Console.WriteLine(claims);
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            Console.WriteLine(tokenDescriptor);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<(int, string)> Registration(User model, string role)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                return (0, "User already Exists");
            }   
            else
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                var createUserResult = await _userManager.CreateAsync(user, model.Password);

                if (!createUserResult.Succeeded)
                    return (0, "User creation failed! Please check user details and try again.");

                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
                if (!await _roleManager.RoleExistsAsync(UserRoles.Farmer))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Farmer));

                if (await _roleManager.RoleExistsAsync(UserRoles.Farmer))
                    await _userManager.AddToRoleAsync(user, role);

                var user1 = new User
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Email = model.Email,
                    MobileNumber = model.MobileNumber,
                    Role = model.Role,
                    ProfileImage = model.ProfileImage,
                };
                _context.Users.Add(user1);
                await _context.SaveChangesAsync();

                return (1, "User created successfully!");
            }

        }


        public async Task<IEnumerable<User>> GetAllFarmers()
        {
            Console.WriteLine(UserRoles.Farmer);
            var formers = _context.Users.Where(x=>x.Role==UserRoles.Farmer).ToListAsync();
            return await formers;
        }
    }
}



