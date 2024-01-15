using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class MessagesView
    {
        public List<MessageViewModel> List { get; set; }
        public Chat Chat { get; set; }
    }
}
