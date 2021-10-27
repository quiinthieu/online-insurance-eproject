using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class PremiumTransaction
    {
        public int Id { get; set; }
        public int? CustomerPolicyId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaidDate { get; set; }
        public DateTime? DueDate { get; set; }

        public virtual CustomerPolicy CustomerPolicy { get; set; }
    }
}
