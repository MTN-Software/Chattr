using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatterChat.Models
{
    public class MessageDTO
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
    }
}