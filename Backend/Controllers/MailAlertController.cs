using System;
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
        public IActionResult PostAlert([FromBody] CustomerModel customer)
        {
            try
            {
                _mailAlerterRespository.SendEmail(customer);
                return Ok("mail sent");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("authenticate")]
        public IActionResult CustomerAuthentication([FromBody] CustomerModel customer)
        {

            var customerDetails = _mailAlerterRespository.FindCustomer(customer.CustomerId);
            if (customerDetails == null)
            {
                _mailAlerterRespository.AddCustomer(customer);
                return Ok("New Customer");
            }

            customerDetails.DeviceId = customer.DeviceId;
            _mailAlerterRespository.AddCustomer(customer);
            return Ok("Existing Customer");
        }
    }
}
