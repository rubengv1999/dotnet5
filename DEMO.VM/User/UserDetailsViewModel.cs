using System;
using System.Collections.Generic;

namespace DEMO.VM.User
{
    public class UserDetailsViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public IEnumerable<Car> Cars { get; set; }

        public UserDetailsViewModel()
        {
        }

        public class Car
        {
            public string Model { get; set; }
            public short Year { get; set; }
            public string Color { get; set; }
        }
    }
}
