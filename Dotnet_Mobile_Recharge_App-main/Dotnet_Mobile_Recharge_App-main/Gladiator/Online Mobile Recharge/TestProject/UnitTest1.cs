using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

[TestFixture]
public class SpringappApplicationTests
{
    private HttpClient _httpClient;
    private string _generatedToken;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io"); 
    }

    [Test, Order(1)]
    public async Task Backend_TestRegisterUser()
    {
        string uniqueId = Guid.NewGuid().ToString();
 
        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
 
        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"customer\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
 
        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
 
        // Assuming you get a 200 OK status for successful registration
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
 
    [Test, Order(2)]
    public async Task Backend_TestLoginUser()
    {
        string uniqueId = Guid.NewGuid().ToString();
 
        string uniqueusername = $"abcd_{uniqueId}";
        string uniquepassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
        string uniquerole = "customer";
        string requestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"{uniquerole}\" }}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
 
        string requestBody1 = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
        HttpResponseMessage response1 = await _httpClient.PostAsync("/api/login", new StringContent(requestBody1, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, response1.StatusCode);
        string responseBody = await response1.Content.ReadAsStringAsync();
    }
 
    [Test, Order(3)]
    public async Task Backend_TestRegisterAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
 
        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"admin\"}}";
       
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
 
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        string responseBody = await response.Content.ReadAsStringAsync();
        // Add your assertions based on the response if needed
    }
 
    [Test, Order(4)]
    public async Task Backend_TestLoginAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();
 
        string uniqueusername = $"abcd_{uniqueId}";
        string uniquepassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
        string uniquerole = "admin";
        string requestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"{uniquerole}\" }}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
 
        string requestBody1 = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
        HttpResponseMessage response1 = await _httpClient.PostAsync("/api/login", new StringContent(requestBody1, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, response1.StatusCode);
        string responseBody = await response1.Content.ReadAsStringAsync();
    }

    [Test, Order(5)]
public async Task Backend_TestAddAddon()
{
    string uniqueId = Guid.NewGuid().ToString();

    // Use a dynamic and unique userName for admin (appending timestamp)
    string uniqueUsername = $"admin_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";
    // Assume you have a valid admin registration method, adjust the request body accordingly
    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"Username\": \"{uniqueUsername}\",\"role\": \"admin\",\"email\": \"{uniqueEmail}\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    // Now, perform the login for the admin user
    string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string responseBody = await loginResponse.Content.ReadAsStringAsync();
    Console.WriteLine(responseBody);
    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    string token = responseMap.token;

    Assert.IsNotNull(token);

    // Create addon data
    string uniqueAddonName = $"addon_{Guid.NewGuid()}";
    decimal addonPrice = 10.99m;
    string addonDetails = "test description";
    string addonValidity = "test validity";

    // Update the addonJson to match your Swagger JSON structure
    string addonJson = $"{{\"userId\": 0, \"addonName\":\"{uniqueAddonName}\",\"addonPrice\":{addonPrice},\"addonDetails\":\"{addonDetails}\",\"addonValidity\":\"{addonValidity}\"}}";

    // Set Authorization header with the token
    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    // Send request to AddAddon endpoint
    HttpResponseMessage addonResponse = await _httpClient.PostAsync("/api/addAddon",
        new StringContent(addonJson, Encoding.UTF8, "application/json"));

    // Print response content in case of failure
    if (addonResponse.StatusCode != HttpStatusCode.OK)
    {
        string responseContent = await addonResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Content: {responseContent}");
    }

    // Assert the response status code is OK
    Assert.AreEqual(HttpStatusCode.OK, addonResponse.StatusCode);
}

}