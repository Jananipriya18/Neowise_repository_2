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

    [Test]
        public void AuthenticationControllerExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.AuthenticationController";

            Assembly assembly = Assembly.Load(assemblyName);
            Type AuthenticationControllerType = assembly.GetType(typeName);

            Assert.IsNotNull(AuthenticationControllerType, "AuthenticationController does not exist in the assembly.");
        }

    [Test, Order(1)]
    public async Task Backend_TestRegisterUser()
    {
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";


        string requestBody = $"{{\"password\": \"abc@123A\",\"email\": \"{uniqueEmail}\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/user/login", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        Console.WriteLine(response.Content);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(response.StatusCode);
        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    // [Test, Order(2)]
    // public async Task Backend_TestLoginAdmin()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     string uniqueusername = $"abcd_{uniqueId}";
    //     string uniquepassword = $"abcdA{uniqueId}@123";
    //     string uniquerole = "admin";
    //     string requestBody = $"{{\"Username\" : \"{uniqueusername}\",\"Password\" : \"{uniquepassword}\",\"Role\" : \"{uniquerole}\" }}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/Authentication/registeration", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    //     Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

    //     string requestBody1 = $"{{\"Username\" : \"{uniqueusername}\",\"Password\" : \"{uniquepassword}\"}}";
    //     HttpResponseMessage response1 = await _httpClient.PostAsync("/api/auth/login", new StringContent(requestBody1, Encoding.UTF8, "application/json"));
    //     Assert.AreEqual(HttpStatusCode.OK, response1.StatusCode);
    //     string responseBody = await response1.Content.ReadAsStringAsync();
    // }

    // [Test, Order(2)]
    // public async Task Backend_TestRegisterAdmin()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    //     string requestBody = $"{{\"Password\": \"abc@123A\", \"UserName\": \"{uniqueUsername}\",\"Role\": \"admin\"}}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/auth/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //     Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    //     string responseBody = await response.Content.ReadAsStringAsync();
    //     //Assert.AreEqual("true", responseBody);
    // }

    //[Test, Order(3)]
    //public async Task Backend_TestLoginAdmin()
    //{
    //    string uniqueId = Guid.NewGuid().ToString();

    //    string uniqueUsername = $"admin_{uniqueId}";
    //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    //    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"Admin\",\"email\": \"{uniqueEmail}\"}}";
    //    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/authenticate/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    //    string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
    //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/authenticate/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //    string token = responseMap.token;

    //    Assert.IsNotNull(token);
    //    // Assert or process the login response as needed
    //}



    //[Test, Order(4)]
    //public async Task Backend_TestLoginUser()
    //{

    //    string uniqueId = Guid.NewGuid().ToString();

    //    string uniqueUsername = $"admin_{uniqueId}";
    //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    //    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"APPLICANT\",\"email\": \"{uniqueEmail}\"}}";
    //    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/authenticate/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);
    //    string requestBody = $"{{\"email\": \"{uniqueEmail}\", \"password\": \"abc@123A\"}}";

    //    HttpResponseMessage response = await _httpClient.PostAsync("api/authenticate/login", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    //    string responseString = await response.Content.ReadAsStringAsync();
    //    dynamic responseMap = JsonConvert.DeserializeObject(responseString);

    //    string token = responseMap.token;

    //    Assert.IsNotNull(token);
    //}

    //[Test, Order(5)]
    //public async Task Backend_TestCreateJobAdmin()
    //{
    //    string uniqueId = Guid.NewGuid().ToString();

    //    // Use a dynamic and unique userName for admin (appending timestamp)
    //    string uniqueUsername = $"admin_{uniqueId}";
    //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    //    // Assume you have a valid admin registration method, adjust the request body accordingly
    //    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"Admin\",\"email\": \"{uniqueEmail}\"}}";
    //    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/authenticate/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    //    // Now, perform the login for the admin user
    //    string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
    //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/authenticate/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //    string token = responseMap.token;

    //    Assert.IsNotNull(token);

    //    string uniquetitle = Guid.NewGuid().ToString();

    //    // Use a dynamic and unique userName for admin (appending timestamp)
    //    string uniquejobTitle_ = $"jobTitle_{uniquetitle}";

    //    string jobJson = $"{{\"title\":\"{uniquejobTitle_}\",\"dept\":\"test\",\"location\":\"test\",\"responsibility\":\"test\",\"qualification\":\"test\",\"deadline\":\"2022-12-31T23:59:59.000+00:00\", \"category\":\"Free\"}}";
    //    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    //    HttpResponseMessage response = await _httpClient.PostAsync("/api/job",
    //        new StringContent(jobJson, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    //}

    //[Test, Order(6)]
    //public async Task Backend_TestGetAllJobsForAdmin()
    //{
    //    string uniqueId = Guid.NewGuid().ToString();

    //    // Use a dynamic and unique userName for admin (appending timestamp)
    //    string uniqueUsername = $"admin_{uniqueId}";
    //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    //    // Assume you have a valid admin registration method, adjust the request body accordingly
    //    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"Admin\",\"email\": \"{uniqueEmail}\"}}";
    //    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/authenticate/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    //    // Now, perform the login for the admin user
    //    string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
    //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/authenticate/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //    string token = responseMap.token;

    //    Assert.IsNotNull(token);


    //    Console.WriteLine("admin111" + token);
    //    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    //    HttpResponseMessage response = await _httpClient.GetAsync("/api/job");

    //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    //}

    ////[Test, Order(5)]
    ////public async Task Backend_TestCreateApplication()
    ////{
    ////    // Ensure a user is logged in
    ////    await Backend_TestLoginUser();

    ////    // Assume you have a valid application creation method, adjust the request body accordingly
    ////    string applicationRequestBody = "{\"id\":1,\"applicationName\":\"Application1\",\"jobTitle\":\"Software Engineer\",\"status\":\"Applied\",\"job\":{\"jobid\": 1}}";
    ////    HttpResponseMessage response = await _httpClient.PostAsync("/api/Application", new StringContent(applicationRequestBody, Encoding.UTF8, "application/json"));

    ////    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

    ////    // Read the response content asynchronously
    ////    string responseBody = await response.Content.ReadAsStringAsync();

    ////    // Assert that the response content is equal to "Application added"
    ////    //Assert.AreEqual("Application added", responseBody);
    ////}

    ////[Test, Order(6)]
    ////public async Task Backend_TestGetAllJobsForUser()
    ////{
    ////    // Ensure a user is logged in
    ////    await Backend_TestLoginUser();

    ////    HttpResponseMessage response = await _httpClient.GetAsync("/api/applicant/job");
    ////    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    ////    // Assert or process the response as needed
    ////}

    //[Test, Order(7)]
    //public async Task Backend_TestGetPremiumJobsAdmin()
    //{
    //    string uniqueId = Guid.NewGuid().ToString();

    //    // Use a dynamic and unique userName for admin (appending timestamp)
    //    string uniqueUsername = $"admin_{uniqueId}";
    //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    //    // Assume you have a valid admin registration method, adjust the request body accordingly
    //    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"Admin\",\"email\": \"{uniqueEmail}\"}}";
    //    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/authenticate/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    //    // Now, perform the login for the admin user
    //    string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
    //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/authenticate/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //    string token = responseMap.token;

    //    Assert.IsNotNull(token);
    //    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    //    HttpResponseMessage response = await _httpClient.GetAsync("/api/job/premiumjobs");
    //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    //    // Assert or process the response as needed
    //}

    //[Test, Order(8)]
    //public async Task Backend_TestGetFreeJobsAdmin()
    //{
    //    string uniqueId = Guid.NewGuid().ToString();

    //    // Use a dynamic and unique userName for admin (appending timestamp)
    //    string uniqueUsername = $"admin_{uniqueId}";
    //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    //    // Assume you have a valid admin registration method, adjust the request body accordingly
    //    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"ADMIN\",\"email\": \"{uniqueEmail}\"}}";
    //    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/authenticate/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    //    // Now, perform the login for the admin user
    //    string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
    //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/authenticate/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //    string token = responseMap.token;

    //    Assert.IsNotNull(token);
    //    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    //    HttpResponseMessage response = await _httpClient.GetAsync("/api/job/freejobs");
    //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    //}

    //[Test, Order(9)]
    //public async Task Backend_TestGetAllApplicantsByAdmin()
    //{
    //    string uniqueId = Guid.NewGuid().ToString();

    //    // Use a dynamic and unique userName for admin (appending timestamp)
    //    string uniqueUsername = $"admin_{uniqueId}";
    //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    //    // Assume you have a valid admin registration method, adjust the request body accordingly
    //    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"ADMIN\",\"email\": \"{uniqueEmail}\"}}";
    //    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/auth/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    //    // Now, perform the login for the admin user
    //    string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
    //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/auth/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //    string token = responseMap.token;

    //    Assert.IsNotNull(token);
    //    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    //    HttpResponseMessage response = await _httpClient.GetAsync("/api/job/applicants");
    //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    //}

    //[Test, Order(10)]
    //public async Task Backend_TestGetFreeJobsApplicant()
    //{
    //    string uniqueId = Guid.NewGuid().ToString();

    //    // Use a dynamic and unique userName for admin (appending timestamp)
    //    string uniqueUsername = $"admin_{uniqueId}";
    //    string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    //    // Assume you have a valid admin registration method, adjust the request body accordingly
    //    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"ADMIN\",\"email\": \"{uniqueEmail}\"}}";
    //    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/authenticate/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    //    // Now, perform the login for the admin user
    //    string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
    //    HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/authenticate/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

    //    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //    string token = responseMap.token;

    //    Assert.IsNotNull(token);
    //    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    //    HttpResponseMessage response = await _httpClient.GetAsync("/api/applicant/freeJobs");
    //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    //}

    ////[Test, Order(5)]
    ////public async Task Backend_TestCreateApplicationByApplicants()
    ////{
    ////    string uniqueId = Guid.NewGuid().ToString();

    ////    // Use a dynamic and unique userName for admin (appending timestamp)
    ////    string uniqueUsername = $"admin_{uniqueId}";
    ////    string uniqueEmail = $"abcd{uniqueId}@gmail.com";


    ////    // Assume you have a valid admin registration method, adjust the request body accordingly
    ////    string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueUsername}\",\"role\": \"APPLICANT\",\"email\": \"{uniqueEmail}\"}}";
    ////    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/auth/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

    ////    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    ////    // Now, perform the login for the admin user
    ////    string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
    ////    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/auth/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

    ////    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    ////    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    ////    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    ////    string token = responseMap.token;

    ////    Assert.IsNotNull(token);

    ////    string uniquetitle = Guid.NewGuid().ToString();

    ////    // Use a dynamic and unique userName for admin (appending timestamp)
    ////    string uniquejobTitle_ = $"jobTitle_{uniquetitle}";

    ////    string jobJson = $"{{\"applicantName\":\"{uniquejobTitle_}\",\"jobTitle\":\"test\",\"status\":\"test\"}}";
    ////    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    ////    HttpResponseMessage response = await _httpClient.PostAsync("/api/applicant",
    ////        new StringContent(jobJson, Encoding.UTF8, "application/json"));

    ////    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    ////}

    [TearDown]
    public void TearDown()
    {
        // Cleanup or additional teardown logic if needed.
        _httpClient.Dispose();
    }
}
