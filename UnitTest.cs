using dotnetapp.Exceptions;
using dotnetapp.Models;
using dotnetapp.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Reflection;
using dotnetapp.Services;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace dotnetapp.Tests
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

   [Test, Order(1)]
    public async Task Backend_Test_Post_Method_Register_Manager_Returns_HttpStatusCode_OK()
    {
        ClearDatabase();
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Manager\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
  
   [Test, Order(2)]
    public async Task Backend_Test_Post_Method_Login_Manager_Returns_HttpStatusCode_OK()
    {
        ClearDatabase();

        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Manager\"}}";
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
public async Task Backend_Test_Get_All_LeaveRequest_With_Token_By_Manager_Returns_HttpStatusCode_OK()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Manager\"}}";
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
    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    string token = responseMap.token;

    Assert.IsNotNull(token);

    // Use the token to get all feeds
    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/leaverequest");

    // Print feed response
    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, apiResponse.StatusCode);
}


[Test, Order(4)]
public async Task Backend_Test_Get_All_LeaveRequest_Without_Token_By_Manager_Returns_HttpStatusCode_Unauthorized()
{
    ClearDatabase();
    string uniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Manager\"}}";
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
    string responseBody = await loginResponse.Content.ReadAsStringAsync();

    HttpResponseMessage apiResponse = await _httpClient.GetAsync("/api/leaverequest");

    // Print feed response
    string apiResponseBody = await apiResponse.Content.ReadAsStringAsync();
    Console.WriteLine("apiResponseBody: " + apiResponseBody);

    Assert.AreEqual(HttpStatusCode.Unauthorized, apiResponse.StatusCode);
}


