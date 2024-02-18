using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;

[TestFixture]
public class SpringappApplicationTests
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io");
    }

    [Test, Order(1)]
    public async Task Backend_TestRegisterAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test, Order(2)]
    public async Task Backend_TestLoginAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        // Print registration response
        string registerResponseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Registration Response: " + registerResponseBody);

        // Login with the registered user
        string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
        
        // Print login response
        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        Console.WriteLine("Login Response: " + loginResponseBody);

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }

    [Test, Order(3)]
    public async Task Backend_TestRegisterUser()
    {
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Operator\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test, Order(4)]
    public async Task Backend_TestLoginUser()
    {
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Operator\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        // Print registration response
        string registerResponseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Registration Response: " + registerResponseBody);

        // Login with the registered user
        string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
        
        // Print login response
        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        Console.WriteLine("Login Response: " + loginResponseBody);

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }

    [Test]
public async Task Backend_TestGetContainer()
{
    // Register a user with the "Admin" role
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string userAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to post a new container
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

    // Post a new container
   var newContainer = new Container
    {
        ContainerId = 0, // Set the desired ContainerId
        Type = "string",
        Status = "string",
        Capacity = 100,
        Location = "string",
        Weight = 50.5,
        Owner = "string",
        CreationDate = DateTime.UtcNow,
        LastInspectionDate = DateTime.UtcNow
    };

    string postContainerBody = JsonConvert.SerializeObject(newContainer);
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

    // Retrieve the posted container
    HttpResponseMessage getContainerResponse = await _httpClient.GetAsync("/api/container");
    Assert.AreEqual(HttpStatusCode.OK, getContainerResponse.StatusCode);

    // Validate the response content
    string getContainerResponseBody = await getContainerResponse.Content.ReadAsStringAsync();
    Console.WriteLine($"Response Body: {getContainerResponseBody}");

    var containers = JsonConvert.DeserializeObject<List<Container>>(getContainerResponseBody);

    // Console log to inspect containers
    foreach (var container in containers)
    {
        Console.WriteLine($"ContainerId: {container.ContainerId}, Type: {container.Type}, Status: {container.Status}");
    }

    // Assert that containers are not null and there is at least one container
    Assert.IsNotNull(containers);
    Assert.IsTrue(containers.Any()); // This ensures that there is at least one container


}

[Test]
public async Task Backend_TestPostContainer()
{
    // Register a user with the "Admin" role
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string userAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to post a new container
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

    // Declare and initialize newContainer
    var newContainer = new Container
    {
        ContainerId = 0, // Set the desired ContainerId
        Type = "string",
        Status = "string",
        Capacity = 100,
        Location = "string",
        Weight = 50.5,
        Owner = "string",
        CreationDate = DateTime.UtcNow,
        LastInspectionDate = DateTime.UtcNow
    };

    // POST operation
    string postContainerBody = JsonConvert.SerializeObject(newContainer);
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);
}

[Test]
public async Task Backend_TestPutContainer()
{
    // Register a user with the "Admin" role
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string userAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to post a new container
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

    // POST: Create a new container
    var newContainer = new Container
    {
        ContainerId = 0, // Set the desired ContainerId
        Type = "string",
        Status = "string",
        Capacity = 100,
        Location = "string",
        Weight = 50.5,
        Owner = "string",
        CreationDate = DateTime.UtcNow,
        LastInspectionDate = DateTime.UtcNow
    };

    string postContainerBody = JsonConvert.SerializeObject(newContainer);
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

    // Extract the created container from the response
    var createdContainer = JsonConvert.DeserializeObject<Container>(await postContainerResponse.Content.ReadAsStringAsync());
    long containerId = createdContainer.ContainerId;

    // PUT: Update the created container
    newContainer.Type = "UpdatedType";
    newContainer.Status = "UpdatedStatus";

    string putContainerBody = JsonConvert.SerializeObject(newContainer);
    HttpResponseMessage putContainerResponse = await _httpClient.PutAsync($"/api/container/{containerId}", new StringContent(putContainerBody, Encoding.UTF8, "application/json"));
    if (putContainerResponse.StatusCode == HttpStatusCode.OK)
    {
        // Container updated successfully
        Console.WriteLine("Container updated successfully");
    }
    else if (putContainerResponse.StatusCode == HttpStatusCode.NotFound)
    {
        // Cannot find the container
        Console.WriteLine("Cannot find the container");
    }
    else
    {
        // Handle other status codes if needed
        Console.WriteLine($"Unexpected status code: {putContainerResponse.StatusCode}");
    }

    // GET: Retrieve the updated container
    HttpResponseMessage getContainerResponse = await _httpClient.GetAsync("/api/container");
    Assert.AreEqual(HttpStatusCode.OK, getContainerResponse.StatusCode);

    // Validate the response content
    string getContainerResponseBody = await getContainerResponse.Content.ReadAsStringAsync();
    // Console.WriteLine($"Response Body: {getContainerResponseBody}");

    var containers = JsonConvert.DeserializeObject<List<Container>>(getContainerResponseBody);

    //Console log to inspect containers
    foreach (var container in containers)
    {
        Console.WriteLine($"ContainerId: {container.ContainerId}, Type: {container.Type}, Status: {container.Status}");
    }

    // Assert that containers are not null and there is at least one container
    Assert.IsNotNull(containers);
    Assert.IsTrue(containers.Any()); // This ensures that there is at least one container
}

