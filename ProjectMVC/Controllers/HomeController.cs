using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private AppDbContext db;

        public HomeController(AppDbContext context)
        {
            db = context;
        }

        /*public async Task<IActionResult> Index()
        {
            return View(await db.User.ToListAsync());
        }*/

        public IActionResult Index()
        {
            return View();
        }

    }
}
