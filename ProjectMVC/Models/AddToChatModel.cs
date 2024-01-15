using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class AddToChatModel
    {
        public List<User> Friends { get; set; }
        public Chat Chat { get; set; }
    }
}
