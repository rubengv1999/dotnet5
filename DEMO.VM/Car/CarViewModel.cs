using System;
namespace DEMO.VM.Car
{
    public class CarViewModel
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Model { get; set; }
        public short Year { get; set; }
        public string Color { get; set; }

        public CarViewModel()
        {
        }
    }
}
