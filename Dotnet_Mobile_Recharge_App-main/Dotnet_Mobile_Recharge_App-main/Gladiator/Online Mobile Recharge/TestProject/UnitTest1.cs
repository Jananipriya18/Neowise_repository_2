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
    public async Task Backend_TestAddAddon()
    {
        string uniqueId = Guid.NewGuid().ToString();

        // Use a dynamic and unique userName for admin (appending timestamp)
        string uniqueUsername = $"admin_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        // Assume you have a valid admin registration method, adjust the request body accordingly
        string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"Admin\",\"email\": \"{uniqueEmail}\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/auth/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Now, perform the login for the admin user
        string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/authenticate/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
        string responseBody = await loginResponse.Content.ReadAsStringAsync();

        dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

        string token = responseMap.token;

        Assert.IsNotNull(token);

        string uniquetitle = Guid.NewGuid().ToString();

        // Use a dynamic and unique job title
        string uniquejobTitle = $"jobTitle_{uniquetitle}";

        string jobJson = $"{{\"title\":\"{uniquejobTitle}\",\"dept\":\"test\",\"location\":\"test\",\"responsibility\":\"test\",\"qualification\":\"test\",\"deadline\":\"2022-12-31T23:59:59.000+00:00\", \"category\":\"Free\"}}";
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        HttpResponseMessage jobResponse = await _httpClient.PostAsync("/api/addAddon",
            new StringContent(jobJson, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, jobResponse.StatusCode);
    }
}