using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TJ.Application.DeviceActions;
using TJ.Domain.Entities;
using TJ.Interfaces.DbInterfaces;
using TJ.Models;
using TJ.Persistence.Repositories;

namespace TJ.ApplicationUnitTests.DeviceActionsTests
{
    [TestClass]
    public class DeviceActionTests
    {
        private IDeviceAction _deviceAction;
        private IGenericRepository<Device> _genericRepositoryDevice;
        private ConnectionFactory _connectionFactory;
       
        [TestInitialize]
        public void Setup()
        {
            _connectionFactory = new ConnectionFactory();
            _genericRepositoryDevice = new GenericRepository<Device>(_connectionFactory.CreateContextForSQLite());
            _deviceAction = new DeviceAction(_genericRepositoryDevice);
        }

        [TestCleanup]
        public void dispose() {
            _connectionFactory.Dispose();
        }

        [TestMethod]
        public async Task CreateNewDevice_ThenDeviceCreated()
        {
            //Arrange
            string deviceName = "TvTest";
            var timeBeforeTest = DateTime.Now;
            DeviceModel deviceModelToCreate = new DeviceModel()
            {
                Name = deviceName
            };

            //Act
            var createdDevice= await _deviceAction.Create(deviceModelToCreate);

            //Assert
            Assert.IsNotNull(createdDevice);
            Assert.AreNotEqual(createdDevice.Id, 0);
            Assert.AreEqual(deviceName, createdDevice.Name);
            Assert.IsTrue(createdDevice.DateAdded > timeBeforeTest);
        }

        [TestMethod]
        public async Task Get_ThenReturnAllDevices() 
        {
            //Arrang
            var injectedDevices = await InjectDevicesInDb();
            
            //Act
            var result = await _deviceAction.Get();

            //Assert
            Assert.AreEqual(injectedDevices.Count, result.Count);
        }

        [TestMethod]
        public async Task Get_ThenReturnEmptyList()
        {
            //Arrang

            //Act
            var result = await _deviceAction.Get();

            //Assert
            Assert.AreEqual(0, result.Count);
        }


        public async Task<List<Device>> InjectDevicesInDb()
        {
            Device device1 = new Device()
            {
                Name = "device1"
            };
            Device device2 = new Device()
            {
                Name = "device2"
            };
            Device device3 = new Device()
            {
                Name = "device3"
            };

            device1= await _genericRepositoryDevice.Create(device1);
            device2= await _genericRepositoryDevice.Create(device2);
            device3= await _genericRepositoryDevice.Create(device3);

            List<Device> devices = new List<Device>
            {
                device1,
                device2,
                device3
            };

            return devices;
        }


    }
}
