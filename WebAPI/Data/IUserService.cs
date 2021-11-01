using Models;

namespace WebAPI.Data
{
    public interface IUserService
    {
        User ValidateUser(string Username, string Password);
        void RegisterUser(string username, string password);
        public bool DoesUsernameAlreadyExist(string username);
    }
}