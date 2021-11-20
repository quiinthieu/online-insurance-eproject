using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Agents = new HashSet<Agent>();
            Credentials = new HashSet<Credential>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Agent> Agents { get; set; }
        public virtual ICollection<Credential> Credentials { get; set; }
    }
}
