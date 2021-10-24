using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class InsuranceType
    {
        public InsuranceType()
        {
            Policies = new HashSet<Policy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Policy> Policies { get; set; }
    }
}
