using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Repository;

namespace Backend.Controllers
{
    [Route("api/filterNew")]
    [ApiController]

    public class FilterControllerNew: Controller
    {
        readonly IDeviceFiltersRepository _repository;
        public FilterControllerNew(IDeviceFiltersRepository repository)
        {
            this._repository = repository;
        }
        [HttpPut]

        public JsonResult Put([FromBody] DataModels.FilterDataModel filters)
        {
            IEnumerable<DataModels.DeviceModel> db = this._repository.ApplyAllFiltersNew(filters);
            foreach(var d in db)
            {
                Console.WriteLine(d.batterycapacity);
            }
           
            return Json(db);
        }
    }
}
