using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;
using Newtonsoft.Json.Linq;


[TestFixture]
public class Tests
{
    private HttpClient _httpClient;
    private string _generatedToken;
    [SetUp]
    public void Setup()
    {
         _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:8080"); 

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
    public async Task Backend_TestAddGiftAdmin()
    {
       string uniqueId = Guid.NewGuid().ToString();

       // Use a dynamic and unique userName for admin (appending timestamp)
       string uniqueusername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";


       // Assume you have a valid admin registration method, adjust the request body accordingly
       string adminRegistrationRequestBody = $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueusername}\",\"role\": \"admin\",\"email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\"}}";
       HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

       Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

       // Now, perform the login for the admin user
       string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
       HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

       Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
       string responseBody = await loginResponse.Content.ReadAsStringAsync();

       dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

       string token = responseMap.token;

       Assert.IsNotNull(token);

       string uniquetitle = Guid.NewGuid().ToString();

       // Use a dynamic and unique userName for admin (appending timestamp)
       string uniqueGiftTitle = $"giftTitle_{uniquetitle}";

       string giftJson = $"{{\"GiftType\":\"{uniqueGiftTitle}\",\"GiftImageUrl\":\"test\",\"GiftDetails\":\"test\",\"GiftPrice\":10,\"Quantity\":1}}";
       _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
       HttpResponseMessage response = await _httpClient.PostAsync("/api/gift",
           new StringContent(giftJson, Encoding.UTF8, "application/json"));

       Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test, Order(6)]
    public async Task Backend_TestGetAllGiftsForAdminAndCustomer()
    {
       string uniqueId = Guid.NewGuid().ToString();

       // Use a dynamic and unique userName for admin (appending timestamp)
       string uniqueusername = $"admin_{uniqueId}";
       string uniqueEmail = $"abcd{uniqueId}@gmail.com";


       // Assume you have a valid admin registration method, adjust the request body accordingly
       string adminRegistrationRequestBody =  $"{{\"password\": \"abc@123A\", \"userName\": \"{uniqueusername}\",\"role\": \"admin\",\"email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\"}}";
       HttpResponseMessage registrationResponse = await _httpClient.PostAsync("api/register", new StringContent(adminRegistrationRequestBody, Encoding.UTF8, "application/json"));

       Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

       // Now, perform the login for the admin user
       string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"abc@123A\"}}";
       HttpResponseMessage loginResponse = await _httpClient.PostAsync("api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));

       Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
       string responseBody = await loginResponse.Content.ReadAsStringAsync();

       dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

       string token = responseMap.token;

       Assert.IsNotNull(token);


       Console.WriteLine("admin111" + token);
       _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

       HttpResponseMessage response = await _httpClient.GetAsync("/api/gift");

       Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test, Order(7)]
    public async Task Backend_TestPostReview()
    {
        HttpResponseMessage response = null;

        // Register a new customer and obtain the authentication token
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        // Register a new customer
        string registerRequestBody = $"{{\"password\": \"{uniquePassword}\", \"userName\": \"{uniqueUsername}\",\"role\": \"customer\",\"email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Log in the registered customer and obtain the authentication token
        string loginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string customerAuthToken = responseMap.token;

        // Set the authentication token in the HTTP client headers
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

        // Create a review object
        var review = new
        {
            Subject = "Test subject",
            Body = "Test body",
            Rating = 5
        };

    try
        {
            string requestBody = JsonConvert.SerializeObject(review);
            response = await _httpClient.PostAsync("/api/review", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Print response content for debugging purposes
            Console.WriteLine($"Response Content: {await response.Content.ReadAsStringAsync()}");

        // Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Additional assertions based on the properties of the posted review
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");

            if (response != null)
            {
                // Print response content for debugging purposes
                Console.WriteLine($"Response Content: {await response.Content.ReadAsStringAsync()}");
            }

            throw;
        }
    }

    [Test, Order(8)]
    public async Task Backend_TestGetAllReviews()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string RegisterrequestBody = $"{{\"password\": \"{uniquePassword}\", \"userName\": \"{uniqueUsername}\",\"role\": \"admin\",\"email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        var adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        HttpResponseMessage getReviewsResponse = await _httpClient.GetAsync("/api/review");
    
        Assert.AreEqual(HttpStatusCode.OK, getReviewsResponse.StatusCode);
    }
 

    [Test, Order(9)]
    public async Task Backend_TestEditGift()
    {
        HttpResponseMessage response = null;

        // Register a new admin and obtain the authentication token
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"admin_{uniqueId}";
        string uniquePassword = $"adminA{uniqueId}@123";
        string uniqueEmail = $"admin{uniqueId}@gmail.com";

        // Register a new admin
        string registerRequestBody = $"{{\"password\": \"{uniquePassword}\", \"userName\": \"{uniqueUsername}\",\"role\": \"admin\",\"email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Log in the registered admin and obtain the authentication token
        string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        // Set the authentication token in the HTTP client headers
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        // Create a new gift to be updated
        var giftToAdd = new
        {
            GiftType = "Test Gift",
            GiftImageUrl = "test_image.jpg",
            GiftDetails = "Original details",
            GiftPrice = 20.0,
            Quantity = 5
        };

        string addGiftRequestBody = JsonConvert.SerializeObject(giftToAdd);
        HttpResponseMessage addGiftResponse = await _httpClient.PostAsync("/api/gift", new StringContent(addGiftRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, addGiftResponse.StatusCode);

        // Retrieve the added gift details
        string addedGiftResponseString = await addGiftResponse.Content.ReadAsStringAsync();
        dynamic addedGiftResponseMap = JsonConvert.DeserializeObject(addedGiftResponseString);
        long giftId = addedGiftResponseMap.giftId;

        // Create updated gift details
        var updatedGift = new
        {
            GiftType = "Updated Gift",
            GiftImageUrl = "updated_image.jpg",
            GiftDetails = "Updated details",
            GiftPrice = 25.0,
            Quantity = 8
        };

        string updateGiftRequestBody = JsonConvert.SerializeObject(updatedGift);
        HttpResponseMessage updateGiftResponse = await _httpClient.PutAsync($"/api/gift/{giftId}", new StringContent(updateGiftRequestBody, Encoding.UTF8, "application/json"));

        // Check if the response is OK (200)
        Assert.AreEqual(HttpStatusCode.OK, updateGiftResponse.StatusCode);

    }

    [Test, Order(10)]
    public async Task Backend_TestDeleteGift()
    {
        HttpResponseMessage response = null;

        // Register a new admin and obtain the authentication token
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"admin_{uniqueId}";
        string uniquePassword = $"adminA{uniqueId}@123";
        string uniqueEmail = $"admin{uniqueId}@gmail.com";

        // Register a new admin
        string registerRequestBody = $"{{\"password\": \"{uniquePassword}\", \"userName\": \"{uniqueUsername}\",\"role\": \"admin\",\"email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Log in the registered admin and obtain the authentication token
        string adminLoginRequestBody = $"{{\"email\": \"{uniqueEmail}\",\"password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        // Set the authentication token in the HTTP client headers
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        // Create a new gift to be deleted
        var giftToAdd = new
        {
            GiftType = "Test Gift",
            GiftImageUrl = "test_image.jpg",
            GiftDetails = "Original details",
            GiftPrice = 20.0,
            Quantity = 5
        };

        string addGiftRequestBody = JsonConvert.SerializeObject(giftToAdd);
        HttpResponseMessage addGiftResponse = await _httpClient.PostAsync("/api/gift", new StringContent(addGiftRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, addGiftResponse.StatusCode);

        // Retrieve the added gift details
        string addedGiftResponseString = await addGiftResponse.Content.ReadAsStringAsync();
        dynamic addedGiftResponseMap = JsonConvert.DeserializeObject(addedGiftResponseString);
        long giftId = addedGiftResponseMap.giftId;

        // Attempt to delete the gift
        HttpResponseMessage deleteGiftResponse = await _httpClient.DeleteAsync($"/api/gift/{giftId}");

        // Check if the response is OK (200)
        Assert.AreEqual(HttpStatusCode.OK, deleteGiftResponse.StatusCode);

    }
    
    [Test]
public async Task Backend_TestDeleteReview()
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
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    
    // Add a review to delete
    var initialReviewDetails = new
    {
        UserId = 1,
        Subject = "Delete Subject",
        Body = "Delete Body",
        Rating = 5,
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

    string initialReviewRequestBody = JsonConvert.SerializeObject(initialReviewDetails);
    HttpResponseMessage addReviewResponse = await _httpClient.PostAsync("/api/review", new StringContent(initialReviewRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, addReviewResponse.StatusCode);

    // Get the added review details
    string addReviewResponseBody = await addReviewResponse.Content.ReadAsStringAsync();
    dynamic addReviewResponseMap = JsonConvert.DeserializeObject(addReviewResponseBody);

    // Extract the reviewId for deletion
    int? reviewId = addReviewResponseMap?.reviewId;

    if (reviewId.HasValue)
    {
        // Delete the review
        HttpResponseMessage deleteReviewResponse = await _httpClient.DeleteAsync($"/api/review/{reviewId}");

        // Assert that the review is deleted successfully
        if (deleteReviewResponse.StatusCode != HttpStatusCode.OK)
        {
            // Additional information about the response
            string responseContent = await deleteReviewResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {responseContent}");
        }

        Assert.AreEqual(HttpStatusCode.OK, deleteReviewResponse.StatusCode);
    }
    else
    {
        // Log additional information for debugging
        string responseContent = await addReviewResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Add Review Response Content: {responseContent}");

        Assert.Fail("Review ID is null or not found in the response.");
    }
}

[Test]
public async Task Backend_TestPutReviews()
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
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    
    // Add a review to update
    var initialReviewDetails = new
    {
        UserId = 1,
        Subject = "Initial Subject",
        Body = "Initial Body",
        Rating = 3,
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

    string initialReviewRequestBody = JsonConvert.SerializeObject(initialReviewDetails);
    HttpResponseMessage addReviewResponse = await _httpClient.PostAsync("/api/review", new StringContent(initialReviewRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, addReviewResponse.StatusCode);

    // Get the added review details
    string addReviewResponseBody = await addReviewResponse.Content.ReadAsStringAsync();
    dynamic addReviewResponseMap = JsonConvert.DeserializeObject(addReviewResponseBody);

    // Handle the potential null value for the review ID
    int? reviewId = addReviewResponseMap?.reviewId;

    if (reviewId.HasValue)
    {
        // Update the review with the correct reviewId
        var updatedReviewDetails = new
        {
            ReviewId = reviewId,
            UserId = 1,
            Subject = "Updated Subject",
            Body = "Updated Body",
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

        string updateReviewRequestBody = JsonConvert.SerializeObject(updatedReviewDetails);
        HttpResponseMessage updateReviewResponse = await _httpClient.PutAsync($"/api/review/{reviewId}", new StringContent(updateReviewRequestBody, Encoding.UTF8, "application/json"));

        // Assert that the review is updated successfully
        if (updateReviewResponse.StatusCode != HttpStatusCode.OK)
        {
            // Additional information about the response
            string responseContent = await updateReviewResponse.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {responseContent}");
        }

        Assert.AreEqual(HttpStatusCode.OK, updateReviewResponse.StatusCode);
    }
    else
    {
        // Log additional information for debugging
        string responseContent = await addReviewResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Add Review Response Content: {responseContent}");

        Assert.Fail("Review ID is null or not found in the response.");
    }
}


[Test]
public async Task Backend_TestGetAllOrders()
{
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniquePassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    // Register a new admin and obtain the authentication token
    string registerRequestBody = $"{{\"password\": \"{uniquePassword}\", \"userName\": \"{uniqueUsername}\",\"role\": \"admin\",\"email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    // Log in the registered admin and obtain the authentication token
    string adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    string responseString = await loginResponse.Content.ReadAsStringAsync();
    dynamic responseMap = JsonConvert.DeserializeObject(responseString);
    string adminAuthToken = responseMap.token;

    // Set the authentication token in the HTTP client headers
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

    // Send GET request to retrieve all orders
    HttpResponseMessage getOrdersResponse = await _httpClient.GetAsync("/api/orders");

    // Assert the response status code
    Assert.AreEqual(HttpStatusCode.OK, getOrdersResponse.StatusCode);
}

    [TearDown]
    public void TearDown()
    {
        // Cleanup or additional teardown logic if needed.
        _httpClient.Dispose();
    }
}