using System;
using System.Collections.Generic;
using DEMO.DAL.DAL;
using DEMO.DAL.Models;
using DEMO.VM.Car;

namespace DEMO.BL
{
    public class CarsBL
    {
        private readonly CarsDAL CarsDAL = new CarsDAL();

        public CarsBL()
        {
        }

        #region GET

        public IEnumerable<CarIndexViewModel> Index() => CarsDAL.Index();

        public CarViewModel Find(Guid id)
        {
            var model = CarsDAL.Find(id);
            return new CarViewModel()
            {
                Id = model.Id,
                Color = model.Color,
                Model = model.Model,
                Year = model.Year,
                OwnerId = model.OwnerId
            };
        }

        public CarDetailsViewModel Details(Guid id) => CarsDAL.Details(id);

        #endregion

        #region POST

        public bool Post(CarCreateViewModel model)
        {
            return CarsDAL.Post(new Car()
            {
                OwnerId = model.OwnerId,
                Model = model.Model,
                Year = model.Year,
                Color = model.Color
            });
        }

        public bool Put(CarEditViewModel model)
        {
            return CarsDAL.Put(new Car()
            {
                Id = model.Id,
                OwnerId = model.OwnerId,
                Model = model.Model,
                Year = model.Year,
                Color = model.Color
            });
        }

        public bool DeleteConfirmed(Guid id) => CarsDAL.DeleteConfirmed(id);

        #endregion
    }
}
