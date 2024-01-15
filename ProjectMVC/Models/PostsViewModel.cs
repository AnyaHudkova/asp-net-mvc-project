using ProjectMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class PostsViewModel
    {
        public Post Post { get; set; }
        public User User { get; set; }

    }
}
