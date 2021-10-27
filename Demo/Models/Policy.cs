using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Policy
    {
        public Policy()
        {
            CustomerPolicies = new HashSet<CustomerPolicy>();
        }

        public int Id { get; set; }
        public int? InsuranceTypeId { get; set; }
        public string Name { get; set; }
        public int? Term { get; set; }
        public decimal? Amount { get; set; }
        public double? InterestRate { get; set; }
        public string Description { get; set; }

        public virtual InsuranceType InsuranceType { get; set; }
        public virtual ICollection<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
