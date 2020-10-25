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

    public class FilterControllerNew : Controller
    {
        readonly IDeviceFiltersRepository _repository;
        public FilterControllerNew(IDeviceFiltersRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet]
        [Route("{ip}")]
        public JsonResult Get(string ip)
        {
            DataModels.FilterDataModel db = Utility.CustomerPreferenceFilterHandeler.GetPreferences(ip);
            //Console.WriteLine(db.ToString());
            //foreach(var v in db)
            //{
            //    Console.WriteLine(v.mrn);
            //}
            return Json(db);


        }
        [HttpPut]
        
        public JsonResult Put([FromBody] DataModels.FilterDataModel filters)
        {
            IEnumerable<DataModels.DeviceModel> db = this._repository.ApplyAllFiltersNew(filters);
            //foreach(var d in db)
            //{
            //    Console.WriteLine(d.batterycapacity);
            //}
           
            return Json(db);
        }

        //http://localhost:5000/api/filterNew
        [HttpPost]
        [Route("{ipAddress}")]
        
        public JsonResult Post(string ipAddress,[FromBody] DataModels.FilterDataModel filters)
        {
            bool s = Utility.CustomerPreferenceFilterHandeler.saveToCsv(ipAddress,filters);
            s.ToString();
            return Json(s);
        }
    }
}
