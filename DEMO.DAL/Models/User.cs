using System;
using System.Collections.Generic;

#nullable disable

namespace DEMO.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Car = new HashSet<Car>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
