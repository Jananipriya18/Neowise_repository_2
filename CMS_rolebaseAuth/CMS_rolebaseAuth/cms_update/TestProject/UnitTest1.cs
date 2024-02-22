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
        _httpClient.BaseAddress = new Uri("https://8080-aabdbffdadabafcfdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/");
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
        // LastInspectionDate = DateTime.UtcNow
    };

    string postContainerBody = JsonConvert.SerializeObject(newContainer);
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

    // Retrieve the posted container
    HttpResponseMessage getContainerResponse = await _httpClient.GetAsync("/api/container");
    Assert.AreEqual(HttpStatusCode.OK, getContainerResponse.StatusCode);

    // Validate the response content
    string getContainerResponseBody = await getContainerResponse.Content.ReadAsStringAsync();
    // Console.WriteLine($"Response Body: {getContainerResponseBody}");

    var containers = JsonConvert.DeserializeObject<List<Container>>(getContainerResponseBody);

    // Console log to inspect containers
    // foreach (var container in containers)
    // {
    //     Console.WriteLine($"ContainerId: {container.ContainerId}, Type: {container.Type}, Status: {container.Status}");
    // }

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
        // LastInspectionDate = DateTime.UtcNow
    };

    // POST operation
    string postContainerBody = JsonConvert.SerializeObject(newContainer);
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);
}

// [Test]
// public async Task Backend_TestPutContainer()
// {
//     // Register a user with the "Admin" role
//     string uniqueId = Guid.NewGuid().ToString();
//     string uniqueUsername = $"abcd_{uniqueId}";
//     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

//     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
//     HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

//     // Login the registered user
//     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}";
//     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
//     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
//     dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
//     string userAuthToken = loginResponseMap.token;

//     // Use the obtained token in the request to post a new container
//     _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

//     // POST: Create a new container
//     var newContainer = new Container
//     {
//         ContainerId = 0, 
//         Type = "string",
//         Status = "string",
//         Capacity = 100,
//         Location = "string",
//         Weight = 50.5,
//         Owner = "string",
//         CreationDate = DateTime.UtcNow,
//         LastInspectionDate = DateTime.UtcNow
//     };

//     string postContainerBody = JsonConvert.SerializeObject(newContainer);
//     HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

//     // Extract the created container from the response
//     var createdContainer = JsonConvert.DeserializeObject<Container>(await postContainerResponse.Content.ReadAsStringAsync());
//     long containerId = createdContainer.ContainerId;

//     // PUT: Update the created container
//     newContainer.Type = "UpdatedType";
//     newContainer.Status = "UpdatedStatus";

//     string putContainerBody = JsonConvert.SerializeObject(newContainer);
//     HttpResponseMessage putContainerResponse = await _httpClient.PutAsync($"/api/container/{containerId}", new StringContent(putContainerBody, Encoding.UTF8, "application/json"));
//     // if (putContainerResponse.StatusCode == HttpStatusCode.OK)
//     // {
//     //     // Container updated successfully
//     //     Console.WriteLine("Container updated successfully");
//     // }
//     // else if (putContainerResponse.StatusCode == HttpStatusCode.NotFound)
//     // {
//     //     // Cannot find the container
//     //     Console.WriteLine("Cannot find the container");
//     // }
//     // else
//     // {
//     //     // Handle other status codes if needed
//     //     Console.WriteLine($"Unexpected status code: {putContainerResponse.StatusCode}");
//     // }

//     // GET: Retrieve the updated container
//     HttpResponseMessage getContainerResponse = await _httpClient.GetAsync("/api/container");
//     Assert.AreEqual(HttpStatusCode.OK, getContainerResponse.StatusCode);

//     // Validate the response content
//     string getContainerResponseBody = await getContainerResponse.Content.ReadAsStringAsync();
//     // Console.WriteLine($"Response Body: {getContainerResponseBody}");

//     var containers = JsonConvert.DeserializeObject<List<Container>>(getContainerResponseBody);

//     //Console log to inspect containers
//     // foreach (var container in containers)
//     // {
//     //     Console.WriteLine($"ContainerId: {container.ContainerId}, Type: {container.Type}, Status: {container.Status}");
//     // }

//     // Assert that containers are not null and there is at least one container
//     Assert.IsNotNull(containers);
//     Assert.IsTrue(containers.Any()); // This ensures that there is at least one container
// }

