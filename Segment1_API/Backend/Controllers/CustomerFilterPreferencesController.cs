using Backend.Repository;
using Microsoft.AspNetCore.Mvc;


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
            DataModels.FilterDataModel db = _repository.GetCustomerPreferencesByIp(ip);
            return Json(db);
        }

        [HttpPost]
        [Route("{ipAddress}")]

        public ActionResult Post(string ipAddress, [FromBody] DataModels.FilterDataModel filters)
        {
            return Json(_repository.SaveCustomerPreferencesToFile(ipAddress,filters));
            
        }
    }
}
