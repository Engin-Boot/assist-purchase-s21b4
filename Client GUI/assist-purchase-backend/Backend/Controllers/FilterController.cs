using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [Route("api/filter")]
    [ApiController]
    public class FilterController : Controller
    {

        readonly Repository.IDeviceRepository _repository;
        public FilterController(Repository.IDeviceRepository repository)
        {
            this._repository = repository;
        }
        
        [HttpGet]
        [Route("measurements")]
        public JsonResult Get()
        {
            var m = Repository.FilterOptions.getUniqueMeasurements();
            
            return Json(m);
        }
       
    }
}