// [Test]
// public async Task Backend_TestDeleteContainer()
// {
//     // Register a user with the "Admin" role
//     string uniqueId = Guid.NewGuid().ToString();
//     string uniqueUsername = $"abcd_{uniqueId}";
//     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

//     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
//     HttpResponseMessage registerResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

//     // Login the registered user
//     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}";
//     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
//     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
//     dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
//     string userAuthToken = loginResponseMap.token;

//     // Use the obtained token in the request to post a new container
//     _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

//     // POST: Create a new container
//     var newContainer = new Container
//     {
//         ContainerId = 0, // Set the desired ContainerId
//         Type = "string",
//         Status = "string",
//         Capacity = 100,
//         Location = "string",
//         Weight = 50.5,
//         Owner = "string",
//         CreationDate = DateTime.UtcNow,
//         LastInspectionDate = DateTime.UtcNow
//     };

//     string postContainerBody = JsonConvert.SerializeObject(newContainer);
//     HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));
//     Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

//     // Extract the created container from the response
//     var createdContainer = JsonConvert.DeserializeObject<Container>(await postContainerResponse.Content.ReadAsStringAsync());
//     long containerId = createdContainer.ContainerId;

//     // DELETE: Delete the created container
//     HttpResponseMessage deleteContainerResponse = await _httpClient.DeleteAsync($"/api/container/{containerId}");
//     if (deleteContainerResponse.StatusCode == HttpStatusCode.OK)
//     {
//         // Container deleted successfully
//         Console.WriteLine("Container deleted successfully");
//     }
//     else if (deleteContainerResponse.StatusCode == HttpStatusCode.NotFound)
//     {
//         // Cannot find the container
//         Console.WriteLine("Cannot find the container");
//     }
//     else
//     {
//         // Handle other status codes if needed
//         Console.WriteLine($"Unexpected status code: {deleteContainerResponse.StatusCode}");
//     }

//     // GET: Retrieve the containers
//     HttpResponseMessage getContainerResponse = await _httpClient.GetAsync("/api/container");
//     Assert.AreEqual(HttpStatusCode.OK, getContainerResponse.StatusCode);

//     // Validate the response content
//     string getContainerResponseBody = await getContainerResponse.Content.ReadAsStringAsync();
//     Console.WriteLine($"Response Body: {getContainerResponseBody}");

//     var containers = JsonConvert.DeserializeObject<List<Container>>(getContainerResponseBody);

//     // Console log to inspect containers
//     // foreach (var container in containers)
//     // {
//     //     Console.WriteLine($"ContainerId: {container.ContainerId}, Type: {container.Type}, Status: {container.Status}");
//     // }

//     // Assert that containers are not null
//     Assert.IsNotNull(containers);
// }

[Test]
public async Task Backend_TestPostAssignmentAsAdmin()
{
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
    
    // Create a new container payload
    var newContainer = new
    {
        Type = "string",
        Status = "string",
        Capacity = 0,
        Location = "string",
        Weight = 0,
        Owner = "string",
        CreationDate = "2024-02-18T03:08:57.598Z",
        // LastInspectionDate = "2024-02-18T03:08:57.598Z"
    };

    // Convert the newContainer object to JSON string
    string postContainerBody = JsonConvert.SerializeObject(newContainer);

    // POST: Create a new container
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));

    // Assert the response status code
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

    // Extract the created container from the response
    // Extract the created container from the response
   var createdContainer = JsonConvert.DeserializeObject<dynamic>(await postContainerResponse.Content.ReadAsStringAsync());

// Check if the createdContainer is not null and contains the ContainerId property
if (createdContainer != null && createdContainer.ContainerId != null)
{
    // Use the obtained ContainerId in the newAssignment JSON
    long uniqueContainerId = GenerateUniqueContainerId(); // Replace with your logic to generate a unique long value

    var newAssignment = new
    {
        AssignmentId = 0,
        ContainerId = uniqueContainerId,
        UserId = 1,
        Status = "string",
        UpdateTime = "2024-02-18T02:11:12.528Z",
        Route = "string",
        Shipment = "string",
        Destination = "string"
    };

    // Convert the newAssignment object to JSON string
string postAssignmentBody = JsonConvert.SerializeObject(newAssignment);

long GenerateUniqueContainerId()
{
    return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
}


    // POST: Create a new assignment
    HttpResponseMessage postAssignmentResponse = await _httpClient.PostAsync("/api/assignment", new StringContent(postAssignmentBody, Encoding.UTF8, "application/json"));

    // Log request and response details for debugging
    Console.WriteLine($"Request Body: {postAssignmentBody}");
    Console.WriteLine($"Response Status Code: {postAssignmentResponse.StatusCode}");
    Console.WriteLine($"Response Body: {await postAssignmentResponse.Content.ReadAsStringAsync()}");

    // Assert the response status code
    Assert.AreEqual(HttpStatusCode.Created, postAssignmentResponse.StatusCode);

    // Validate the response content
    string postAssignmentResponseBody = await postAssignmentResponse.Content.ReadAsStringAsync();
    Console.WriteLine($"Response Body: {postAssignmentResponseBody}");

    // Extract the created assignment from the response
    var createdAssignment = JsonConvert.DeserializeObject<dynamic>(postAssignmentResponseBody);

    // Assert that the assignment is not null
    Assert.IsNotNull(createdAssignment);
}
}

