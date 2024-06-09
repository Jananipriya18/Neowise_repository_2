using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

public class Tests
{
    private HttpClient _httpClient;
    private HttpClient _httpClient1;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _httpClient1 = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:8080");
        _httpClient1.BaseAddress = new Uri("http://localhost:8081");
    }

    [Test, Order(1)]
    public async Task OrderService_8080_PostOrder()
    {
        string uniqueId = Guid.NewGuid().ToString();

        string uniquename = $"abcd_{uniqueId}";

        string requestBody = $"{{\"customername\": \"{uniquename}\", \"description\": \"abc@123A\", \"totalAmount\": 10.23}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/order", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Order(2)]
    public async Task OrderService_8080_GetOrders()
    {
        HttpResponseMessage prodresponse = await _httpClient.GetAsync("/api/order");
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test, Order(3)]
    public async Task ApiGatewayService_8081_PostOrder()
    {
        string uniqueId = Guid.NewGuid().ToString();

        string uniquename = $"abcd_{uniqueId}";

        string requestBody = $"{{\"customername\": \"{uniquename}\", \"description\": \"abc@123A\", \"totalAmount\": 10.23}}";
        HttpResponseMessage response = await _httpClient1.PostAsync("/order-api/order", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Order(4)]
    public async Task ApiGatewayService_8081_GetOrders()
    {
        HttpResponseMessage prodresponse = await _httpClient1.GetAsync("/order-api/order");
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test]
    public void Order_Properties_OrderId_ReturnExpectedDataTypes_int()
    {
        string assemblyName = "OrderService";
        string typeName = "OrderService.Models.Order";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("OrderId");
        Assert.IsNotNull(propertyInfo, "The property 'OrderId' was not found on the Order class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(int), propertyType, "The data type of 'OrderId' property is not as expected (int).");
    }

    [Test]
    public void Order_Properties_TotalAmount_ReturnExpectedDataTypes_decimal()
    {
        string assemblyName = "OrderService";
        string typeName = "OrderService.Models.Order";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("TotalAmount");
        Assert.IsNotNull(propertyInfo, "The property 'TotalAmount' was not found on the Order class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(decimal), propertyType, "The data type of 'TotalAmount' property is not as expected (decimal).");
    }

    [Test]
    public void Order_Properties_OrderDate_ReturnExpectedDataTypes_DateTime()
    {
        string assemblyName = "OrderService";
        string typeName = "OrderService.Models.Order";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("OrderDate");
        Assert.IsNotNull(propertyInfo, "The property 'OrderDate' was not found on the Order class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(DateTime), propertyType, "The data type of 'OrderDate' property is not as expected (DateTime).");
    }

    [Test]
    public void Order_Properties_CustomerName_ReturnExpectedDataTypes_String()
    {
        string assemblyName = "OrderService";
        string typeName = "OrderService.Models.Order";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("CustomerName");
        Assert.IsNotNull(propertyInfo, "The property 'CustomerName' was not found on the Order class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(string), propertyType, "The data type of 'CustomerName' property is not as expected (string).");
    }
}