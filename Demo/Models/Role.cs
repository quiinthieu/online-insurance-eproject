using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Role
    {
        public Role()
        {
            Credentials = new HashSet<Credential>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Credential> Credentials { get; set; }
    }
}