[Test, Order(5)]
public async Task Backend_Test_Get_Method_Get_LeaveRequestById_In_Leave_Service_Fetches_LeaveRequest_Successfully()
{
    ClearDatabase();

    // Set up user data
    var userData = new Dictionary<string, object>
    {
        { "UserId", 401 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Employee" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Set up leave request data
    var leaveRequestData = new Dictionary<string, object>
    {
        { "LeaveRequestId", 1 },
        { "UserId", 401 },
        { "StartDate", DateTime.Now.AddDays(1) },
        { "EndDate", DateTime.Now.AddDays(10) },
        { "Reason", "Vacation" },
        { "LeaveType", "PTO" },
        { "Status", "Pending" },
        { "CreatedOn", DateTime.Now }
    };

    // Create leave request instance and set properties
    var leaveRequest = new LeaveRequest();
    foreach (var kvp in leaveRequestData)
    {
        var propertyInfo = typeof(LeaveRequest).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(leaveRequest, kvp.Value);
        }
    }

    // Add leave request to the context and save changes
    _context.LeaveRequests.Add(leaveRequest);
    _context.SaveChanges();

    // // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.LeaveRequestService";
    string modelName = "dotnetapp.Models.LeaveRequest";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // // Get the GetLeaveRequestById method
    MethodInfo getLeaveRequestMethod = serviceType.GetMethod("GetLeaveRequestById");

    // Check if method exists
    if (getLeaveRequestMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var retrievedLeaveRequest = (Task<LeaveRequest>)getLeaveRequestMethod.Invoke(service, new object[] { 1 });

        // Assert the retrieved leave request is not null and properties match
        Assert.IsNotNull(retrievedLeaveRequest);
        Assert.AreEqual(leaveRequest.Reason, retrievedLeaveRequest.Result.Reason);
    }
    else
    {
        Assert.Fail();
    }
}



[Test, Order(6)]
public async Task Backend_Test_Put_Method_UpdateLeaveRequest_In_Leave_Service_Updates_LeaveRequest_Successfully()
{
    ClearDatabase();

    // Set up user data
    var userData = new Dictionary<string, object>
    {
        { "UserId", 51 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Employee" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Set up initial leave request data
    var initialLeaveRequestData = new Dictionary<string, object>
    {
        { "LeaveRequestId", 1 },
        { "UserId", 51 },
        { "StartDate", DateTime.Now.AddDays(1) },
        { "EndDate", DateTime.Now.AddDays(10) },
        { "Reason", "Vacation" },
        { "LeaveType", "PTO" },
        { "Status", "Pending" },
        { "CreatedOn", DateTime.Now }
    };

    // Create and add initial leave request to context
    var initialLeaveRequest = new LeaveRequest();
    foreach (var kvp in initialLeaveRequestData)
    {
        var propertyInfo = typeof(LeaveRequest).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(initialLeaveRequest, kvp.Value);
        }
    }
    _context.LeaveRequests.Add(initialLeaveRequest);
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.LeaveRequestService";
    string modelName = "dotnetapp.Models.LeaveRequest";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(modelName);

    // Get UpdateLeaveRequest method
    MethodInfo updateMethod = serviceType.GetMethod("UpdateLeaveRequest", new[] { typeof(int), modelType });

    if (updateMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);

        // Set up updated leave request data
        var updatedLeaveRequestData = new Dictionary<string, object>
        {
            { "LeaveRequestId", 1 },
            { "UserId", 51 },
            { "StartDate", DateTime.Now.AddDays(5) },
            { "EndDate", DateTime.Now.AddDays(15) },
            { "Reason", "Updated Vacation" },
            { "LeaveType", "PTO" },
            { "Status", "Pending" },
            { "CreatedOn", DateTime.Now }
        };

        // Create updated leave request instance and set properties
        var updatedLeaveRequest = Activator.CreateInstance(modelType);
        foreach (var kvp in updatedLeaveRequestData)
        {
            var propertyInfo = modelType.GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(updatedLeaveRequest, kvp.Value);
            }
        }

        // Invoke UpdateLeaveRequest method
        var updateResult = (Task<bool>)updateMethod.Invoke(service, new object[] { 1, updatedLeaveRequest });
        await updateResult;

        // Retrieve updated leave request from database
        var updatedLeaveRequestFromDb = await _context.LeaveRequests.FindAsync(1);

        // Assert the updated leave request properties
        Assert.IsNotNull(updatedLeaveRequestFromDb);
        Assert.AreEqual("Updated Vacation", updatedLeaveRequestFromDb.Reason);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(7)]
public async Task Backend_Test_Delete_Method_DeleteWfhRequest_In_Wfh_Service_Deletes_WfhRequest_Successfully()
{
    ClearDatabase();

    // Set up user data
    var userData = new Dictionary<string, object>
    {
        { "UserId", 402 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Employee" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Set up initial WFH request data
    var initialWfhRequestData = new Dictionary<string, object>
    {
        { "WfhRequestId", 1 },
        { "UserId", 402 },
        { "StartDate", DateTime.Now.AddDays(1) },
        { "EndDate", DateTime.Now.AddDays(10) },
        { "Reason", "Medical" },
        { "Status", "Pending" },
        { "CreatedOn", DateTime.Now }
    };

    // Create and add initial WFH request to context
    var initialWfhRequest = new WfhRequest();
    foreach (var kvp in initialWfhRequestData)
    {
        var propertyInfo = typeof(WfhRequest).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(initialWfhRequest, kvp.Value);
        }
    }
    _context.WfhRequests.Add(initialWfhRequest);
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.WfhRequestService";

    Type serviceType = assembly.GetType(serviceName);

    // Get DeleteWfhRequest method
    MethodInfo deleteMethod = serviceType.GetMethod("DeleteWfhRequest", new[] { typeof(int) });

    if (deleteMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);

        // Invoke DeleteWfhRequest method
        var deleteResult = (Task<bool>)deleteMethod.Invoke(service, new object[] { 1 });
        await deleteResult;

        // Retrieve deleted WFH request from database
        var deletedWfhRequestFromDb = await _context.WfhRequests.FindAsync(1);

        // Assert the WFH request is deleted
        Assert.IsNull(deletedWfhRequestFromDb);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(8)]
public async Task Backend_Test_Get_Method_GetWfhRequestsByUserId_In_Wfh_Service_Fetches_Successfully()
{
    ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId", 402 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Employee" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Add WFH request
    var wfhRequestData = new Dictionary<string, object>
    {
        { "WfhRequestId", 1 },
        { "UserId", 402 },
        { "StartDate", DateTime.Now.AddDays(1) },
        { "EndDate", DateTime.Now.AddDays(5) },
        { "Reason", "Medical" },
        { "Status", "Pending" },
        { "CreatedOn", DateTime.Now }
    };

    var wfhRequest = new WfhRequest();
    foreach (var kvp in wfhRequestData)
    {
        var propertyInfo = typeof(WfhRequest).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(wfhRequest, kvp.Value);
        }
    }
    _context.WfhRequests.Add(wfhRequest);
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.WfhRequestService";

    Type serviceType = assembly.GetType(serviceName);

    // Get GetWfhRequestsByUserId method
    MethodInfo getWfhRequestsByUserIdMethod = serviceType.GetMethod("GetWfhRequestsByUserId");

    if (getWfhRequestsByUserIdMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);

        // Invoke GetWfhRequestsByUserId method
        var result = (Task<IEnumerable<WfhRequest>>)getWfhRequestsByUserIdMethod.Invoke(service, new object[] { 402 });
        Assert.IsNotNull(result);

        var check = true;
        foreach (var wfhRequestItem in result.Result)
        {
            check = false;
            Assert.AreEqual(402, wfhRequestItem.UserId);
            Assert.AreEqual("Medical", wfhRequestItem.Reason);
        }

        if (check)
        {
            Assert.Fail("No WFH requests found for the user ID");
        }
    }
    else
    {
        Assert.Fail("Method GetWfhRequestsByUserId not found in WfhService");
    }
}

