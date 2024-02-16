using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;

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

//     [Test]
// public async Task Backend_TestAddAddon()
// {
//     // Assuming registration and login have already been done in previous test cases

//     // Use a dynamic and unique userName for admin (appending timestamp)
//     string uniqueUsername = $"admin_{Guid.NewGuid()}";
//     string uniqueEmail = $"abcd{Guid.NewGuid()}@gmail.com";

//     // Register the admin user
//     string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"Username\": \"{uniqueUsername}\",\"role\": \"admin\",\"email\": \"{uniqueEmail}\"}}";
//     HttpResponseMessage adminRegistrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, adminRegistrationResponse.StatusCode);

//     // Login the admin user
//     string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
//     HttpResponseMessage adminLoginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, adminLoginResponse.StatusCode);
//     string adminLoginResponseBody = await adminLoginResponse.Content.ReadAsStringAsync();
//     dynamic adminLoginResponseMap = JsonConvert.DeserializeObject(adminLoginResponseBody);

//     // Store the generated token
//     _generatedToken = adminLoginResponseMap.token;
//     var uniqueAddonName = "addon"
//     // Create addon data
//     string uniqueAddonName = $"addon_{Guid.NewGuid()}";
//     decimal addonPrice = 10.99m;
//     string addonDetails = "test description";
//     string addonValidity = "test validity";

//     // Update the addonJson to match your Swagger JSON structure
//     string addonJson = $"{{\"userId\": {adminLoginResponseMap.userId}, \"addonName\":\"{uniqueAddonName}\",\"addonPrice\":{addonPrice},\"addonDetails\":\"{addonDetails}\",\"addonValidity\":\"{addonValidity}\"}}";

//     // Set Authorization header with the stored token
//     _httpClient.DefaultRequestHeaders.Clear();  // Clear any existing headers
//     _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _generatedToken);

//     // Send request to AddAddon endpoint
//     HttpResponseMessage addonResponse = await _httpClient.PostAsync("/api/addAddon",
//         new StringContent(addonJson, Encoding.UTF8, "application/json"));

//     // Print response content in case of failure
//     if (addonResponse.StatusCode != HttpStatusCode.OK)
//     {
//         string responseContent = await addonResponse.Content.ReadAsStringAsync();
//         Console.WriteLine($"Response Content: {responseContent}");
//     }

//     // Assert the response status code is OK
//     Assert.AreEqual(HttpStatusCode.OK, addonResponse.StatusCode);
// }
[Test]
public async Task Backend_TestAddAddon()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"admin\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to add an addon
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    var review = new
    {
        AddonValidity = "Test subject",
        AddonDetails = "Test body",
        AddonPrice = 5,
        AddonName = "sample name"
    };

    string addonRequestBody = JsonConvert.SerializeObject(review);
    HttpResponseMessage addonResponse = await _httpClient.PostAsync("api/addAddon", new StringContent(addonRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, addonResponse.StatusCode);
}

[Test]
public async Task Backend_TestGetAddonsAsAdmin()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"admin\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to get addons
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    // Make a request to get addons
    HttpResponseMessage getAddonsResponse = await _httpClient.GetAsync("api/getAddon");
    Assert.AreEqual(HttpStatusCode.OK, getAddonsResponse.StatusCode);

    // Validate the response content (assuming the response is a JSON array of addons)
    string getAddonsResponseBody = await getAddonsResponse.Content.ReadAsStringAsync();
    var addons = JsonConvert.DeserializeObject<List<Addon>>(getAddonsResponseBody);
    Assert.IsNotNull(addons);
    Assert.IsTrue(addons.Any());
}

[Test]
public async Task Backend_TestGetAddonsAsCustomer()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"customer\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to get addons
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    // Make a request to get addons
    HttpResponseMessage getAddonsResponse = await _httpClient.GetAsync("api/getAddon");
    Assert.AreEqual(HttpStatusCode.OK, getAddonsResponse.StatusCode);

    // Validate the response content (assuming the response is a JSON array of addons)
    string getAddonsResponseBody = await getAddonsResponse.Content.ReadAsStringAsync();
    var addons = JsonConvert.DeserializeObject<List<Addon>>(getAddonsResponseBody);
    Assert.IsNotNull(addons);
    Assert.IsTrue(addons.Any());
}

