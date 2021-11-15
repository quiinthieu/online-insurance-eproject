using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class BuyPolicy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Occupation { get; set; }
        public int CredentialId { get; set; }
        public string CitizenId { get; set; }
        public string Action { get; set; }
        public int PremiumTypeId { get; set; }
        public List<int> PolicyId { get; set; }
    }
}
