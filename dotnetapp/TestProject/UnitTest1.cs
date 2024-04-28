using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;
using dotnetapp.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class TrainTicketBookingTests
    {
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(_dbContextOptions);

            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                var teamData = new Dictionary<string, object>
                    {
                        //{ "TrainID", 1 },
                        { "DepartureLocation", "Location A" },
                        { "Destination", "Location B" },
                        { "DepartureTime", DateTime.Parse("2023-08-30") },
                        { "MaximumCapacity", 4 }
                    };
                // Add test data to the in-memory database
                var train = new Train();
                foreach (var kvp in teamData)
                {
                    var propertyInfo = typeof(Train).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(train, kvp.Value);
                    }
                }

                dbContext.Trains.Add(train);
                dbContext.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                // Clear the in-memory database after each test
                dbContext.Database.EnsureDeleted();
            }
        }

        [Test]
        public void BookSeat_TrainController_ValidPassenger_JoinsSuccessfully_Redirect_to_Details_TrainController()
        {
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string modelType = "dotnetapp.Models.Passenger";
            string controllerTypeName = "dotnetapp.Controllers.PassengerController"; // Corrected controller type name
            Type controllerType = assembly.GetType(controllerTypeName);
            Type controllerType2 = assembly.GetType(modelType);
            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var teamData = new Dictionary<string, object>
                    {
                        { "Name", "John Doe" },
                        { "Email", "johndoe@example.com" },
                        { "Phone", "1234567890" }
                    };
                var passenger = new Passenger();
                foreach (var kvp in teamData)
                {
                    var propertyInfo = typeof(Passenger).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(passenger, kvp.Value);
                    }
                }
                MethodInfo method = controllerType.GetMethod("BookSeat", new[] { typeof(int), controllerType2 });

                if (method != null)
                {
                    var controller = Activator.CreateInstance(controllerType, _context);
                    var result = method.Invoke(controller, new object[] { 1, passenger }) as RedirectToActionResult;


                    //var result = TrainController.BookSeat(1, passenger) as RedirectToActionResult;

                    Assert.IsNotNull(result);

                    Assert.AreEqual("Details", result.ActionName);
                    Assert.AreEqual("Train", result.ControllerName);
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void BookSeat_TrainController_ValidPassenger_Adds_Passenger_To_Train_Successfully()
        {
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string modelType = "dotnetapp.Models.Passenger"; // Corrected model type to Passenger
            string controllerTypeName = "dotnetapp.Controllers.PassengerController"; // Corrected controller type name to PassengerController
            Type controllerType = assembly.GetType(controllerTypeName);
            Type controllerType2 = assembly.GetType(modelType);
            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                var teamData = new Dictionary<string, object>
                {
                    { "Name", "John Doe" },
                    { "Email", "johndoe@example.com" },
                    { "Phone", "1234567890" }
                };
                var passenger = new Passenger(); 
                foreach (var kvp in teamData)
                {
                    var propertyInfo = typeof(Passenger).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(passenger, kvp.Value);
                    }
                }
                MethodInfo method = controllerType.GetMethod("BookSeat", new[] { typeof(int), controllerType2 }); // Changed BookSeat to BookSeat

                if (method != null)
                {
                    var ride1 = _context.Trains.Include(r => r.Passengers).ToList().FirstOrDefault(o => o.TrainID == 1); // Simplified retrieving the ride
                    Assert.AreEqual(0, ride1.Passengers.Count);
                    var controller = Activator.CreateInstance(controllerType, _context);
                    var result = method.Invoke(controller, new object[] { 1, passenger }) as RedirectToActionResult;
                    var ride = _context.Trains.Include(r => r.Passengers).ToList().FirstOrDefault(o => o.TrainID == 1); // Simplified retrieving the ride
                    Assert.IsNotNull(ride);
                    Assert.AreEqual(1, ride.Passengers.Count);
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void BookSeat_TrainController_InvalidPassenger_Name_Email_Phone_are_required_ModelStateInvalid()
        {
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string controllerTypeName = "dotnetapp.Controllers.PassengerController"; // Corrected controller type name to PassengerController
            Type controllerType = assembly.GetType(controllerTypeName);
            string modelType = "dotnetapp.Models.Passenger";
            Type modelType2 = assembly.GetType(modelType);

            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                // Arrange
                var trainController = Activator.CreateInstance(controllerType, dbContext); // Corrected variable name to trainController
                var passenger = Activator.CreateInstance(modelType2); // Corrected variable name to passenger 

                // Add errors to ModelState
                var modelStateProperty = controllerType.GetProperty("ModelState");
                var modelState = modelStateProperty.GetValue(trainController) as ModelStateDictionary;
                modelState.AddModelError("Name", "Name is required");
                modelState.AddModelError("Email", "Email is required");
                modelState.AddModelError("Phone", "Phone is required");

                // Invoke BookSeat method using reflection
                MethodInfo bookSeatMethod = controllerType.GetMethod("BookSeat", new[] { typeof(int), modelType2 });
                var result = bookSeatMethod.Invoke(trainController, new object[] { 1, passenger  }) as ViewResult;

                // Assert
                Assert.IsNotNull(result);
                Assert.IsFalse(result.ViewData.ModelState.IsValid);
                Assert.AreEqual(3, result.ViewData.ModelState.ErrorCount);
                Assert.IsTrue(result.ViewData.ModelState.ContainsKey("Name"));
                Assert.IsTrue(result.ViewData.ModelState.ContainsKey("Email"));
                Assert.IsTrue(result.ViewData.ModelState.ContainsKey("Phone"));
            }
        }


        // test to check that BookSeat method in TrainController with invalid ride id returns NotFoundResult
        [Test]
        public void BookSeat_TrainController_RideNotFound_ReturnsNotFoundResult()
        {
            string assemblyName = "dotnetapp";
            Assembly assembly = Assembly.Load(assemblyName);
            string modelType = "dotnetapp.Models.Passenger";
            string controllerTypeName = "dotnetapp.Controllers.PassengerController";
            Type controllerType = assembly.GetType(controllerTypeName);
            Type controllerType2 = assembly.GetType(modelType);

            if (controllerType == null || controllerType2 == null)
            {
                Assert.Fail("Controller types not found.");
            }

            using (var dbContext = new ApplicationDbContext(_dbContextOptions))
            {
                var teamData = new Dictionary<string, object>
                {
                    { "Name", "John Doe" },
                    { "Email", "johndoe@example.com" },
                    { "Phone", "1234567890" }
                };
                var passenger = new Passenger();
                foreach (var kvp in teamData)
                {
                    var propertyInfo = typeof(Passenger).GetProperty(kvp.Key);
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(passenger, kvp.Value);
                    }
                }

                var constructor = controllerType.GetConstructor(new[] { typeof(ApplicationDbContext) });
                if (constructor == null)
                {
                    Assert.Fail("Constructor not found.");
                }

                var controller = Activator.CreateInstance(controllerType, dbContext);
                if (controller == null)
                {
                    Assert.Fail("Controller instance could not be created.");
                }

                MethodInfo method = controllerType.GetMethod("BookSeat", new[] { typeof(int) });
                if (method == null)
                {
                    Assert.Fail("Method not found.");
                }

                var result = method.Invoke(controller, new object[] { 2 }) as NotFoundResult;
                Assert.IsNotNull(result);
            }
        }

        // // test to check that BookSeat method in TrainController throws exception when maximum capacity is reached
        // [Test]
        // public void BookSeat_TrainController_MaximumCapacityReached_ThrowsException()
        // {
        //     string assemblyName = "dotnetapp";
        //     Assembly assembly = Assembly.Load(assemblyName);
        //     string modelType = "dotnetapp.Models.Passenger";
        //     string exception = "dotnetapp.Exceptions.TrainBookingException";
        //     string controllerTypeName = "dotnetapp.Controllers.PassengerController";
        //     Type controllerType = assembly.GetType(controllerTypeName);
        //     Type controllerType2 = assembly.GetType(modelType);
        //     Type exceptionType = assembly.GetType(exception);

        //     using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        //     {
        //         var teamData = new Dictionary<string, object>
        //             {
        //                 { "Name", "John Doe" },
        //                 { "Email", "johndoe@example.com" },
        //                 { "Phone", "1234567890" }
        //             };
        //         var teamData1 = new Dictionary<string, object>
        //             {
        //                 { "Name", "John Doe1" },
        //                 { "Email", "johndoe1@example.com" },
        //                 { "Phone", "1234567891" }
        //             };
        //         var passenger = new Passenger();
        //         var passenger1 = new Passenger();
        //         foreach (var kvp in teamData1)
        //         {
        //             var propertyInfo = typeof(Passenger).GetProperty(kvp.Key);
        //             if (propertyInfo != null)
        //             {
        //                 propertyInfo.SetValue(passenger1, kvp.Value);
        //             }
        //         }
        //         foreach (var kvp in teamData)
        //         {
        //             var propertyInfo = typeof(Passenger).GetProperty(kvp.Key);
        //             if (propertyInfo != null)
        //             {
        //                 propertyInfo.SetValue(passenger, kvp.Value);
        //             }
        //         }
        //         MethodInfo method = controllerType.GetMethod("BookSeat", new[] { typeof(int) });


        //         var ride = _context.Trains.Include(r => r.Passengers).ToList().FirstOrDefault(o => (int)o.GetType().GetProperty("TrainID").GetValue(o) == 1);
        //         ride.Passengers.Add(passenger1);
        //         ride.Passengers.Add(passenger);
        //         var propertyInfo1 = ride.GetType().GetProperty("MaximumCapacity");
        //         if (propertyInfo1 != null)
        //         {
        //             propertyInfo1.SetValue(ride, 2);
        //         }

        //         dbContext.SaveChanges();

        //         var teamData2 = new Dictionary<string, object>
        //             {
        //                 { "Name", "John Doe2" },
        //                 { "Email", "johndoe2@example.com" },
        //                 { "Phone", "1234567892" }
        //             };
        //         var passenger2 = new Passenger();
        //         foreach (var kvp in teamData2)
        //         {
        //             var propertyInfo = typeof(Passenger).GetProperty(kvp.Key);
        //             if (propertyInfo != null)
        //             {
        //                 propertyInfo.SetValue(passenger2, kvp.Value);
        //             }
        //         }
        //         if (method != null)
        //         {
        //             var controller = Activator.CreateInstance(controllerType, _context);
        //             var ex = Assert.Throws<TargetInvocationException>(() => method.Invoke(controller, new object[] { 1 }));

        //             var innerException = ex.InnerException;

        //             Assert.IsNotNull(innerException);
        //             var rideSharingExceptionType = exceptionType;
        //             bool isRideSharingException = rideSharingExceptionType.IsInstanceOfType(innerException);

        //             Assert.IsTrue(isRideSharingException, $"Expected inner exception of type {rideSharingExceptionType.FullName}");
        //         }
        //     }
        // }

