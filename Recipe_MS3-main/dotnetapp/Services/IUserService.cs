// using dotnetapp.Models;

// namespace dotnetapp.Services
// {
//     public interface UserService
//     {
//         User register(User user);
//         User login(User user);
//         string GenerateJwtToken(User user);
//     }
// }


using dotnetapp.Models;
using System.Threading.Tasks;
using dotnetapp.Data;

namespace dotnetapp.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(User user);
        Task<object> LoginAsync(string email, string password);
    }
}
