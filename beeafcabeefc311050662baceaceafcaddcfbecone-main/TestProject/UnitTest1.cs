using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;
using System.Reflection;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class OrderControllerTests
    {
         private const string OrderServiceName = "OrderService";
         private const string OrderControllerName = "OrderController";

        private HttpClient _httpClient;
        private Assembly _assembly;
        private Order _testOrder;

        [SetUp]
        public async Task Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080"); // Base URL of your API
            _assembly = Assembly.GetAssembly(typeof(dotnetapp.Services.OrderService));
            
            // Create a new test order before each test case
            _testOrder = await CreateTestOrder();
        }

        private async Task<Order> CreateTestOrder()
        {
            var newOrder = new Order
            {
                OrderId = 0,
                CustomerName = "Test Order",
                OrderDate = DateTime.Now,
                TotalAmount = 9.99m,
                Status = "Test Order Description"
            };

            var json = JsonConvert.SerializeObject(newOrder);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/order", content);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());
        }

        [Test]
        public async Task Test_GetAllOrders_ReturnsListOfOrders()
        {
            // Arrange - no specific arrangement needed as we're not modifying state
            // Act
            var response = await _httpClient.GetAsync("api/order");
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<Order[]>(content);

            Assert.IsNotNull(orders);
            Assert.IsTrue(orders.Length > 0);
        }

        [Test]
        public async Task Test_GetOrderById_ValidId_ReturnsOrder()
        {
            // Arrange - no specific arrangement needed as we're not modifying state
            // Act
            var response = await _httpClient.GetAsync($"api/order/{_testOrder.OrderId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(content);

            Assert.IsNotNull(order);
            Assert.AreEqual(_testOrder.OrderId, order.OrderId);
        }

        [Test]
        public async Task Test_GetOrderById_InvalidId_ReturnsNotFound()
        {
            var response = await _httpClient.GetAsync("api/order/999999"); // Using an invalid ID
            
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void Test_OrderService_Exist()
        {
            AssertServiceInstanceNotNull(OrderServiceName);
        }

        private void AssertServiceInstanceNotNull(string serviceName)
        {
            Type serviceType = _assembly.GetType($"dotnetapp.Services.{serviceName}");

            if (serviceType == null)
            {
                Assert.Fail($"Service {serviceName} does not exist.");
            }

            object serviceInstance = Activator.CreateInstance(serviceType);
            Assert.IsNotNull(serviceInstance);
        }
        [Test]
        public void Test_OrderController_Exist()
        {
            AssertControllerClassExist(OrderControllerName);
        }

        private void AssertControllerClassExist(string controllerName)
        {
            Type controllerType = _assembly.GetType($"dotnetapp.Controllers.{controllerName}");

            if (controllerType == null)
            {
                Assert.Fail($"Controller {controllerName} does not exist.");
            }
        }


        [TearDown]
        public async Task Cleanup()
        {
            _httpClient.Dispose();
        }

    }
}
