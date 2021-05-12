using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TJ.Domain.Entities;
using TJ.Models;
using TJ.Persistence.Repositories;

namespace TJ.ApplicationInegrationTests.DeviceControllerTests
{
    [TestClass]
    public class DeviceControllerTest : SetupTestEnviroment
    {


        [TestMethod]
        public async Task PostDeviceModel_ThenDeviceCreated()
        {
            //Arrange
            var timeBeforeTest = DateTime.Now;
            var deviceModelToCreate = new DeviceModel()
            {
                Name = "radioTest"
            };
            var deviceModelJSON = JsonConvert.SerializeObject(deviceModelToCreate);

            //Act
            var response = await testClient.PostAsync("/api/device/create", new StringContent(deviceModelJSON, Encoding.UTF8, "application/json"));

            var stringResponse = await response.Content.ReadAsStringAsync();

            var createdDevice = JsonConvert.DeserializeObject<DeviceModel>(stringResponse);
            
            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(createdDevice.Name, deviceModelToCreate.Name);
            Assert.AreNotEqual(0, createdDevice.Id);
            Assert.IsTrue(createdDevice.DateAdded > timeBeforeTest);
        }

        [TestMethod]
        public async Task Get_ThenReturnListDevices()
        {
            //Arrange
            var injectedDevices =await InjectDevicesInDb();
            //Act
            var response = await testClient.GetAsync("/api/device/get");

            var stringResponse = await response.Content.ReadAsStringAsync();

            var devicesList = JsonConvert.DeserializeObject<List<DeviceModel>>(stringResponse);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(devicesList.Count, injectedDevices.Count);
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
           var _genericRepositoryDevice = new GenericRepository<Device>(dbContext);
            device1 = await _genericRepositoryDevice.Create(device1);
            device2 = await _genericRepositoryDevice.Create(device2);
            device3 = await _genericRepositoryDevice.Create(device3);

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
