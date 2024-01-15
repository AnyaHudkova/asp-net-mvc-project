using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Models;
using ProjectMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    public class AdminController : Controller
    {

        private AppDbContext db;
        private UserRepository repository;

        public AdminController(AppDbContext context)
        {
            db = context;
            repository = new UserRepository(context);
        }
        public IActionResult AdminPage()
        {
            List<User> users = repository.GetAllUsers();
            return View(users);
        }

        public RedirectToRouteResult UnblockUser(User user)
        {
            repository.UnblockUser(user.Id);
            return RedirectToRoute(new { controller = "Admin", action = "AdminPage" });
        }
        public RedirectToRouteResult BlockUser(User user)
        {
            repository.BlockUser(user.Id);
            return RedirectToRoute(new { controller = "Admin", action = "AdminPage" });
        }
    }
}