[Test, Order(9)]
public async Task Backend_Test_Get_Method_GetAllWfhRequests_In_WfhService_Returns_All_WfhRequests()
{
    ClearDatabase();

    var userData = new Dictionary<string, object>
    {
        { "UserId", 500 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Manager" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Set up initial WfhRequest data
    var initialWfhRequestData = new List<Dictionary<string, object>>
    {
        new Dictionary<string, object>
        {
            { "WfhRequestId", 1 },
            { "UserId", 500 },
            { "StartDate", DateTime.Now.AddDays(1) },
            { "EndDate", DateTime.Now.AddDays(5) },
            { "Reason", "Medical" },
            { "Status", "Pending" },
            { "CreatedOn", DateTime.Now }
        },
        new Dictionary<string, object>
        {
            { "WfhRequestId", 2 },
            { "UserId", 500 },
            { "StartDate", DateTime.Now.AddDays(10) },
            { "EndDate", DateTime.Now.AddDays(15) },
            { "Reason", "Family" },
            { "Status", "Pending" },
            { "CreatedOn", DateTime.Now }
        }
    };

    // Create and add WfhRequests to context
    foreach (var wfhRequestData in initialWfhRequestData)
    {
        var wfhRequest = new WfhRequest();
        foreach (var kvp in wfhRequestData)
        {
            var propertyInfo = typeof(WfhRequest).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(wfhRequest, kvp.Value);
            }
        }
        _context.WfhRequests.Add(wfhRequest);
    }
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.WfhRequestService";

    Type serviceType = assembly.GetType(serviceName);

    // Get GetAllWfhRequests method
    MethodInfo getAllWfhRequestsMethod = serviceType.GetMethod("GetAllWfhRequests");

    if (getAllWfhRequestsMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);

        // Invoke GetAllWfhRequests method
        var getAllWfhRequestsResult = (Task<IEnumerable<WfhRequest>>)getAllWfhRequestsMethod.Invoke(service, null);
        var wfhRequests = await getAllWfhRequestsResult;

        // Assert the results
        Assert.IsNotNull(wfhRequests);
        Assert.AreEqual(2, wfhRequests.Count());
        Assert.AreEqual("Medical", wfhRequests.First().Reason);
        Assert.AreEqual("Family", wfhRequests.Last().Reason);
    }
    else
    {
        Assert.Fail("GetAllWfhRequests method not found.");
    }
}