[Test]
public async Task Backend_TestDeleteContainer()
{
    // Register a user with the "Admin" role
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string userAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to post a new container
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

    // POST: Create a new container
    var newContainer = new Container
    {
        ContainerId = 0, // Set the desired ContainerId
        Type = "string",
        Status = "string",
        Capacity = 100,
        Location = "string",
        Weight = 50.5,
        Owner = "string",
        CreationDate = DateTime.UtcNow,
        LastInspectionDate = DateTime.UtcNow
    };

    string postContainerBody = JsonConvert.SerializeObject(newContainer);
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

    // Extract the created container from the response
    var createdContainer = JsonConvert.DeserializeObject<Container>(await postContainerResponse.Content.ReadAsStringAsync());
    long containerId = createdContainer.ContainerId;

    // DELETE: Delete the created container
    HttpResponseMessage deleteContainerResponse = await _httpClient.DeleteAsync($"/api/container/{containerId}");
    if (deleteContainerResponse.StatusCode == HttpStatusCode.OK)
    {
        // Container deleted successfully
        Console.WriteLine("Container deleted successfully");
    }
    else if (deleteContainerResponse.StatusCode == HttpStatusCode.NotFound)
    {
        // Cannot find the container
        Console.WriteLine("Cannot find the container");
    }
    else
    {
        // Handle other status codes if needed
        Console.WriteLine($"Unexpected status code: {deleteContainerResponse.StatusCode}");
    }

    // GET: Retrieve the containers
    HttpResponseMessage getContainerResponse = await _httpClient.GetAsync("/api/container");
    Assert.AreEqual(HttpStatusCode.OK, getContainerResponse.StatusCode);

    // Validate the response content
    string getContainerResponseBody = await getContainerResponse.Content.ReadAsStringAsync();
    Console.WriteLine($"Response Body: {getContainerResponseBody}");

    var containers = JsonConvert.DeserializeObject<List<Container>>(getContainerResponseBody);

    // Console log to inspect containers
    foreach (var container in containers)
    {
        Console.WriteLine($"ContainerId: {container.ContainerId}, Type: {container.Type}, Status: {container.Status}");
    }

    // Assert that containers are not null
    Assert.IsNotNull(containers);
}

[Test]
public async Task Backend_TestPostAssignmentAsAdmin()
{
    // Register a user with the "Admin" role
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

    // Login the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string userAuthToken = loginResponseMap.token;

    // Use the obtained token in the request to post a new assignment
    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

    // Create a new assignment payload
    var newAssignment = new
    {
        AssignmentId = 0,
        ContainerId = 0,
        UserId = 0,
        Status = "string",
        UpdateTime = DateTime.UtcNow,
        Route = "string",
        Shipment = "string",
        Destination = "string",
        Issue = new
        {
            IssueId = 0,
            Description = "string",
            Severity = "string",
            ReportedDate = DateTime.UtcNow,
            UserId = 0,
            AssignmentId = 0,
            User = new
            {
                UserId = 0,
                Email = "string",
                Password = "string",
                Username = "string",
                MobileNumber = "string",
                UserRole = "string"
            },
            Assignment = "string"
        },
        Container = new
        {
            ContainerId = 0,
            Type = "string",
            Status = "string",
            Capacity = 0,
            Location = "string",
            Weight = 0,
            Owner = "string",
            CreationDate = DateTime.UtcNow,
            LastInspectionDate = DateTime.UtcNow
        },
        User = new
        {
            UserId = 0,
            Email = "string",
            Password = "string",
            Username = "string",
            MobileNumber = "string",
            UserRole = "string"
        }
    };

    // Convert the newAssignment object to JSON string
    string postAssignmentBody = JsonConvert.SerializeObject(newAssignment);

    // POST: Create a new assignment
    HttpResponseMessage postAssignmentResponse = await _httpClient.PostAsync("/api/assignment", new StringContent(postAssignmentBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, postAssignmentResponse.StatusCode);

    // Validate the response content
    string postAssignmentResponseBody = await postAssignmentResponse.Content.ReadAsStringAsync();
    Console.WriteLine($"Response Body: {postAssignmentResponseBody}");

    // Extract the created assignment from the response
    var createdAssignment = JsonConvert.DeserializeObject<dynamic>(postAssignmentResponseBody);

    // Assert that the assignment is not null
    Assert.IsNotNull(createdAssignment);

    // Add additional assertions as needed for the response content
    // ...

    // You may also want to test retrieving the assignment by its ID
    // This depends on your API design and available endpoints
}


}
