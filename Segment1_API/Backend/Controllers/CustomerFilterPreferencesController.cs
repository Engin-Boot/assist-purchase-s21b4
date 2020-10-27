using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/filterpreferences")]
    [ApiController]
    public class CustomerFilterPreferencesController: Controller
    {
        readonly ICustomerFilterPreferencesRepository _repository;


        public CustomerFilterPreferencesController(ICustomerFilterPreferencesRepository repository)
        {
            this._repository = repository;

        }
        [HttpGet]
        [Route("{ip}")]
        public ActionResult Get(string ip)
        {
            DataModels.FilterDataModel db = _repository.getCustomerPreferencesByIP(ip);
            return Json(db);
        }

        //http://localhost:5000/api/filter
        [HttpPost]
        [Route("{ipAddress}")]

        public ActionResult Post(string ipAddress, [FromBody] DataModels.FilterDataModel filters)
        {
            bool s = _repository.saveCustomerPreferencesToFile(ipAddress,filters);
            //bool s = Utility.CustomerPreferenceFilterHandeler.saveToCsv(ipAddress, filters);
            s.ToString();
            return Json(s);
        }
    }
}