[Test]
public async Task Backend_TestGetAssignmentAsAdmin()
{
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
    // Create a new assignment payload for POST
    // GET: Retrieve assignments
    HttpResponseMessage getAssignmentsResponse = await _httpClient.GetAsync("/api/assignment");

    // Log request and response details for debugging
    Console.WriteLine($"GET Assignments Response Status Code: {getAssignmentsResponse.StatusCode}");
    Console.WriteLine($"GET Assignments Response Body: {await getAssignmentsResponse.Content.ReadAsStringAsync()}");

    // Assert the response status code
    Assert.AreEqual(HttpStatusCode.OK, getAssignmentsResponse.StatusCode);

    // Validate the response content
    string getAssignmentsResponseBody = await getAssignmentsResponse.Content.ReadAsStringAsync();
    Console.WriteLine($"GET Assignments Response Body: {getAssignmentsResponseBody}");

    // Extract the list of assignments from the response
    var assignments = JsonConvert.DeserializeObject<List<dynamic>>(getAssignmentsResponseBody);

    // Log the count of assignments for debugging purposes
    Console.WriteLine($"Number of Assignments: {assignments.Count}");

    // Assert that the list is not null and contains assignments
    Assert.IsNotNull(assignments);
}

[Test]
public async Task Backend_TestGetAssignmentById()
{
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

    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

    // Create a new container payload
    var newContainer = new
    {
        Type = "string",
        Status = "string",
        Capacity = 0,
        Location = "string",
        Weight = 0,
        Owner = "string",
        CreationDate = "2024-02-18T03:08:57.598Z",
        // LastInspectionDate = "2024-02-18T03:08:57.598Z"
    };

    // Convert the newContainer object to JSON string
    string postContainerBody = JsonConvert.SerializeObject(newContainer);

    // POST: Create a new container
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));

    // Assert the response status code
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

    // Extract the created container from the response
    var createdContainer = JsonConvert.DeserializeObject<dynamic>(await postContainerResponse.Content.ReadAsStringAsync());

    // Check if the createdContainer is not null and contains the ContainerId property
    if (createdContainer != null && createdContainer.ContainerId != null)
    {
        // Use the obtained ContainerId in the newAssignment JSON
        long uniqueContainerId = GenerateUniqueContainerId(); // Replace with your logic to generate a unique long value

        var newAssignment = new
        {
            AssignmentId = 0,
            ContainerId = uniqueContainerId,
            UserId = 1,
            Status = "string",
            UpdateTime = "2024-02-18T02:11:12.528Z",
            Route = "string",
            Shipment = "string",
            Destination = "string"
        };

        // Convert the newAssignment object to JSON string
        string postAssignmentBody = JsonConvert.SerializeObject(newAssignment);

        // POST: Create a new assignment
        HttpResponseMessage postAssignmentResponse = await _httpClient.PostAsync("/api/assignment", new StringContent(postAssignmentBody, Encoding.UTF8, "application/json"));

        // Log request and response details for debugging
        Console.WriteLine($"Request Body: {postAssignmentBody}");
        Console.WriteLine($"Response Status Code: {postAssignmentResponse.StatusCode}");
        Console.WriteLine($"Response Body: {await postAssignmentResponse.Content.ReadAsStringAsync()}");

        // Assert the response status code
        Assert.AreEqual(HttpStatusCode.Created, postAssignmentResponse.StatusCode);

        // Validate the response content
        string postAssignmentResponseBody = await postAssignmentResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Body: {postAssignmentResponseBody}");

        // Extract the created assignment from the response
        var createdAssignment = JsonConvert.DeserializeObject<dynamic>(postAssignmentResponseBody);

        // Assert that the assignment is not null
        Assert.IsNotNull(createdAssignment);

        // Log in as an operator and retrieve assignments by user ID
        // Assuming you have an operator's credentials
        string operatorEmail = "operator@example.com";
        string operatorPassword = "operatorPassword";

        // Log in as an operator
        HttpResponseMessage operatorLoginResponse = await _httpClient.PostAsync("/api/login", new StringContent($"{{\"Email\" : \"{operatorEmail}\",\"Password\" : \"{operatorPassword}\"}}", Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, operatorLoginResponse.StatusCode);

        // Retrieve assignments by user ID
        HttpResponseMessage getAssignmentsByUserIdResponse = await _httpClient.GetAsync($"/api/assignment/user/{createdAssignment.UserId}");
        
        // Log request and response details for debugging
        Console.WriteLine($"GET Assignments by User ID Response Status Code: {getAssignmentsByUserIdResponse.StatusCode}");
        Console.WriteLine($"GET Assignments by User ID Response Body: {await getAssignmentsByUserIdResponse.Content.ReadAsStringAsync()}");

        // Assert the response status code for retrieving assignments by user ID
        Assert.AreEqual(HttpStatusCode.OK, getAssignmentsByUserIdResponse.StatusCode);
        
        // Validate the response content if needed

        // You can add more assertions based on your specific requirements
    }

long GenerateUniqueContainerId()
{
    return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
}
}

