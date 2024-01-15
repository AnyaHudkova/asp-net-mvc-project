using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean PrivateChat { get; set; }
    }
}
