using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.DAL.Models;
using DEMO.VM;
using DEMO.VM.User;
using Microsoft.EntityFrameworkCore;

namespace DEMO.DAL.DAL
{
    public class UsersDAL
    {
        public UsersDAL()
        {
        }

        #region GET

        public IEnumerable<UserIndexViewModel> Index()
        {
            try
            {
                using var db = new DEMOContext();

                var v1 = db.User.Include(x => x.Car);

                var sqlv1 = v1.ToQueryString();

                var listv1 = v1.ToList();

                var v2 = db.User.Select(model => new UserIndexViewModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    Cars = model.Car.Count
                });

                var sqlv2 = v2.ToQueryString();

                var listv2 = v2.ToList();

                return listv2;
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<SelectItem> Select()
        {
            try
            {
                using var db = new DEMOContext();
                return db.User.Select(model => new SelectItem()
                {
                    Value = model.Id.ToString(),
                    Text = model.Name
                }).ToList();
            }
            catch
            {
                throw;
            }
        }

        public User Find(Guid id)
        {
            try
            {
                using var db = new DEMOContext();
                return db.User.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public UserDetailsViewModel Details(Guid id)
        {
            try
            {
                using var db = new DEMOContext();

                var v1 = db.User.Include(x => x.Car);

                var sqlv1 = v1.ToQueryString();

                var itemv1 = v1.FirstOrDefault(x => x.Id == id);

                var v2 = db.User.Where(x => x.Id == id).Select(model => new UserDetailsViewModel()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Birthday = model.Birthday,
                    Cars = model.Car.Select(x => new UserDetailsViewModel.Car()
                    {
                        Model = x.Model,
                        Year = x.Year,
                        Color = x.Color
                    })
                });

                var sqlv2 = v2.ToQueryString();

                var itemv2 = v2.SingleOrDefault();

                return itemv2;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region POST

        public bool Post(User model)
        {
            try
            {
                using var db = new DEMOContext();
                db.User.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Put(User model)
        {
            try
            {
                using var db = new DEMOContext();
                db.User.Update(model);
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
                var model = db.User.Find(id);
                if (model == null) return false;
                db.User.Remove(model);
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
