using System;
using System.Collections.Generic;
using DEMO.DAL.DAL;
using DEMO.DAL.Models;
using DEMO.VM;
using DEMO.VM.User;

namespace DEMO.BL
{
    public class UsersBL
    {
        private readonly UsersDAL UsersDAL = new UsersDAL();

        public UsersBL()
        {
        }

        #region GET

        public IEnumerable<UserIndexViewModel> Index() => UsersDAL.Index();

        public IEnumerable<SelectItem> Select() => UsersDAL.Select();

        public UserViewModel Find(Guid id)
        {
            var model = UsersDAL.Find(id);
            return new UserViewModel()
            {
                Id = model.Id,
                Birthday = model.Birthday,
                Email = model.Email,
                Name = model.Name
            };
        }

        public UserDetailsViewModel Details(Guid id) => UsersDAL.Details(id);

        #endregion

        #region POST

        public bool Post(UserCreateViewModel model)
        {
            return UsersDAL.Post(new User()
            {
                Name = model.Name,
                Email = model.Email,
                Birthday = model.Birthday
            });
        }

        public bool Put(UserEditViewModel model)
        {
            return UsersDAL.Put(new User()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Birthday = model.Birthday
            });
        }

        public bool DeleteConfirmed(Guid id) => UsersDAL.DeleteConfirmed(id);

        #endregion
    }
}