// [Test]
// public void BookSeat_TrainController_MaximumCapacityReached_ThrowsException()
// {
//     string assemblyName = "dotnetapp";
//     Assembly assembly = Assembly.Load(assemblyName);
//     string controllerTypeName = "dotnetapp.Controllers.TrainController";
//     Type controllerType = assembly.GetType(controllerTypeName);
//     Type exceptionType = typeof(TrainBookingException);

//     using (var dbContext = new ApplicationDbContext(_dbContextOptions))
//     {
//         var train = new Train { TrainID = 1, MaximumCapacity = 2 };
//         train.Passengers.Add(new Passenger { Name = "John Doe1", Email = "johndoe1@example.com", Phone = "1234567891" });
//         train.Passengers.Add(new Passenger { Name = "John Doe2", Email = "johndoe2@example.com", Phone = "1234567892" });
//         dbContext.Trains.Add(train);
//         dbContext.SaveChanges();

//         // Invoke the BookSeat method
//         var controller = Activator.CreateInstance(controllerType, dbContext);
//         var method = controllerType.GetMethod("BookSeat", new[] { typeof(int) });

//         // Ensure that the method is not null before invoking
//         if (method == null)
//         {
//             throw new InvalidOperationException($"Method 'BookSeat' not found in {controllerTypeName}");
//         }

