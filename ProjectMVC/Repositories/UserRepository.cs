using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Repositories
{
    public class UserRepository
    {
        public AppDbContext context { get; set; }
        private DbSet<User> table = null;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
            table = context.Set<User>();
        }

        public void BlockUser(int userId)
        {
            User user = GetUserById(userId);
            user.Blocked = true;
            table.Update(user);
            context.SaveChanges();
        }
        public void UnblockUser(int userId)
        {
            User user = GetUserById(userId);
            user.Blocked = false;
            table.Update(user);
            context.SaveChanges();
        }
        public void Add(User userModel)
        {
            userModel.Blocked = false;
            table.Add(userModel);
            context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            foreach (User user in table)
            {
                users.Add(user);
            }
            return users;
        }

        public bool FindUser(User userModel)
        {
            foreach (User user in table)
            {
                if (userModel.Email == user.Email && userModel.Password == user.Password)
                {
                    userModel = user;
                    return true;
                }
                    
            }
            return false;
        }

        public User GetUserById(int id)
        {
            foreach (User user in table)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public User GetUserByLoginInfo(string email, string password)
        {
            foreach (User user in table)
            {
                if (email == user.Email && password == user.Password)
                {
                    return user;   
                }
            }
            return null;
        }

        public bool EmailExists(User userModel)
        {
            foreach (User user in table)
                if (userModel.Email == user.Email)return true; 
            return false;
        }
    }
}
