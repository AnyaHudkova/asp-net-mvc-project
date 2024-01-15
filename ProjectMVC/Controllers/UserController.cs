using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Models;
using ProjectMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    public class UserController : Controller
    {
        private AppDbContext db;
        private UserRepository repository;

        public UserController(AppDbContext context)
        {
            db = context;
            repository = new UserRepository(context);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if(user.Email == Admin.Login && user.Password == Admin.Password)
            {
                return RedirectToAction("AdminPage", "Admin");
            }
            if (user.Email != null && user.Password != null)
            {
                if (!repository.FindUser(user))
                    ModelState.AddModelError("", "Oops, incorrect email or password...");
                else
                {
                    if(repository.GetUserByLoginInfo(user.Email, user.Password).Blocked)
                    {
                        ModelState.AddModelError("", "Sorry, your account is blocked.");
                    }
                    else
                    {
                        user = repository.GetUserByLoginInfo(user.Email, user.Password);
                        CurrentUser.UserId = user.Id;
                        return RedirectToAction("AccountPage", "Account");
                    }
                }  
            }
            else ModelState.AddModelError("", "All fields must be filled");
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registration(User user)
        {
            if (user.Email != null && user.Password != null && user.Username != null && user.Age != 0)
            {
                if (repository.EmailExists(user))
                    ModelState.AddModelError("", "Oops, this email has already been used...");
                else
                {
                    repository.Add(user);
                    user = repository.GetUserByLoginInfo(user.Email, user.Password);
                    CurrentUser.UserId = user.Id;
                    return RedirectToAction("AccountPage", "Account");
                }

            }  
            else ModelState.AddModelError("", "All fields must be filled");
            return View();
        }
    }
}