//         // Handle the exception within the test
//         try
//         {
//             method.Invoke(controller, new object[] { 1 });
//         }
//         catch (TargetInvocationException ex)
//         {
//             // Assert that the inner exception is of type TrainBookingException
//             var innerException = ex.InnerException;
//             Assert.IsNotNull(innerException);
//             Assert.IsTrue(exceptionType.IsInstanceOfType(innerException), $"Expected inner exception of type {exceptionType.FullName}");
//         }
//     }
// }

//         // test to check that BookSeat method in TrainController throws exception when maximum capacity is reached with correct message "Maximum capacity reached"
//         [Test]
//         public void BookSeat_TrainController_MaximumCapacityReached_ThrowsException_with_Message()
//         {
//             string assemblyName = "dotnetapp";
//             Assembly assembly = Assembly.Load(assemblyName);
//             string modelType = "dotnetapp.Models.Passenger";
//             string exception = "dotnetapp.Exceptions.TrainBookingException";
//             string controllerTypeName = "dotnetapp.Controllers.PassengerController";
//             Type controllerType = assembly.GetType(controllerTypeName);
//             Type controllerType2 = assembly.GetType(modelType);
//             Type exceptionType = assembly.GetType(exception);

//             using (var dbContext = new ApplicationDbContext(_dbContextOptions))
//             {
//                 var teamData = new Dictionary<string, object>
//                     {
//                         { "Name", "John Doe" },
//                         { "Email", "johndoe@example.com" },
//                         { "Phone", "1234567890" }
//                     };
//                 var teamData1 = new Dictionary<string, object>
//                     {
//                         { "Name", "John Doe1" },
//                         { "Email", "johndoe1@example.com" },
//                         { "Phone", "1234567891" }
//                     };
//                 var passenger = new Passenger();
//                 var passenger1 = new Passenger();
//                 foreach (var kvp in teamData1)
//                 {
//                     var propertyInfo = typeof(Passenger).GetProperty(kvp.Key);
//                     if (propertyInfo != null)
//                     {
//                         propertyInfo.SetValue(passenger1, kvp.Value);
//                     }
//                 }
//                 foreach (var kvp in teamData)
//                 {
//                     var propertyInfo = typeof(Passenger).GetProperty(kvp.Key);
//                     if (propertyInfo != null)
//                     {
//                         propertyInfo.SetValue(passenger, kvp.Value);
//                     }
//                 }
//                 MethodInfo method = controllerType.GetMethod("BookSeat", new[] { typeof(int) });


