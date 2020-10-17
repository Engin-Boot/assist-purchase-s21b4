using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/filters")]
    [ApiController]
    public class DeviceFiltersController : ControllerBase
    {
        readonly IDeviceFiltersRepository _repository;
        public DeviceFiltersController(IDeviceFiltersRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet("{filters}")]
        
        public List<DataModels.DeviceModel> Get(string filters)
        {
            try
            {
                List<string> filtersList = filters.Split(',').ToList();
                var results = _repository.ApplyAllFilters(filtersList);
                return results;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
