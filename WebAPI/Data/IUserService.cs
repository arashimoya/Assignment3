using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace WebAPI.Data
{
    public interface IUserService
    {
        Task<User> ValidateUser(string username, string password);
        Task RegisterUser(string username, string password);
        Task<bool> DoesUsernameAlreadyExist(string username);
        Task<IList<User>> GetAllAsync();
    }
}