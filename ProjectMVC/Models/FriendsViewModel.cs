using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class FriendsViewModel
    {
        public List<User> UserFriends { get; set; }
        public List<User> UserNotFriends { get; set; }
    }
}
