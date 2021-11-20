using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Agent
    {
        public Agent()
        {
            CustomerPolicies = new HashSet<CustomerPolicy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public int? BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual ICollection<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
