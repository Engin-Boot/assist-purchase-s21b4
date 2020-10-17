using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : ControllerBase
    {

        readonly Repository.IDeviceRepository _repository;
        public DeviceController(Repository.IDeviceRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet]
        public IEnumerable<DataModels.DeviceModel> Get()
        {
            return this._repository.GetAllDevices();

        }
        [HttpGet("{Id}")]
        public DataModels.DeviceModel Get(string id)
        {
            return _repository.GetDevice(id);
        }
        [HttpPost]
        public bool Post([FromBody] DataModels.DeviceModel device)
        {
            return _repository.AddNewDevice(device);
        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, [FromBody] DataModels.DeviceModel updatedState)
        {
            if (id != null&& updatedState!=null)
            {
                _repository.UpdateDevice(id, updatedState);
                return Ok();
            }
            return NotFound();
            
        }

        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
           return  _repository.DeleteDevice(id);
        }
    }
}
