using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataModels;

namespace Backend.Controllers
{
    [Route("api/alerter")]
    [ApiController]
    public class MailAlertController : Controller
    {
        readonly Repository.IMailAlerterRepository _mailAlerterRespository;

        public MailAlertController(Repository.IMailAlerterRepository customerRepository)
        {
            _mailAlerterRespository = customerRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<DataModels.CustomerModel> db = this._mailAlerterRespository.GetAllCustomers();
            return Json(db);
        }

        // POST api/<CustomerAlertController>
        [HttpPost("alert")]
        public bool PostAlert([FromBody] CustomerModel customer)
        {
            return _mailAlerterRespository.SendEmail(customer);
        }
        [HttpPost("authenticate")]
        public bool CustomerAuthentication([FromBody] CustomerModel customer)
        {
            return _mailAlerterRespository.AddCustomer(customer);
        }
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _mailAlerterRespository.DeleteCustomerDetails(id);
        }
    }
}
