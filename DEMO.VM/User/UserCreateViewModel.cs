using System;
using System.ComponentModel.DataAnnotations;

namespace DEMO.VM.User
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "This field cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "This field is an e-mail field")]
        [StringLength(100, ErrorMessage = "This field cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public UserCreateViewModel()
        {
        }
    }
}