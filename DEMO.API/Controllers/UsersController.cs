using System;
using System.Collections.Generic;
using DEMO.BL;
using DEMO.VM;
using DEMO.VM.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DEMO.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UsersBL UsersBL = new UsersBL();

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        #region GET

        [HttpGet]
        [Route("Index")]
        public IEnumerable<UserIndexViewModel> Index() => UsersBL.Index();

        [HttpGet]
        [Route("Select")]
        public IEnumerable<SelectItem> Select() => UsersBL.Select();

        [HttpGet]
        [Route("Edit/{id}")]
        public UserViewModel Edit(Guid id) => UsersBL.Find(id);

        [HttpGet]
        [Route("Details/{id}")]
        public UserDetailsViewModel Details(Guid id) => UsersBL.Details(id);

        [HttpGet]
        [Route("Delete/{id}")]
        public UserViewModel Delete(Guid id) => UsersBL.Find(id);

        #endregion

        #region POST

        [HttpPost]
        public bool Post(UserCreateViewModel model) => UsersBL.Post(model);

        [HttpPut("{id}")]
        public bool Put(Guid id, UserEditViewModel model) => id == model.Id && UsersBL.Put(model);

        [HttpDelete("{id}")]
        public bool DeleteConfirmed(Guid id) => UsersBL.DeleteConfirmed(id);

        #endregion
    }
}
