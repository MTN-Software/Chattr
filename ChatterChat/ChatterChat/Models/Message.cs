using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatterChat.Models
{
    public class Message
    {
        public int ID { get; set; }

        [Required]
        public string Content { get; set; }

        // Foreign key
        public int UserID { get; set; }

        // Navigation Property
        public virtual User User { get; set; }
    }
}