[Test]
public async Task Backend_TestUpdateAddon()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"admin\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to add an addon
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    // Add an addon
    var addAddonReview = new
    {
        AddonValidity = "Test subject",
        AddonDetails = "Test body",
        AddonPrice = 5,
        AddonName = "sample name"
    };

    string addonRequestBody = JsonConvert.SerializeObject(addAddonReview);
    HttpResponseMessage addonResponse = await _httpClient.PostAsync("api/addAddon", new StringContent(addonRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, addonResponse.StatusCode);

    // Get the list of addons
    HttpResponseMessage getAddonsResponse = await _httpClient.GetAsync("api/getAddon");
    Assert.AreEqual(HttpStatusCode.OK, getAddonsResponse.StatusCode);
    string getAddonsResponseBody = await getAddonsResponse.Content.ReadAsStringAsync();
    var addons = JsonConvert.DeserializeObject<List<Addon>>(getAddonsResponseBody);
    Assert.IsNotNull(addons);
    Assert.IsTrue(addons.Any());

    // Update the newly added addon
    var updatedAddon = new
    {
        AddonValidity = "Updated subject",
        AddonDetails = "Updated body",
        AddonPrice = 10,
        AddonName = "updated name"
    };

    string updateAddonRequestBody = JsonConvert.SerializeObject(updatedAddon);

    // Assuming AddonId property exists in Addon model
    long addonIdToUpdate = addons.FirstOrDefault()?.AddonId ?? 1;
    HttpResponseMessage updateAddonResponse = await _httpClient.PutAsync($"api/editAddon/{addonIdToUpdate}", new StringContent(updateAddonRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, updateAddonResponse.StatusCode);
}

[Test]
public async Task Backend_TestDeleteAddon()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"admin\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to add an addon
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    // Add an addon
    var addAddonReview = new
    {
        AddonValidity = "Test subject",
        AddonDetails = "Test body",
        AddonPrice = 5,
        AddonName = "sample name"
    };

    string addonRequestBody = JsonConvert.SerializeObject(addAddonReview);
    HttpResponseMessage addonResponse = await _httpClient.PostAsync("api/addAddon", new StringContent(addonRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, addonResponse.StatusCode);

    // Get the list of addons
    HttpResponseMessage getAddonsResponse = await _httpClient.GetAsync("api/getAddon");
    Assert.AreEqual(HttpStatusCode.OK, getAddonsResponse.StatusCode);
    string getAddonsResponseBody = await getAddonsResponse.Content.ReadAsStringAsync();
    var addons = JsonConvert.DeserializeObject<List<Addon>>(getAddonsResponseBody);
    Assert.IsNotNull(addons);
    Assert.IsTrue(addons.Any());

    // Delete the newly added addon
    // Assuming AddonId property exists in Addon model
    long addonIdToDelete = addons.FirstOrDefault()?.AddonId ?? 1;
    HttpResponseMessage deleteAddonResponse = await _httpClient.DeleteAsync($"api/deleteAddon/{addonIdToDelete}");
    Assert.AreEqual(HttpStatusCode.OK, deleteAddonResponse.StatusCode);

    // Verify that the addon is deleted
    HttpResponseMessage verifyDeleteResponse = await _httpClient.GetAsync($"api/getAddon/{addonIdToDelete}");
    Assert.AreEqual(HttpStatusCode.NotFound, verifyDeleteResponse.StatusCode);
}

[Test]
public async Task Backend_TestAddPlan()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"admin\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to add an addon
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    var planDetails = new
    {
        PlanName = "Test Plan",
        PlanPrice = 10,
        PlanDescription = "Test plan description",
        PlanType = "prepaid",
        PlanValidity = "30",
        PlanOffer = "Test Offer"
    };

    string planRequestBody = JsonConvert.SerializeObject(planDetails);
    HttpResponseMessage planResponse = await _httpClient.PostAsync("api/addPlan", new StringContent(planRequestBody, Encoding.UTF8, "application/json"));
    
    // Assert that the plan is added successfully
    Assert.AreEqual(HttpStatusCode.OK, planResponse.StatusCode);
}

[Test]
public async Task Backend_TestGetPlans()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"admin\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to add an addon
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    var planDetails = new
    {
        PlanName = "Test Plan",
        PlanPrice = 10,
        PlanDescription = "Test plan description",
        PlanType = "prepaid",
        PlanValidity = "30",
        PlanOffer = "Test Offer"
    };

    string planRequestBody = JsonConvert.SerializeObject(planDetails);
    HttpResponseMessage planResponse = await _httpClient.PostAsync("api/addPlan", new StringContent(planRequestBody, Encoding.UTF8, "application/json"));
    
    // Assert that the plan is added successfully
    Assert.AreEqual(HttpStatusCode.OK, planResponse.StatusCode);

    // Use the obtained token in the request to get plans
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    // Make a request to get all plans
    HttpResponseMessage getPlansResponse = await _httpClient.GetAsync("api/getAllPlan");
    Assert.AreEqual(HttpStatusCode.OK, getPlansResponse.StatusCode);

    // Validate the response content (assuming the response is a JSON array of plans)
    string getPlansResponseBody = await getPlansResponse.Content.ReadAsStringAsync();
    var plans = JsonConvert.DeserializeObject<List<Plan>>(getPlansResponseBody);
    Assert.IsNotNull(plans);
    Assert.IsTrue(plans.Any());
}