//                 var ride = _context.Rides.Include(r => r.Passengers).ToList().FirstOrDefault(o => (int)o.GetType().GetProperty("RideID").GetValue(o) == 1);
//                 ride.Passengers.Add(passenger1);
//                 ride.Passengers.Add(passenger);
//                 var propertyInfo1 = ride.GetType().GetProperty("MaximumCapacity");
//                 if (propertyInfo1 != null)
//                 {
//                     propertyInfo1.SetValue(ride, 2);
//                 }

//                 dbContext.SaveChanges();

//                 var teamData2 = new Dictionary<string, object>
//                     {
//                         { "Name", "John Doe2" },
//                         { "Email", "johndoe2@example.com" },
//                         { "Phone", "1234567892" }
//                     };
//                 var passenger2 = new Passenger();
//                 foreach (var kvp in teamData2)
//                 {
//                     var propertyInfo = typeof(Passenger).GetProperty(kvp.Key);
//                     if (propertyInfo != null)
//                     {
//                         propertyInfo.SetValue(passenger2, kvp.Value);
//                     }
//                 }
//                 if (method != null)
//                 {
//                     var controller = Activator.CreateInstance(controllerType, _context);
//                     //var ex =Assert.Throws<RideSharingException>(() => method.Invoke(controller, new object[] { 1, passenger }));
//                     //Console.WriteLine(ex.Message);
//                     var ex = Assert.Throws<TargetInvocationException>(() => method.Invoke(controller, new object[] { 1 }));

//                     // Retrieve the original exception thrown by the BookSeat method
//                     var innerException = ex.InnerException;

//                     // Assert that the inner exception is of type RideSharingException
//                     Assert.IsNotNull(innerException);
//                     var rideSharingExceptionType = exceptionType;
//                     bool isRideSharingException = rideSharingExceptionType.IsInstanceOfType(innerException);

