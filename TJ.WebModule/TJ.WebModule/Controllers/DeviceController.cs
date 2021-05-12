using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TJ.Application.DeviceActions;
using TJ.Models;

namespace TJ.WebModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceAction _deviceAction;
        public DeviceController(IDeviceAction deviceAction)
        {
            _deviceAction = deviceAction;
        }

        [HttpPost("create")]
        public async Task<DeviceModel> Create(DeviceModel deviceModel)
        {
            var createdDevice = await _deviceAction.Create(deviceModel);

            return createdDevice;
        }

        [HttpGet("get")]
        public async Task<List<DeviceModel>> Get() 
        {
            var devicesList = await _deviceAction.Get();

            return devicesList;
        }
    }
}