[Test]
public async Task Backend_TestPostAndPutAssignmentByAssignmentId()
{
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

    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

    // Create a new container payload
    var newContainer = new
    {
        Type = "string",
        Status = "string",
        Capacity = 0,
        Location = "string",
        Weight = 0,
        Owner = "string",
        CreationDate = "2024-02-18T03:08:57.598Z",
        // LastInspectionDate = "2024-02-18T03:08:57.598Z"
    };

    // Convert the newContainer object to JSON string
    string postContainerBody = JsonConvert.SerializeObject(newContainer);

    // POST: Create a new container
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));

    // Assert the response status code
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

    // Extract the created container from the response
    var createdContainer = JsonConvert.DeserializeObject<dynamic>(await postContainerResponse.Content.ReadAsStringAsync());

    // Check if the createdContainer is not null and contains the ContainerId property
    if (createdContainer != null && createdContainer.ContainerId != null)
    {
        // Use the obtained ContainerId in the newAssignment JSON
        long uniqueContainerId = GenerateUniqueContainerId(); // Replace with your logic to generate a unique long value

        var newAssignment = new
        {
            AssignmentId = 0,
            ContainerId = uniqueContainerId,
            UserId = 1,
            Status = "string",
            UpdateTime = "2024-02-18T02:11:12.528Z",
            Route = "string",
            Shipment = "string",
            Destination = "string"
        };

        // Convert the newAssignment object to JSON string
        string postAssignmentBody = JsonConvert.SerializeObject(newAssignment);

        // POST: Create a new assignment
        HttpResponseMessage postAssignmentResponse = await _httpClient.PostAsync("/api/assignment", new StringContent(postAssignmentBody, Encoding.UTF8, "application/json"));

        // Log request and response details for debugging
        Console.WriteLine($"Request Body (POST Assignment): {postAssignmentBody}");
        Console.WriteLine($"Response Status Code (POST Assignment): {postAssignmentResponse.StatusCode}");
        Console.WriteLine($"Response Body (POST Assignment): {await postAssignmentResponse.Content.ReadAsStringAsync()}");

        // Assert the response status code for POSTing assignment
        Assert.AreEqual(HttpStatusCode.Created, postAssignmentResponse.StatusCode);

        // Validate the response content
        string postAssignmentResponseBody = await postAssignmentResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Body (POST Assignment): {postAssignmentResponseBody}");

        // Extract the created assignment from the response
        var createdAssignment = JsonConvert.DeserializeObject<dynamic>(postAssignmentResponseBody);

        // Assert that the assignment is not null
        Assert.IsNotNull(createdAssignment);

        // Update the created assignment
        createdAssignment.Status = "UpdatedStatus";
        createdAssignment.Route = "UpdatedRoute";

        // Convert the updatedAssignment object to JSON string
        string putAssignmentBody = JsonConvert.SerializeObject(createdAssignment);

        // PUT: Update the assignment
        HttpResponseMessage putAssignmentResponse = await _httpClient.PutAsync($"/api/assignment/{createdAssignment.AssignmentId}", new StringContent(putAssignmentBody, Encoding.UTF8, "application/json"));

        // Log request and response details for debugging
        Console.WriteLine($"Request Body (PUT Assignment): {putAssignmentBody}");
        Console.WriteLine($"Response Status Code (PUT Assignment): {putAssignmentResponse.StatusCode}");
        Console.WriteLine($"Response Body (PUT Assignment): {await putAssignmentResponse.Content.ReadAsStringAsync()}");

        // Assert the response status code for PUTting assignment
        Assert.AreEqual(HttpStatusCode.OK, putAssignmentResponse.StatusCode);

        // Validate the response content if needed

        // You can add more assertions based on your specific requirements
    }
}
long GenerateUniqueContainerId()
{
    return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
}


