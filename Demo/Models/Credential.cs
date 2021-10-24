using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Credential
    {
        public Credential()
        {
            Agents = new HashSet<Agent>();
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte? Status { get; set; }
        public int? RoleId { get; set; }
        public string ActivationCode { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
