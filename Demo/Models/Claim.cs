using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Claim
    {
        public int Id { get; set; }
        public int? CustomerPolicyId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ClaimedDate { get; set; }

        public virtual CustomerPolicy CustomerPolicy { get; set; }
    }
}
