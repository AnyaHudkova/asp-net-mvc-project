using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
