using System;
using System.Collections.Generic;

#nullable disable

namespace DEMO.DAL.Models
{
    public partial class Car
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Model { get; set; }
        public short Year { get; set; }
        public string Color { get; set; }

        public virtual User Owner { get; set; }
    }
}
