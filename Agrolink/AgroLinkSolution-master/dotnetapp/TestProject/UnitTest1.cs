using dotnetapp.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace TestProject
{
    [TestFixture]
    public class Tests
    {
        private ApplicationDbContext _context;
        private HttpClient _httpClient;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            _context = new ApplicationDbContext(options);

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080");
        }
        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        //User testing
        [Test, Order(1)]
        public async Task Backend_Test_Post_Method_Register_Farmer()
        {
            ClearDatabase();
            string uniqueId = Guid.NewGuid().ToString();

            // Generate a unique userName based on a timestamp
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string requestBody = $"{{\"UserName\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"Farmer\",\"ProfileImage\":\"farmerimage1\"}}";
            HttpResponseMessage response = await _httpClient.PostAsync("/user/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            Console.WriteLine(response.StatusCode);
            string responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test, Order(2)]
        public async Task Backend_Test_Post_Method_Login_Farmer()
        {
            ClearDatabase();

            string uniqueId = Guid.NewGuid().ToString();

            // Generate a unique userName based on a timestamp
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string requestBody = $"{{\"UserName\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"Farmer\",\"ProfileImage\":\"farmerimage1\"}}";
            HttpResponseMessage response = await _httpClient.PostAsync("/user/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Print registration response
            string registerResponseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Registration Response: " + registerResponseBody);

            // Login with the registered user
            string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
            HttpResponseMessage loginResponse = await _httpClient.PostAsync("/user/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

            // Print login response
            string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Login Response: " + loginResponseBody);

            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
        }
        [Test, Order(3)]
        public async Task Backend_Test_Post_Method_Register_Seller()
        {
            ClearDatabase();
            string uniqueId = Guid.NewGuid().ToString();

            // Generate a unique userName based on a timestamp
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string requestBody = $"{{\"UserName\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"Seller\",\"ProfileImage\":\"sellerimage1\"}}";
            HttpResponseMessage response = await _httpClient.PostAsync("/user/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            Console.WriteLine(response.StatusCode);
            string responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }
        [Test, Order(4)]
        public async Task Backend_Test_Post_Method_Login_Seller()
        {
            ClearDatabase();

            string uniqueId = Guid.NewGuid().ToString();

            // Generate a unique userName based on a timestamp
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string requestBody = $"{{\"UserName\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"Seller\",\"ProfileImage\":\"sellerimage1\"}}";
            HttpResponseMessage response = await _httpClient.PostAsync("/user/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Print registration response
            string registerResponseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Registration Response: " + registerResponseBody);

            // Login with the registered user
            string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
            HttpResponseMessage loginResponse = await _httpClient.PostAsync("/user/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

            // Print login response
            string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Login Response: " + loginResponseBody);

            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
        }

        // Crop testing
        [Test, Order(5)]
        public async Task Backend_Test_Post_Crop_By_Farmer()
        {
            ClearDatabase();
            string uniqueId = Guid.NewGuid().ToString();

            // Generate a unique userName based on a timestamp
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string requestBody = $"{{\"UserName\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"Farmer\",\"ProfileImage\":\"farmerimage1\"}}";
            HttpResponseMessage response = await _httpClient.PostAsync("/user/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Print registration response
            string registerResponseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Registration Response: " + registerResponseBody);

            // Login with the registered user
            string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
            HttpResponseMessage loginResponse = await _httpClient.PostAsync("/user/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

            // Print login response
            string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Login Response: " + loginResponseBody);

            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

            string token = responseMap.token;

            Assert.IsNotNull(token);

            string uniquetitle = Guid.NewGuid().ToString();

            // Use a dynamic and unique userName for admin (appending timestamp)
            string uniquecropname = $"loan_{uniquetitle}";

            string loanjson = $"{{\"CropName\":\"{uniquecropname}\",\"CropType\":\"Rice\",\"Description\":\"cropforrice,\"PlantingDate\"\"{new DateTime()}}}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage loanresponse = await _httpClient.PostAsync("/crop/addcrop",
            new StringContent(loanjson, Encoding.UTF8, "application/json"));

            Console.WriteLine("loanresponse" + loanresponse);

            Assert.AreEqual(HttpStatusCode.OK, loanresponse.StatusCode);
        }

        [Test, Order(6)]
        public async Task Backend_Test_Post_crop_By_Seller_Returns_Forbidden()
        {
            ClearDatabase();
            string uniqueId = Guid.NewGuid().ToString();
            // Generate a unique userName based on a timestamp
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string requestBody = $"{{\"UserName\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"Farmer\",\"ProfileImage\":\"farmerimage1\"}}";
            HttpResponseMessage response = await _httpClient.PostAsync("/user/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Print registration response
            string registerResponseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Registration Response: " + registerResponseBody);

            // Login with the registered user
            string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
            HttpResponseMessage loginResponse = await _httpClient.PostAsync("/user/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

            // Print login response
            string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Login Response: " + loginResponseBody);

            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

            string token = responseMap.token;

            Assert.IsNotNull(token);

            string uniquetitle = Guid.NewGuid().ToString();

            // Use a dynamic and unique userName for admin (appending timestamp)
            string uniqueloantype = $"loan_{uniquetitle}";

            string loanjson = $"{{\"CropName\":\"{uniqueloantype}\",\"Description\":\"cropforrice,\"PlantingDate\"\"{new DateTime()}}}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage loanresponse = await _httpClient.PostAsync("/crop/addcrop",
            new StringContent(loanjson, Encoding.UTF8, "application/json"));

            Console.WriteLine("loanresponse" + loanresponse);

            Assert.AreEqual(HttpStatusCode.Forbidden, loanresponse.StatusCode);
        }
        [Test, Order(7)]

        public async Task Backend_Test_Post_Crop_Without_Token_By_Farmer_Returns_Unauthorize()
        {
            ClearDatabase();
            string uniqueId = Guid.NewGuid().ToString();

            // Generate a unique userName based on a timestamp
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"Farmer\",\"ProfileImage\":\"farmerimage1\"}}";
            HttpResponseMessage response = await _httpClient.PostAsync("/user/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Print registration response
            string registerResponseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Registration Response: " + registerResponseBody);

            // Login with the registered user
            string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
            HttpResponseMessage loginResponse = await _httpClient.PostAsync("/user/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

            // Print login response
            string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Login Response: " + loginResponseBody);

            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

            string uniquetitle = Guid.NewGuid().ToString();

            // Use a dynamic and unique userName for admin (appending timestamp)
            string uniqueloantype = $"loan_{uniquetitle}";

            string loanjson = $"{{\"LoanType\":\"{uniqueloantype}\",\"Description\":\"test\",\"InterestRate\":10,\"MaximumAmount\":1000}}";
            HttpResponseMessage loanresponse = await _httpClient.PostAsync("/crop/addcrop",
            new StringContent(loanjson, Encoding.UTF8, "application/json"));

            Console.WriteLine("loanresponse" + loanresponse);

            Assert.AreEqual(HttpStatusCode.Unauthorized, loanresponse.StatusCode);
        }

        [Test, Order(8)]
        public async Task Backend_Test_Get_Method_Get_all_Crops_By_Farmer()
        {
            ClearDatabase();

            string uniqueId = Guid.NewGuid().ToString();

            // Generate a unique userName based on a timestamp
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"Farmer\",\"ProfileImage\":\"farmerimage1\"}}";
            HttpResponseMessage response = await _httpClient.PostAsync("/user/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Print registration response
            string registerResponseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Registration Response: " + registerResponseBody);

            // Login with the registered user
            string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
            HttpResponseMessage loginResponse = await _httpClient.PostAsync("/user/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

            // Print login response
            string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Login Response: " + loginResponseBody);

            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

            string token = responseMap.token;

            Assert.IsNotNull(token);

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage loanresponse = await _httpClient.GetAsync("/crop/GetAllCrop");
            Assert.AreEqual(HttpStatusCode.OK, loanresponse.StatusCode);
        }

        [Test, Order(9)]
        public async Task Backend_Test_Post_AgroChemical_By_seller()
        {
            ClearDatabase();
            string uniqueId = Guid.NewGuid().ToString();

            // Generate a unique userName based on a timestamp
            string uniqueUsername = $"abcd_{uniqueId}";
            string uniqueEmail = $"abcd{uniqueId}@gmail.com";

            string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"Role\": \"Seller\",\"ProfileImage\":\"farmerimage1\"}}";
            HttpResponseMessage response = await _httpClient.PostAsync("/user/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            // Print registration response
            string registerResponseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Registration Response: " + registerResponseBody);

            // Login with the registered user
            string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
            HttpResponseMessage loginResponse = await _httpClient.PostAsync("/user/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

            // Print login response
            string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Login Response: " + loginResponseBody);

            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
            string responseBody = await loginResponse.Content.ReadAsStringAsync();

            dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

            string token = responseMap.token;

            Assert.IsNotNull(token);

            string uniquetitle = Guid.NewGuid().ToString();

            // Use a dynamic and unique userName for admin (appending timestamp)
            string uniquecropname = $"loan_{uniquetitle}";

            string agrochemical = $"{{\"Name\":\"{uniquecropname}\",\"Brand\":\"mybrand\",\"Category\":\"rice,\"PlantingDate\"\"{new DateTime()}}}";

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage loanresponse = await _httpClient.PostAsync("/crop/addcrop",
            new StringContent(agrochemical, Encoding.UTF8, "application/json"));

            Console.WriteLine("loanresponse" + loanresponse);

            Assert.AreEqual(HttpStatusCode.OK, loanresponse.StatusCode);
        }
        private void ClearDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

    }
}