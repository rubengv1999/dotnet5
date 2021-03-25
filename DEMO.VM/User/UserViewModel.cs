using System;
namespace DEMO.VM.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }

        public UserViewModel()
        {
        }
    }
}
