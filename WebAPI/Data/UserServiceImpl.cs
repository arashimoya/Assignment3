using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace WebAPI.Data
{
    public class UserServiceImpl : IUserService
    {
        private IList<User> users;
        private string usersFile = "users.json";

        public UserServiceImpl()
        {
            if (!File.Exists(usersFile))
            {
                Seed();
                WriteUsersToFile();
            }

            string content = File.ReadAllText(usersFile);
            users = JsonSerializer.Deserialize<IList<User>>(content);
        }


        public async Task<User> ValidateUser(string username, string password)
        {
            User user = users.FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
            if (user != null)
            {
                return user;
            }

            throw new Exception("User not found!");
        }

        public Task RegisterUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DoesUsernameAlreadyExist(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<User>> GetAllAsync()
        {
            List<User> tmp = new List<User>(users);
            return tmp;
        }
        private void Seed()
        {
            User[] userArray =
            {
                new User
                {
                    Username = "admin",
                    Password = "monkey123"
                },
            };
            users = userArray.ToList();

        }

        private void WriteUsersToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(usersFile, productsAsJson);
        }
    }
}