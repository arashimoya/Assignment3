using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FileData;
using Microsoft.EntityFrameworkCore;
using Models;

namespace WebAPI.Data
{
    public class UserServiceImpl : IUserService
    {
        private IList<User> users;
        private AssDbContext assDbContext;

        public UserServiceImpl()
        {
            assDbContext = new AssDbContext();
            users = assDbContext.Users.ToList();
            if (!users.Any())
            {
                Seed();
                WriteUsersToDb();
            }
        }


        public async Task<User> ValidateUser(string username, string password)
        {
            User userToValidate = assDbContext.Users.FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
            if (userToValidate != null)
            {
                return userToValidate;
            }

            throw new Exception("Wrong username or password");
        }

        public async Task RegisterUser(string username, string password)
        {
            User userToRegister = new User();
            userToRegister.Password = password;
            userToRegister.Username = username;
            assDbContext.Users.Add(userToRegister);
            await assDbContext.SaveChangesAsync();
        }

        public async Task<bool> DoesUsernameAlreadyExist(string username)
        {
            User first = await assDbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));
            if (first != null)
            {
                throw new Exception("User already exists!");
            }

            return false;
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

        private void WriteUsersToDb()
        {
            foreach (var user in users)
            {
                assDbContext.Users.Add(user);
            }

            assDbContext.SaveChanges();
        }
    }
}