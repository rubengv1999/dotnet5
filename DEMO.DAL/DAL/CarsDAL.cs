using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.DAL.Models;
using DEMO.VM.Car;
using Microsoft.EntityFrameworkCore;

namespace DEMO.DAL.DAL
{
    public class CarsDAL
    {
        public CarsDAL()
        {
        }

        #region GET

        public IEnumerable<CarIndexViewModel> Index()
        {
            try
            {
                using var db = new DEMOContext();

                var v1 = db.Car.Include(x => x.Owner);

                var sqlv1 = v1.ToQueryString();

                var listv1 = v1.ToList();

                var v2 = db.Car.Select(model => new CarIndexViewModel()
                {
                    Id = model.Id,
                    OwnerName = model.Owner.Name,
                    Model = model.Model,
                    Year = model.Year,
                    Color = model.Color
                });

                var sqlv2 = v2.ToQueryString();

                var listv2 = v2.ToList();

                return v2;
            }
            catch
            {
                throw;
            }
        }

        public Car Find(Guid id)
        {
            try
            {
                using var db = new DEMOContext();
                return db.Car.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public CarDetailsViewModel Details(Guid id)
        {
            try
            {
                using var db = new DEMOContext();
                return db.Car.Where(x => x.Id == id).Select(model => new CarDetailsViewModel()
                {
                    OwnerName = model.Owner.Name,
                    Model = model.Model,
                    Year = model.Year,
                    Color = model.Color
                }).SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region POST

        public bool Post(Car model)
        {
            try
            {
                using var db = new DEMOContext();
                db.Car.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Put(Car model)
        {
            try
            {
                using var db = new DEMOContext();
                db.Car.Update(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteConfirmed(Guid id)
        {
            try
            {
                using var db = new DEMOContext();
                var model = db.Car.Find(id);
                if (model == null) return false;
                db.Car.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
