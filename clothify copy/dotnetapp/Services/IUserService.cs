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