[Test, Order(10)]
public async Task Backend_Test_Get_Method_GetAllLeaveRequests_In_LeaveService_Returns_All_LeaveRequests()
{
    ClearDatabase();

    // Set up user data
    var userData = new Dictionary<string, object>
    {
        { "UserId", 500 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Manager" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Set up initial LeaveRequest data
    var initialLeaveRequestData = new List<Dictionary<string, object>>
    {
        new Dictionary<string, object>
        {
            { "LeaveRequestId", 1 },
            { "UserId", 500 },
            { "StartDate", DateTime.Now.AddDays(1) },
            { "EndDate", DateTime.Now.AddDays(5) },
            { "Reason", "Vacation" },
            { "LeaveType", "PTO" },
            { "Status", "Pending" },
            { "CreatedOn", DateTime.Now }
        },
        new Dictionary<string, object>
        {
            { "LeaveRequestId", 2 },
            { "UserId", 500 },
            { "StartDate", DateTime.Now.AddDays(10) },
            { "EndDate", DateTime.Now.AddDays(15) },
            { "Reason", "Medical" },
            { "LeaveType", "PTO" },
            { "Status", "Pending" },
            { "CreatedOn", DateTime.Now }
        }
    };

    // Create and add LeaveRequests to context
    foreach (var leaveRequestData in initialLeaveRequestData)
    {
        var leaveRequest = new LeaveRequest();
        foreach (var kvp in leaveRequestData)
        {
            var propertyInfo = typeof(LeaveRequest).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(leaveRequest, kvp.Value);
            }
        }
        _context.LeaveRequests.Add(leaveRequest);
    }
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.LeaveRequestService";

    Type serviceType = assembly.GetType(serviceName);

    // Get GetAllLeaveRequests method
    MethodInfo getAllLeaveRequestsMethod = serviceType.GetMethod("GetAllLeaveRequests");

    if (getAllLeaveRequestsMethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);

        // Invoke GetAllLeaveRequests method
        var getAllLeaveRequestsResult = (Task<IEnumerable<LeaveRequest>>)getAllLeaveRequestsMethod.Invoke(service, null);
        var leaveRequests = await getAllLeaveRequestsResult;

        // Assert the results
        Assert.IsNotNull(leaveRequests);
        Assert.AreEqual(2, leaveRequests.Count());
        Assert.AreEqual("Vacation", leaveRequests.First().Reason);
        Assert.AreEqual("Medical", leaveRequests.Last().Reason);
    }
    else
    {
        Assert.Fail("GetAllLeaveRequests method not found.");
    }
}