//                     if (isRideSharingException)
//                     {
//                         var messageProperty = rideSharingExceptionType.GetProperty("Message");
//                         if (messageProperty != null)
//                         {
//                             var messageValue = messageProperty.GetValue(innerException);
//                             Assert.AreEqual("Maximum capacity reached", messageValue);
//                         }
//                     }
//                 }
//             }
//         }

// [Test]
// public void TrainController_Delete_Method_ValidId_DeletesTrainSuccessfully_Redirects_AvailableTrains()
// {
//     // Arrange
//     string assemblyName = "dotnetapp";
//     Assembly assembly = Assembly.Load(assemblyName);
//     string controllerTypeName = "dotnetapp.Controllers.TrainController";
//     Type controllerType = assembly.GetType(controllerTypeName);
//     using (var dbContext = new ApplicationDbContext(_dbContextOptions))
//     {
//         var train = new Train { TrainID = 100 }; // Create a train with ID 1
//         dbContext.Trains.Add(train);
//         dbContext.SaveChanges();

//         MethodInfo deleteMethod = controllerType.GetMethod("Delete", new[] { typeof(int) });
//         if (deleteMethod != null)
//         {
//             var controller = Activator.CreateInstance(controllerType, dbContext);

//             // Act
//             var result = deleteMethod.Invoke(controller, new object[] { train.TrainID }) as RedirectToActionResult;

//             // Assert
//             Assert.IsNotNull(result);
//             Assert.AreEqual("AvailableTrains", result.ActionName); // Use string instead of nameof

//             var trainAfterDelete = dbContext.Trains.Find(train.TrainID);
//             Assert.IsNull(trainAfterDelete); // Check if the deleted train is not present
//         }
//         else
//         {
//             Assert.Fail("Delete method not found in TrainController.");
//         }
//     }
// }

// [Test]
// public void BookSeat_DestinationSameAsDeparture_ReturnsViewWithValidationError()
// {
//     using (var dbContext = new ApplicationDbContext(_dbContextOptions))
//     {
//         // Arrange
//         string assemblyName = "dotnetapp";
//         Assembly assembly = Assembly.Load(assemblyName);
//         string controllerTypeName = "dotnetapp.Controllers.PassengerController";
//         Type controllerType = assembly.GetType(controllerTypeName);
        
//         var train = dbContext.Trains.FirstOrDefault(t => t.TrainID == 1);
//         train.Destination = train.DepartureLocation; // Set the destination as the same as departure
//         dbContext.SaveChanges();

//         MethodInfo bookSeatMethod = controllerType.GetMethod("BookSeat", new[] { typeof(int), typeof(Passenger) });
//         if (bookSeatMethod != null)
//         {
//             var passenger = new Passenger
//             {
//                 Name = "John Doe",
//                 Email = "johndoe@example.com",
//                 Phone = "1234567890"
//             };

//             var passengerController = Activator.CreateInstance(controllerType, dbContext);

//             try
//             {
//                 // Act
//                 var result = bookSeatMethod.Invoke(passengerController, new object[] { 1, passenger }) as ViewResult;

//                 // Assert
//                 Assert.IsNotNull(result);
//                 Assert.IsFalse(result.ViewData.ModelState.IsValid);
//                 Assert.IsTrue(result.ViewData.ModelState.ContainsKey("Destination"));
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Exception during method invocation: {ex}");
//                 throw;
//             }
//         }
//         else
//         {
//             Assert.Fail("BookSeat method not found in PassengerController.");
//         }
//     }
// }

//        [Test]
// public void BookSeat_MaximumCapacityNotPositiveInteger_ReturnsViewWithValidationError()
// {
//     using (var dbContext = new ApplicationDbContext(_dbContextOptions))
//     {
//         // Arrange
//         var trainController = new TrainController(dbContext);
//         var passenger = new Passenger
//         {
//             Name = "John Doe",
//             Email = "johndoe@example.com",
//             Phone = "1234567890"
//         };

//         // Act
//         var ride = dbContext.Trains.FirstOrDefault(r => r.TrainID == 1);
//         ride.MaximumCapacity = -5; // Set a negative value for MaximumCapacity
//         dbContext.SaveChanges();

//         var result = trainController.BookSeat(1) as ViewResult;

