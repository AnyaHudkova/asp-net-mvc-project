using Microsoft.AspNetCore.Mvc;
using ProjectMVC.Models;
using ProjectMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{
    public class FriendsController : Controller
    {
        private AppDbContext db;
        private UserRepository userRepository;
        private PostRepository postRepository;
        private FriendsRepository friendsRepository;
        private AccountViewModel accountModel;
        private FriendsViewModel friendsModel;

        public FriendsController(AppDbContext context)
        {
            db = context;
            userRepository = new UserRepository(context);
            postRepository = new PostRepository(context);
            friendsRepository = new FriendsRepository(context);
            accountModel = new AccountViewModel();
            friendsModel = new FriendsViewModel();
        }

        public IActionResult FriendsPage()
        {
            friendsModel.UserFriends = friendsRepository.GetUserFriends(CurrentUser.UserId);
            friendsModel.UserNotFriends = friendsRepository.GetUserNotFriends(CurrentUser.UserId);
            Random rnd = new Random();
            int n = rnd.Next(1, 13);
            string img = "/Avatars/" + n + ".png";
            return View(friendsModel);
        }

        public RedirectToRouteResult AddFriend(User user)
        {
            friendsRepository.AddFriend(user.Id);
            return RedirectToRoute(new { controller = "Friends", action = "FriendsPage" });
        }

        public RedirectToRouteResult DeleteFriend(User user)
        {
            friendsRepository.DeleteFriend(user.Id);
            return RedirectToRoute(new { controller = "Friends", action = "FriendsPage" });
        }

        public IActionResult FriendAccount(User friend)
        {
            User user = userRepository.GetUserById(friend.Id);
            accountModel.AccountUser = user;
            accountModel.Posts = postRepository.GetPostsByUserId(user.Id);
            accountModel.AreFriends = friendsRepository.AreFriends(CurrentUser.UserId, user.Id);
            Random rnd = new Random();
            int n = rnd.Next(1, 16);
            string img = "/Avatars/" + n + ".png";
            accountModel.ProfileImage = img;
            return View(accountModel);
        }
    }
}
