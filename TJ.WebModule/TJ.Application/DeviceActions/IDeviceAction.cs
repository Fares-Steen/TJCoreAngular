using System.Collections.Generic;
using System.Threading.Tasks;
using TJ.Models;

namespace TJ.Application.DeviceActions
{
    public interface IDeviceAction
    {
        public Task<DeviceModel> Create(DeviceModel deviceModel);
        public Task<List<DeviceModel>> Get();
    }
}