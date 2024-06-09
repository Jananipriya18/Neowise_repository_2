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
    public async Task ProductService_8080_PostProduct()
    {
        string uniqueId = Guid.NewGuid().ToString();

        string uniquename = $"abcd_{uniqueId}";

        string requestBody = $"{{\"name\": \"{uniquename}\", \"description\": \"abc@123A\", \"price\": 10.23}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/product", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Order(2)]
    public async Task ProductService_8080_GetProducts()
    {
        HttpResponseMessage prodresponse = await _httpClient.GetAsync("/api/product");
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test, Order(3)]
    public async Task ApiGatewayService_8081_PostProduct()
    {
        string uniqueId = Guid.NewGuid().ToString();

        string uniquename = $"abcd_{uniqueId}";

        string requestBody = $"{{\"name\": \"{uniquename}\", \"description\": \"abc@123A\", \"price\": 10.23}}";
        HttpResponseMessage response = await _httpClient1.PostAsync("/product-api/product", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Order(4)]
    public async Task ApiGatewayService_8081_GetProducts()
    {
        HttpResponseMessage prodresponse = await _httpClient1.GetAsync("/product-api/product");
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test]
    public void Product_Properties_ProductId_ReturnExpectedDataTypes_int()
    {
        string assemblyName = "ProductService";
        string typeName = "ProductService.Models.Product";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("ProductId");
        Assert.IsNotNull(propertyInfo, "The property 'ProductId' was not found on the Product class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(int), propertyType, "The data type of 'ProductId' property is not as expected (int).");
    }

    [Test]
    public void Product_Properties_Price_ReturnExpectedDataTypes_decimal()
    {
        string assemblyName = "ProductService";
        string typeName = "ProductService.Models.Product";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Price");
        Assert.IsNotNull(propertyInfo, "The property 'Price' was not found on the Product class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(decimal), propertyType, "The data type of 'Price' property is not as expected (decimal).");
    }

    [Test]
    public void Product_Properties_Name_ReturnExpectedDataTypes_String()
    {
        string assemblyName = "ProductService";
        string typeName = "ProductService.Models.Product";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Name");
        Assert.IsNotNull(propertyInfo, "The property 'Name' was not found on the Product class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(string), propertyType, "The data type of 'Name' property is not as expected (string).");
    }

    [Test]
    public void Product_Properties_Description_ReturnExpectedDataTypes_String()
    {
        string assemblyName = "ProductService";
        string typeName = "ProductService.Models.Product";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Description");
        Assert.IsNotNull(propertyInfo, "The property 'Description' was not found on the Product class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(string), propertyType, "The data type of 'Description' property is not as expected (string).");
    }
}