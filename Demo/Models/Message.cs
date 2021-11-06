using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Message1 { get; set; }
        public bool? Status { get; set; }
    }
}
