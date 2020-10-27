using DataModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/alerter")]
    [ApiController]
    public class MailAlertController : ControllerBase
    {
        private readonly Repository.IMailAlerterRepository _mailAlerterRespository;

        public MailAlertController(Repository.IMailAlerterRepository customerRepository)
        {
            _mailAlerterRespository = customerRepository;
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
