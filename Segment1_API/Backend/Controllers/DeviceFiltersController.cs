using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/filter")]
    [ApiController]
    public class DeviceFiltersController : Controller
    {
        readonly IDeviceFiltersRepository _deviceFilterRepository;
        

        public DeviceFiltersController(IDeviceFiltersRepository repository)
        {
            this._deviceFilterRepository = repository;
            
        }
        

        [HttpGet]
        [Route("measurements")]
        public ActionResult Get()
        {
            var m = _deviceFilterRepository.getUniqueMeasurements();
            //var m = Repository.MeasurementFilterOptions.getUniqueMeasurements();
            return Json(m);
        }
        

        [HttpPut]
        public ActionResult Put([FromBody] DataModels.FilterDataModel filters)
        {
            IEnumerable<DataModels.DeviceModel> db = this._deviceFilterRepository.ApplyAllFilters(filters);         
            return Json(db);
        }

        
    }
}
