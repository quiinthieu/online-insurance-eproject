using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Credential
    {
        public Credential()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }
        public int? RoleId { get; set; }
        public string ActivationCode { get; set; }
        public int? BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
