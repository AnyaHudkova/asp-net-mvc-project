using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Repositories
{
    public class FriendsRepository
    {
        public AppDbContext context { get; set; }
        private DbSet<Friends> table = null;
        private UserRepository userRepository;

        public FriendsRepository(AppDbContext context)
        {
            this.context = context;
            table = context.Set<Friends>();
            userRepository = new UserRepository(context);
        }
        public void Add(Friends friendsModel)
        {
            table.Add(friendsModel);
            context.SaveChanges();
        }

        public void AddFriend(int friendId)
        {
            Friends friendsModel = new Friends();
            friendsModel.User1 = CurrentUser.UserId;
            friendsModel.User2 = friendId;
            table.Add(friendsModel);
            context.SaveChanges();
        }

        public void DeleteFriend(int friendId)
        {
            int idDeleted = FindFriendship(CurrentUser.UserId, friendId).Id;
            table.Remove(table.Find(idDeleted));
            context.SaveChanges();
        }

        public Friends FindFriendship(int user1, int user2)
        {
            foreach (Friends friends in table)
            {
                if (friends.User1 == user1 && friends.User2 == user2)
                    return friends;
            }
            return null;
        }

        public List<Friends> GetAllFriendships()
        {
            List<Friends> friendshipList = new List<Friends>();
            foreach (Friends friends in table)
            {
                friendshipList.Add(friends);
            }
            return friendshipList;
        }

        public List<User> GetUserFriends(int userId)
        {
            List<Friends> friendshipList = GetAllFriendships();
            List<User> userFriends = new List<User>();
            foreach (Friends friendship in friendshipList)
            {
                if (friendship.User1 == userId) userFriends.Add(userRepository.GetUserById(friendship.User2));
            }
            return userFriends;
        }
        public bool AreFriends(int user1, int user2)
        {
            List<Friends> friendshipList = GetAllFriendships();
            foreach (Friends friendship in friendshipList)
            {
                if (friendship.User1 == user1 && friendship.User2 == user2)
                    return true;
            }
            return false;
        }
        public List<User> GetUserNotFriends(int userId)
        {
            List<User> allUsers = userRepository.GetAllUsers();
            List<User> userNotFriends = new List<User>();
            foreach (User user in allUsers)
            {
                if (user.Id!=userId)
                {
                    if(!AreFriends(userId, user.Id))
                    {
                        userNotFriends.Add(user);
                    }
                }
            }
            return userNotFriends;
        }

    }
}
