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
    public async Task UserService_8080_PostUser()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"user_{uniqueId}";
        string uniqueEmail = $"user{uniqueId}@example.com";
        string uniqueAddress = $"Address_{uniqueId}";

        string requestBody = JsonConvert.SerializeObject(new
        {
            Username = uniqueUsername,
            Email = uniqueEmail,
            Address = uniqueAddress
        });
        HttpResponseMessage response = await _httpClient.PostAsync("/api/user", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Order(2)]
    public async Task UserService_8080_GetUsers()
    {
        HttpResponseMessage prodresponse = await _httpClient.GetAsync("/api/user");
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test, Order(3)]
    public async Task ApiGatewayService_8081_PostUser()
    {
         string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"user_{uniqueId}";
        string uniqueEmail = $"user{uniqueId}@example.com";
        string uniqueAddress = $"Address_{uniqueId}";

        string requestBody = JsonConvert.SerializeObject(new
        {
            Username = uniqueUsername,
            Email = uniqueEmail,
            Address = uniqueAddress
        });
        HttpResponseMessage response = await _httpClient1.PostAsync("/user-api/user", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [Test, Order(4)]
    public async Task ApiGatewayService_8081_GetUsers()
    {
        HttpResponseMessage prodresponse = await _httpClient1.GetAsync("/user-api/user");
        Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    }

    [Test]
    public void User_Properties_UserId_ReturnExpectedDataTypes_int()
    {
        string assemblyName = "UserService";
        string typeName = "UserService.Models.User";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("UserId");
        Assert.IsNotNull(propertyInfo, "The property 'UserId' was not found on the User class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(int), propertyType, "The data type of 'UserId' property is not as expected (int).");
    }

    [Test]
    public void User_Properties_Username_ReturnExpectedDataTypes_string()
    {
        string assemblyName = "UserService";
        string typeName = "UserService.Models.User";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Username");
        Assert.IsNotNull(propertyInfo, "The property 'Username' was not found on the User class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(string), propertyType, "The data type of 'Username' property is not as expected (string).");
    }

    [Test]
    public void User_Properties_Email_ReturnExpectedDataTypes_String()
    {
        string assemblyName = "UserService";
        string typeName = "UserService.Models.User";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Email");
        Assert.IsNotNull(propertyInfo, "The property 'Email' was not found on the User class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(string), propertyType, "The data type of 'Email' property is not as expected (string).");
    }

    [Test]
    public void User_Properties_Address_ReturnExpectedDataTypes_String()
    {
        string assemblyName = "UserService";
        string typeName = "UserService.Models.User";
        Assembly assembly = Assembly.Load(assemblyName);
        Type commuterType = assembly.GetType(typeName);
        PropertyInfo propertyInfo = commuterType.GetProperty("Address");
        Assert.IsNotNull(propertyInfo, "The property 'Address' was not found on the User class.");
        Type propertyType = propertyInfo.PropertyType;
        Assert.AreEqual(typeof(string), propertyType, "The data type of 'Address' property is not as expected (string).");
    }
}