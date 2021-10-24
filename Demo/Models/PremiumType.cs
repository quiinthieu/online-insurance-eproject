using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class PremiumType
    {
        public PremiumType()
        {
            CustomerPolicies = new HashSet<CustomerPolicy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
