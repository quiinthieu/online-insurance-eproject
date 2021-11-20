using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class CustomerPolicy
    {
        public CustomerPolicy()
        {
            Claims = new HashSet<Claim>();
            PremiumTransactions = new HashSet<PremiumTransaction>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? PolicyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PremiumTypeId { get; set; }
        public decimal? PremiumAmount { get; set; }
        public int? AgentId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Policy Policy { get; set; }
        public virtual PremiumType PremiumType { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<PremiumTransaction> PremiumTransactions { get; set; }
    }
}
