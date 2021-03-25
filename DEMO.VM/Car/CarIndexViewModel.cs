using System;
namespace DEMO.VM.Car
{
    public class CarIndexViewModel
    {
        public Guid Id { get; set; }
        public string OwnerName { get; set; }
        public string Model { get; set; }
        public short Year { get; set; }
        public string Color { get; set; }

        public CarIndexViewModel()
        {
        }
    }
}