[Test, Order(11)]
public async Task Backend_Test_Post_Method_AddWfhRequest_In_WfhService_Occurs_WfhException_For_Overlapping_Request()
{
    ClearDatabase();

    // Add user to context
    var userData = new Dictionary<string, object>
    {
        { "UserId", 402 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Employee" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    // Load assembly and types
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string serviceName = "dotnetapp.Services.WfhRequestService";
    string typeName = "dotnetapp.Models.WfhRequest";

    Type serviceType = assembly.GetType(serviceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("AddWfhRequest", new[] { modelType });

    if (method != null)
    {
        var wfhRequestData = new Dictionary<string, object>
        {
            { "WfhRequestId", 1 },
            { "UserId", 402 },
            { "StartDate", DateTime.Now.AddDays(1) },
            { "EndDate", DateTime.Now.AddDays(5) },
            { "Reason", "Medical" },
            { "Status", "Pending" },
            { "CreatedOn", DateTime.Now }
        };

        var wfhRequest = Activator.CreateInstance(modelType);
        foreach (var kvp in wfhRequestData)
        {
            var propertyInfo = modelType.GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(wfhRequest, kvp.Value);
            }
        }

        var service = Activator.CreateInstance(serviceType, _context);
        var result = (Task<bool>)method.Invoke(service, new object[] { wfhRequest });
        var addedWfhRequest = await _context.WfhRequests.FindAsync(1);
        Assert.IsNotNull(addedWfhRequest);

        var overlappingWfhRequestData = new Dictionary<string, object>
        {
            { "WfhRequestId", 2 },
            { "UserId", 402 },
            { "StartDate", DateTime.Now.AddDays(2) },
            { "EndDate", DateTime.Now.AddDays(4) },
            { "Reason", "Family" },
            { "Status", "Pending" },
            { "CreatedOn", DateTime.Now }
        };

        var overlappingWfhRequest = Activator.CreateInstance(modelType);
        foreach (var kvp in overlappingWfhRequestData)
        {
            var propertyInfo = modelType.GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(overlappingWfhRequest, kvp.Value);
            }
        }

        try
        {
            var result1 = (Task<bool>)method.Invoke(service, new object[] { overlappingWfhRequest });
            Console.WriteLine("res" + result1.Result);
            Assert.Fail();
        }
        catch (Exception ex)
        {
            Assert.IsNotNull(ex.InnerException);
            Assert.IsTrue(ex.InnerException is WfhException);
            Assert.AreEqual("There is already a WFH request for these dates", ex.InnerException.Message);
        }
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(12)]
public async Task Backend_Test_Post_Method_AddFeedback_In_Feedback_Service_Posts_Successfully()
{
        ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId",42 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Owner" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();
    // Add loan application
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.FeedbackService";
    string typeName = "dotnetapp.Models.Feedback";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("AddFeedback", new[] { modelType });

    if (method != null)
    {
           var feedbackData = new Dictionary<string, object>
            {
                { "FeedbackId", 11 },
                { "UserId", 42 },
                { "FeedbackText", "Great experience!" },
                { "Date", DateTime.Now }
            };
        var feedback = new Feedback();
        foreach (var kvp in feedbackData)
        {
            var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(feedback, kvp.Value);
            }
        }
        var service = Activator.CreateInstance(serviceType, _context);
        var result = (Task<bool>)method.Invoke(service, new object[] { feedback });
    
        var addedFeedback= await _context.Feedbacks.FindAsync(11);
        Assert.IsNotNull(addedFeedback);
        Assert.AreEqual("Great experience!",addedFeedback.FeedbackText);

    }
    else{
        Assert.Fail();
    }
}

[Test, Order(13)]
public async Task Backend_Test_Delete_Method_Feedback_In_Feeback_Service_Deletes_Successfully()
{
    // Add user
     ClearDatabase();

    var userData = new Dictionary<string, object>
    {
        { "UserId",42 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Owner" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

           var feedbackData = new Dictionary<string, object>
            {
                { "FeedbackId", 11 },
                { "UserId", 42 },
                { "FeedbackText", "Great experience!" },
                { "Date", DateTime.Now }
            };
        var feedback = new Feedback();
        foreach (var kvp in feedbackData)
        {
            var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(feedback, kvp.Value);
            }
        }
     _context.Feedbacks.Add(feedback);
    _context.SaveChanges();
    // Add loan application
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.FeedbackService";
    string typeName = "dotnetapp.Models.Feedback";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

  
    MethodInfo deletemethod = serviceType.GetMethod("DeleteFeedback", new[] { typeof(int) });

    if (deletemethod != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var deleteResult = (Task<bool>)deletemethod.Invoke(service, new object[] { 11 });

        var deletedFeedbackFromDb = await _context.Feedbacks.FindAsync(11);
        Assert.IsNull(deletedFeedbackFromDb);
    }
    else
    {
        Assert.Fail();
    }
}

[Test, Order(14)]
public async Task Backend_Test_Get_Method_GetFeedbacksByUserId_In_Feedback_Service_Fetches_Successfully()
{
    ClearDatabase();

    // Add user
    var userData = new Dictionary<string, object>
    {
        { "UserId", 330 },
        { "Username", "testuser" },
        { "Password", "testpassword" },
        { "Email", "test@example.com" },
        { "MobileNumber", "1234567890" },
        { "UserRole", "Owner" }
    };

    var user = new User();
    foreach (var kvp in userData)
    {
        var propertyInfo = typeof(User).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(user, kvp.Value);
        }
    }
    _context.Users.Add(user);
    _context.SaveChanges();

    var feedbackData= new Dictionary<string, object>
    {
        { "FeedbackId", 13 },
        { "UserId", 330 },
        { "FeedbackText", "Great experience!" },
        { "Date", DateTime.Now }
    };

    var feedback = new Feedback();
    foreach (var kvp in feedbackData)
    {
        var propertyInfo = typeof(Feedback).GetProperty(kvp.Key);
        if (propertyInfo != null)
        {
            propertyInfo.SetValue(feedback, kvp.Value);
        }
    }
    _context.Feedbacks.Add(feedback);
    _context.SaveChanges();

    // Add loan application
    string assemblyName = "dotnetapp";
    Assembly assembly = Assembly.Load(assemblyName);
    string ServiceName = "dotnetapp.Services.FeedbackService";
    string typeName = "dotnetapp.Models.Feedback";

    Type serviceType = assembly.GetType(ServiceName);
    Type modelType = assembly.GetType(typeName);

    MethodInfo method = serviceType.GetMethod("GetFeedbacksByUserId");

    if (method != null)
    {
        var service = Activator.CreateInstance(serviceType, _context);
        var result = ( Task<IEnumerable<Feedback>>)method.Invoke(service, new object[] {330});
        Assert.IsNotNull(result);
         var check=true;
        foreach (var item in result.Result)
        {
            check=false;
            Assert.AreEqual("Great experience!", item.FeedbackText);
   
        }
        if(check==true)
        {
            Assert.Fail();

        }
    }
    else{
        Assert.Fail();
    }
}

private void ClearDatabase()
{
    _context.Database.EnsureDeleted();
    _context.Database.EnsureCreated();
}

}
}

