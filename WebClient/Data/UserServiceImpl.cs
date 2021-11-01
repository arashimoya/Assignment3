using System;
using System.Collections.Generic;
using System.Linq;
using FileData;
using Models;


namespace Assignment1.Data
{
    public class UserServiceImpl : IUserService
    {
        private FileContext FileContext;
        private List<User> users;

        public UserServiceImpl()
        {
            FileContext = new FileContext();
        }
        
        public User ValidateUser(string Username, string Password)
        {
            User first = FileContext.Users.FirstOrDefault(user => user.Username.Equals(Username));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(Password))
            {
                throw new Exception("Incorrect password");
            }

            return first;
        }
        
        //adding new user
        public void RegisterUser(string username, string password)
        {
            User newUser = new User();
            newUser.Username = username;
            newUser.Password = password;
            FileContext.Users.Add(newUser);
            FileContext.SaveChanges();
        }

        public bool DoesUsernameAlreadyExist(string username)
        {
            User newUser = new User();
            newUser.Username = username;
            bool tmp = false;
            foreach (var user in FileContext.Users)
            {
                if (newUser.Username.Equals(user.Username)) tmp = true;
            }

            return tmp;
        }
    }
}