[Test]
public async Task Backend_TestPostAndDeleteAssignmentByAssignmentId()
{
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

    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAuthToken);

    // Create a new container payload
   // Create a new container payload
    var newContainer = new
    {
        Type = "string",
        Status = "string",
        Capacity = 0,
        Location = "string",
        Weight = 0,
        Owner = "string",
        CreationDate = "2024-02-18T03:08:57.598Z",
        // LastInspectionDate = "2024-02-18T03:08:57.598Z"
    };

    // Convert the newContainer object to JSON string
    string postContainerBody = JsonConvert.SerializeObject(newContainer);

    // POST: Create a new container
    HttpResponseMessage postContainerResponse = await _httpClient.PostAsync("/api/container", new StringContent(postContainerBody, Encoding.UTF8, "application/json"));

    // Assert the response status code
    Assert.AreEqual(HttpStatusCode.Created, postContainerResponse.StatusCode);

    // Extract the created container from the response
    var createdContainer = JsonConvert.DeserializeObject<dynamic>(await postContainerResponse.Content.ReadAsStringAsync());

    // Check if the createdContainer is not null and contains the ContainerId property
    if (createdContainer != null && createdContainer.ContainerId != null)
    {
        // Use the obtained ContainerId in the newAssignment JSON
        long uniqueContainerId = GenerateUniqueContainerId(); // Replace with your logic to generate a unique long value

        var newAssignment = new
        {
            AssignmentId = 0,
            ContainerId = uniqueContainerId,
            UserId = 1,
            Status = "string",
            UpdateTime = "2024-02-18T02:11:12.528Z",
            Route = "string",
            Shipment = "string",
            Destination = "string"
        };

        // Convert the newAssignment object to JSON string
        string postAssignmentBody = JsonConvert.SerializeObject(newAssignment);

        // POST: Create a new assignment
        HttpResponseMessage postAssignmentResponse = await _httpClient.PostAsync("/api/assignment", new StringContent(postAssignmentBody, Encoding.UTF8, "application/json"));

        // Log request and response details for debugging
        Console.WriteLine($"Request Body (POST Assignment): {postAssignmentBody}");
        Console.WriteLine($"Response Status Code (POST Assignment): {postAssignmentResponse.StatusCode}");
        Console.WriteLine($"Response Body (POST Assignment): {await postAssignmentResponse.Content.ReadAsStringAsync()}");

        // Assert the response status code for POSTing assignment
        Assert.AreEqual(HttpStatusCode.Created, postAssignmentResponse.StatusCode);

        // Validate the response content
        string postAssignmentResponseBody = await postAssignmentResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Body (POST Assignment): {postAssignmentResponseBody}");

        // Extract the created assignment from the response
        var createdAssignment = JsonConvert.DeserializeObject<dynamic>(postAssignmentResponseBody);

        // Assert that the assignment is not null
        Assert.IsNotNull(createdAssignment);

        // DELETE: Delete the assignment
        HttpResponseMessage deleteAssignmentResponse = await _httpClient.DeleteAsync($"/api/assignment/{createdAssignment.AssignmentId}");

        // Log request and response details for debugging
        Console.WriteLine($"Response Status Code (DELETE Assignment): {deleteAssignmentResponse.StatusCode}");
        Console.WriteLine($"Response Body (DELETE Assignment): {await deleteAssignmentResponse.Content.ReadAsStringAsync()}");

        // Assert the response status code for DELETing assignment
        Assert.AreEqual(HttpStatusCode.OK, deleteAssignmentResponse.StatusCode);

        // Validate the response content if needed

        // You can add more assertions based on your specific requirements
    }
}

// long GenerateUniqueContainerId()
// {
//     return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
// }


}