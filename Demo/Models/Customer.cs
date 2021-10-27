using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerPolicies = new HashSet<CustomerPolicy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Occupation { get; set; }
        public int? CredentialId { get; set; }
        public string CitizenId { get; set; }

        public virtual Credential Credential { get; set; }
        public virtual ICollection<CustomerPolicy> CustomerPolicies { get; set; }
    }
}
