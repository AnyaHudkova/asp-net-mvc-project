using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class AccountViewModel
    {
        public User AccountUser { get; set; }
        public List<Post> Posts { get; set; }
        public string ProfileImage { get; set; }
        public bool AreFriends { get; set; }

    }
}