//         // Assert
//         Assert.IsNotNull(result);
//         Assert.IsFalse(result.ViewData.ModelState.IsValid);
//         Assert.IsTrue(result.ViewData.ModelState.ContainsKey("MaximumCapacity"));
//     }
// }

// Test to check that ApplicationDbContext Contains DbSet for model Train
[Test]
public void ApplicationDbContext_ContainsDbSet_Train()
{
    Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
    Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
    if (contextType == null)
    {
        Assert.Fail("No DbContext found in the assembly");
        return;
    }
    Type trainType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Train");
    if (trainType == null)
    {
        Assert.Fail("No Train model found in the assembly");
        return;
    }
    var propertyInfo = contextType.GetProperty("Trains");
    if (propertyInfo == null)
    {
        Assert.Fail("Trains property not found in the DbContext");
        return;
    }
    else
    {
        Assert.AreEqual(typeof(DbSet<>).MakeGenericType(trainType), propertyInfo.PropertyType);
    }
}


        // Test to check that ApplicationDbContext Contains DbSet for model Passenger
        [Test]
        public void ApplicationDbContext_ContainsDbSet_Passenger()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type PassengerType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Passenger");
            if (PassengerType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Passengers");
            if (propertyInfo == null)
            {
                Assert.Fail("Passengers property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(PassengerType), propertyInfo.PropertyType);
            }
        }

        // Test to Check Passenger Models Property PassengerID Exists with correcct datatype int    
        [Test]
        public void Passenger_PassengerID_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Passenger";
            Assembly assembly = Assembly.Load(assemblyName);
            Type PassengerType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = PassengerType.GetProperty("PassengerID");
            Assert.IsNotNull(propertyInfo, "Property PassengerID does not exist in Passenger class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property PassengerID in Passenger class is not of type int");
        }

        // Test to Check Passenger Models Property Name Exists with correcct datatype string    
        [Test]
        public void Passenger_Name_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Passenger";
            Assembly assembly = Assembly.Load(assemblyName);
            Type PassengerType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = PassengerType.GetProperty("Name");
            Assert.IsNotNull(propertyInfo, "Property Name does not exist in Passenger class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Name in Passenger class is not of type string");
        }

        // Test to Check Passenger Models Property Email Exists with correcct datatype string    
        [Test]
        public void Passenger_Email_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Passenger";
            Assembly assembly = Assembly.Load(assemblyName);
            Type PassengerType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = PassengerType.GetProperty("Email");
            Assert.IsNotNull(propertyInfo, "Property Email does not exist in Passenger class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Email in Passenger class is not of type string");
        }

        // Test to Check Passenger Models Property Phone Exists with correcct datatype string    
        [Test]
        public void Passenger_Phone_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Passenger";
            Assembly assembly = Assembly.Load(assemblyName);
            Type PassengerType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = PassengerType.GetProperty("Phone");
            Assert.IsNotNull(propertyInfo, "Property Phone does not exist in Passenger class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Phone in Passenger class is not of type string");
        }

        // Test to Check Passenger Models PropertyPassengerID Exists with correcct datatype int    
        [Test]
        public void Passenger_RideID_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Passenger";
            Assembly assembly = Assembly.Load(assemblyName);
            Type PassengerType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = PassengerType.GetProperty("PassengerID");
            Assert.IsNotNull(propertyInfo, "PropertyPassengerID does not exist in Passenger class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "PropertyPassengerID in Passenger class is not of type int");
        }

        // Test to Check Train Models Property TrainID Exists with correcct datatype int    
        [Test]
        public void Train_TrainID_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Train";
            Assembly assembly = Assembly.Load(assemblyName);
            Type TrainType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = TrainType.GetProperty("TrainID");
            Assert.IsNotNull(propertyInfo, "Property TrainID does not exist in Train class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property TrainID in Train class is not of type int");
        }

        // Test to Check Train Models Property DepartureLocation Exists with correcct datatype string    
        [Test]
        public void Train_DepartureLocation_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Train";
            Assembly assembly = Assembly.Load(assemblyName);
            Type TrainType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = TrainType.GetProperty("DepartureLocation");
            Assert.IsNotNull(propertyInfo, "Property DepartureLocation does not exist in Ride class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property DepartureLocation in Ride class is not of type string");
        }

        // Test to Check Train Models Property Destination Exists with correcct datatype string    
        [Test]
        public void Train_Destination_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Train";
            Assembly assembly = Assembly.Load(assemblyName);
            Type TrainType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = TrainType.GetProperty("Destination");
            Assert.IsNotNull(propertyInfo, "Property Destination does not exist in Ride class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Destination in Ride class is not of type string");
        }

        // Test to Check Train Models Property DepartureTime Exists with correcct datatype DateTime    
        [Test]
        public void Train_DepartureTime_PropertyExists_ReturnExpectedDataTypes_DateTime()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Train";
            Assembly assembly = Assembly.Load(assemblyName);
            Type TrainType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = TrainType.GetProperty("DepartureTime");
            Assert.IsNotNull(propertyInfo, "Property DepartureTime does not exist in Traine class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(DateTime), expectedType, "Property DepartureTime in Train class is not of type DateTime");
        }

        // Test to Check Ride Models Property MaximumCapacity Exists with correcct datatype int    
        [Test]
        public void Ride_MaximumCapacity_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Train";
            Assembly assembly = Assembly.Load(assemblyName);
            Type TrainType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = TrainType.GetProperty("MaximumCapacity");
            Assert.IsNotNull(propertyInfo, "Property MaximumCapacity does not exist in Train class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property MaximumCapacity in Train class is not of type int");
        }

        // Test to Check TrainController Controllers Method AvailableTrains with no parameter Returns IActionResult
        [Test]
        public void TrainController_AvailableTrains_Method_with_NoParams_Returns_IActionResult()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.TrainController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type TrainControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = TrainControllerType.GetMethod("AvailableTrains", Type.EmptyTypes);
            Assert.AreEqual(typeof(IActionResult), methodInfo.ReturnType, "Method AvailableTrains in TrainController class is not of type IActionResult");
        }

        // Test to Check TrainController Controllers Method Details with parameter int Returns IActionResult
        [Test]
        public void TrainController_Details_Method_Invokes_with_int_Param_Returns_IActionResult()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.TrainController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type TrainControllerType = assembly.GetType(typeName);
            object instance = Activator.CreateInstance(TrainControllerType, _context);
            MethodInfo methodInfo = TrainControllerType.GetMethod("Details", new[] { typeof(int) });
            object result = methodInfo.Invoke(instance, new object[] { default(int) });
            Assert.IsNotNull(result, "Result should not be null");
        }

        // Test to Check TrainController Controllers Method Create with parameter Ride Returns IActionResult
        [Test]
        public void TrainController_Delete_Method_Invokes_with_RideID_Param_Returns_IActionResult()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.TrainController";
            Assembly assembly = Assembly.Load(assemblyName);
            string typeName1 = "dotnetapp.Models.Train";
            Type TrainType = assembly.GetType(typeName1);
            object instance1 = Activator.CreateInstance(TrainType);
            Type TrainControllerType = assembly.GetType(typeName);
            object instance = Activator.CreateInstance(TrainControllerType, _context);
            MethodInfo methodInfo = TrainControllerType.GetMethod("Delete", new Type[] { typeof(int) });
            object result = methodInfo.Invoke(instance, new object[] { 1 });
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(typeof(IActionResult), methodInfo.ReturnType, "Method Delete in TrainController class is not of type IActionResult");
        }

        [Test]
        public void TrainController_DeleteConfirm_Method_Invokes_with_RideID_Param_Returns_IActionResult()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.TrainController";
            Assembly assembly = Assembly.Load(assemblyName);
            string typeName1 = "dotnetapp.Models.Train";
            Type TrainType = assembly.GetType(typeName1);
            object instance1 = Activator.CreateInstance(TrainType);
            Type TrainControllerType = assembly.GetType(typeName);
            object instance = Activator.CreateInstance(TrainControllerType, _context);
            MethodInfo methodInfo = TrainControllerType.GetMethod("DeleteConfirm", new Type[] { typeof(int) });
            object result = methodInfo.Invoke(instance, new object[] { 1 });
            Assert.IsNotNull(result, "Result should not be null");
            Assert.AreEqual(typeof(IActionResult), methodInfo.ReturnType, "Method DeleteConfirm in TrainController class is not of type IActionResult");
        }
     }

}