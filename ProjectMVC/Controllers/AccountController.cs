using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Models;
using ProjectMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext db;
        private UserRepository userRepository;
        private PostRepository postRepository;
        private AccountViewModel accountModel;
        private PostsViewModel postModel;

        public AccountController(AppDbContext context)
        {
            db = context;
            userRepository = new UserRepository(context);
            postRepository = new PostRepository(context);
            accountModel = new AccountViewModel();
            postModel = new PostsViewModel();
        }
        public IActionResult AccountPage()
        {
            User user = userRepository.GetUserById(CurrentUser.UserId);
            accountModel.AccountUser = user;
            accountModel.Posts = postRepository.GetPostsByUserId(user.Id);
            Random rnd = new Random();
            int n = rnd.Next(1, 13);
            string img = "/Avatars/" + n + ".png";
            accountModel.ProfileImage = img;
            return View(accountModel);
        }

        public IActionResult Posts()
        {
            List<PostsViewModel> posts = postRepository.GetPostsAndUsers();
            return View(posts);
        }

        [HttpPost]
        public IActionResult AddPost()
        {

            if(Request.Form["postTheme"] != "")
            {
                if(Request.Form["postText"] != "")
                {
                    Post post = new Post();
                    post.Theme = Request.Form["postTheme"];
                    post.Content = Request.Form["postText"];
                    post.User_id = CurrentUser.UserId;
                    postRepository.Add(post);
                }
            }
            return RedirectToAction("AccountPage", "Account");
        }

        public RedirectToRouteResult DeletePost(Post post)
        {
            if (post.Id != 0) postRepository.DeletePost(post.Id);

            return RedirectToRoute(new { controller = "Account", action = "AccountPage" });
        }
        public IActionResult LogOut()
        {
            CurrentUser.UserId = 0;
            return RedirectToAction("Index", "Home");
        }

    }
}
