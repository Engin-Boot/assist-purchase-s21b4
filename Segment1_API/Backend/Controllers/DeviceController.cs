﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Backend.Controllers
{
    [Route("api/devices")]
    [ApiController]
    public class DeviceController : Controller
    {

        readonly Repository.IDeviceRepository _repository;
        public DeviceController(Repository.IDeviceRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<DataModels.DeviceModel> db = this._repository.GetAllDevices();
            return Json(db);
            

        }
        [HttpGet("{Id}")]
        public ActionResult Get(string id)
        {
            return Json(_repository.GetDevice(id));
        }
        [HttpPost]
        public bool Post([FromBody] DataModels.DeviceModel device)
        {
            return _repository.AddNewDevice(device);
        }

        [HttpPut("{id}")]
        public bool Update(string id, [FromBody] DataModels.DeviceModel updatedState)
        {
            if (!string.IsNullOrEmpty(id) && updatedState!=null)
            {
                return _repository.UpdateDevice(id, updatedState);
                
            }
            return false;
            
        }

        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
           return  _repository.DeleteDevice(id);
        }
    }
}
