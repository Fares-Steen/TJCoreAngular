using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TJ.Domain.Entities;
using TJ.Interfaces.DbInterfaces;
using TJ.Models;

namespace TJ.Application.DeviceActions
{
    public class DeviceAction: IDeviceAction
    {
        private readonly IGenericRepository<Device> _genericRepository;
        private readonly IMapper mapper;

        public DeviceAction(IGenericRepository<Device> genericRepository)
        {
            _genericRepository = genericRepository;

           var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Device, DeviceModel>();
                cfg.CreateMap<DeviceModel, Device>();
            });

            mapper = mapperConfiguration.CreateMapper();
        }

        public async Task<DeviceModel> Create(DeviceModel deviceModel)
        {
            var device = mapper.Map<DeviceModel, Device>(deviceModel);

            var createdDevice=await _genericRepository.Create(device);

            var createdDeviceModel = mapper.Map<Device, DeviceModel>(createdDevice);

            return createdDeviceModel;
        }

        public async Task<List<DeviceModel>> Get()
        {
            var devices = await _genericRepository.Get();

            var devicesModel = mapper.Map<List<Device>, List<DeviceModel>>(devices);

            return devicesModel;
        }
    }
}
