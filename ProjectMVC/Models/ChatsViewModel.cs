using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class ChatsViewModel
    {
        public List<Chat> UserChats { get; set; }
        public List<Chat> PublicChats { get; set; }
        public string Query { get; set; }
    }
}
