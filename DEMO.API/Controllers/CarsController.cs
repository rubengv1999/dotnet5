using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DEMO.VM.Car;
using DEMO.BL;

namespace DEMO.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarsBL CarsBL = new CarsBL();
        private readonly ILogger<CarsController> _logger;

        public CarsController(ILogger<CarsController> logger)
        {
            _logger = logger;
        }

        #region GET

        [HttpGet]
        [Route("Index")]
        public IEnumerable<CarIndexViewModel> Index() => CarsBL.Index();

        [HttpGet]
        [Route("Edit/{id}")]
        public CarViewModel Edit(Guid id) => CarsBL.Find(id);

        [HttpGet]
        [Route("Delete/{id}")]
        public CarViewModel Delete(Guid id) => CarsBL.Find(id);

        [HttpGet]
        [Route("Details/{id}")]
        public CarDetailsViewModel Details(Guid id) => CarsBL.Details(id);

        #endregion

        #region POST

        [HttpPost]
        public bool Post(CarCreateViewModel model) => CarsBL.Post(model);

        [HttpPut("{id}")]
        public bool Put(Guid id, CarEditViewModel model) => id == model.Id && CarsBL.Put(model);

        [HttpDelete("{id}")]
        public bool DeleteConfirmed(Guid id) => CarsBL.DeleteConfirmed(id);

        #endregion
    }
}