// [Test]
// public async Task Backend_TestGetRecharge()
// {
//     // Generate unique identifiers
//     string uniqueId = Guid.NewGuid().ToString();
//     string uniqueusername = $"abcd_{uniqueId}";
//     string uniquepassword = $"abcdA{uniqueId}@123";
//     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

//     // Register a customer
//     string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"admin\" }}";
//     HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

//     // Login the registered customer
//     string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
//     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
//     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
//     dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
//     string customerAuthToken = loginResponseMap.token;

//     // Use the obtained token in the request to add an addon
//     _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

//     var rechargeDetails = new
//     {
//         Price = 20,
//         RechargeDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
//         ValidityDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-ddTHH:mm:ss"),
//         UserId = 1, // Replace with an existing user ID
//         PlanId = 1, // Replace with an existing plan ID
//     };

//     string rechargeRequestBody = JsonConvert.SerializeObject(rechargeDetails);
//     HttpResponseMessage rechargeResponse = await _httpClient.PostAsync("api/addRecharge", new StringContent(rechargeRequestBody, Encoding.UTF8, "application/json"));

//     // Assert that the recharge is added successfully
//     Assert.AreEqual(HttpStatusCode.OK, rechargeResponse.StatusCode);
// }

[Test]
public async Task Backend_TestGetAllReviews()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"admin\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to add an addon
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;
 
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);
 
        HttpResponseMessage getReviewsResponse = await _httpClient.GetAsync("/api");
       
        Assert.AreEqual(HttpStatusCode.OK, getReviewsResponse.StatusCode);
}

[Test]
public async Task Backend_TestPostReviews()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"customer\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to add a review
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    var reviewDetails = new
    {
        ReviewId = 0,
        UserId = 1,
        Subject = "Test Subject",
        Body = "Test Body",
        Rating = 4,
        DateCreated = DateTime.Now,
        User = new
        {
            UserId = 0,
            Email = "string",
            Password = "string",
            Username = "string",
            MobileNumber = "string",
            Role = "string"
        }
    };

    string reviewRequestBody = JsonConvert.SerializeObject(reviewDetails);
    HttpResponseMessage addReviewResponse = await _httpClient.PostAsync("/api", new StringContent(reviewRequestBody, Encoding.UTF8, "application/json"));

    // Assert that the review is added successfully
    if (addReviewResponse.StatusCode != HttpStatusCode.OK)
    {
        // Additional information about the response
        string responseContent = await addReviewResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Content: {responseContent}");
    }

    Assert.AreEqual(HttpStatusCode.OK, addReviewResponse.StatusCode);
}

[Test]
public async Task Backend_TestAddRecharge()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"customer\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to add a recharge
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    var rechargeDetails = new
    {
        RechargeId = 0,
        Price = 20,
        RechargeDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
        ValidityDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
        UserId = 1, // Assuming the user is registered with ID 1
        PlanId = 1, // Assuming the plan is already registered with ID 1
        User = new
        {
            UserId = 0,
            Email = "string",
            Password = "string",
            Username = "string",
            MobileNumber = "string",
            Role = "string"
        },
        Plan = new
        {
            PlanId = 0,
            PlanType = "string",
            PlanName = "string",
            PlanValidity = "string",
            PlanOffer = "string",
            PlanDescription = "string",
            PlanPrice = 0
        }
    };


    string rechargeRequestBody = JsonConvert.SerializeObject(rechargeDetails);
    HttpResponseMessage rechargeResponse = await _httpClient.PostAsync("/api/addRecharge", new StringContent(rechargeRequestBody, Encoding.UTF8, "application/json"));

    // Assert that the recharge is added successfully
    Assert.AreEqual(HttpStatusCode.OK, rechargeResponse.StatusCode);
}

[Test]
public async Task Backend_TestGetRechargeById()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"customer\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to add a recharge
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    // Assuming you have a rechargeId from a previous recharge
    long rechargeId = 1; // Replace with the actual rechargeId

    HttpResponseMessage getRechargeResponse = await _httpClient.GetAsync($"/api/getRecharge/{rechargeId}");

    // Assert that the response is successful
    Assert.AreEqual(HttpStatusCode.OK, getRechargeResponse.StatusCode);
}

[Test]
public async Task Backend_TestGetRechargesByUserId()
{
    // Generate unique identifiers
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueusername = $"abcd_{uniqueId}";
    string uniquepassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a customer
    string registerRequestBody = $"{{\"Username\": \"{uniqueusername}\", \"Password\": \"{uniquepassword}\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\",\"Role\" : \"customer\" }}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered customer
    string loginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquepassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string customerAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to get recharges by user ID
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    // Assuming there is a user with ID 1
    long userId = 1;

    HttpResponseMessage getRechargesResponse = await _httpClient.GetAsync($"/api/getRechargesByUser/{userId}");

    // Assert that the response is successful
    Assert.AreEqual(HttpStatusCode.OK, getRechargesResponse.StatusCode);

    // Additional assertions or validations based on your specific requirements
}

}