using System;
using System.ComponentModel.DataAnnotations;

namespace DEMO.VM.Car
{
    public class CarEditViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Owner")]
        public Guid OwnerId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "This field cannot exceed 100 characters")]
        public string Model { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public short Year { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [StringLength(100, ErrorMessage = "This field cannot exceed 100 characters")]
        public string Color { get; set; }

        public CarEditViewModel()
        {
        }
    }
}
