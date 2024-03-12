using NUnit.Framework;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace dotnetapp.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private ApplicationDbContext _context;
        private Type _feedbackType;
        private PropertyInfo[] _bookProperties;
        private DbContextOptions<ApplicationDbContext> _options1;




        [SetUp]
        public void Setup()
        {
        //     _feedbackType = new Book().GetType();
        //     _bookProperties = _feedbackType.GetProperties();        
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
   
        }

        
        [Test]
public void CreateFeedback_AddsFeedbackToDatabase()
{
    Type feedbackType = Assembly.GetAssembly(typeof(Feedback)).GetTypes()
        .FirstOrDefault(t => t.Name == "Feedback");

    if (feedbackType == null)
    {
        Assert.Fail("Feedback type not found.");
        return;
    }

    dynamic feedback = Activator.CreateInstance(feedbackType);
    feedback.Id = 0;
    feedback.StudentName = "SampleStudentName";
    feedback.Course = "SampleCourse";
    feedback.Feedbacks = "SampleFeedback";
    feedback.Rating = 4;
    feedback.DateSubmitted = DateTime.Now;

    var dbSetType = typeof(DbContext).GetMethods()
        .FirstOrDefault(m => m.Name == "Set" && m.ContainsGenericParameters)
        ?.MakeGenericMethod(feedbackType);

    if (dbSetType == null)
    {
        Assert.Fail("Set method not found.");
        return;
    }

    var set = dbSetType.Invoke(_context, null);

    MethodInfo addMethod = set.GetType().GetMethod("Add");

    if (addMethod == null)
    {
        Assert.Fail("Add method not found.");
        return;
    }

    addMethod.Invoke(set, new object[] { feedback });

    _context.SaveChanges();

    var addedFeedback = _context.Find(feedbackType, 1);

    // Ensure the addedFeedback is of the Feedback type before accessing its properties
    if (addedFeedback is Feedback retrievedFeedback)
    {
        Assert.IsNotNull(retrievedFeedback);
        Assert.AreEqual("SampleStudentName", retrievedFeedback.StudentName);
    }
    else
    {
        Assert.Fail("Failed to retrieve the Feedback or incorrect type.");
    }
}

[Test]
public void ApplicationDbContextContainsDbSetFeedbacks_Property()
{
    // Get the assembly that contains your ApplicationDbContext
    Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));

    // Get the ApplicationDbContext type
    Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));

    if (contextType == null)
    {
        Assert.Fail("ApplicationDbContext type not found.");
        return;
    }

    // Check if DbSet<Feedback> exists
    Type feedbackType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Feedback");

    if (feedbackType == null)
    {
        Assert.Fail("Feedback type not found.");
        return;
    }

    // Get the property info using reflection
    var propertyInfo = contextType.GetProperty("Feedbacks");

    if (propertyInfo == null)
    {
        Assert.Fail("Feedbacks property not found.");
    }
    else
    {
        Assert.AreEqual(typeof(DbSet<>).MakeGenericType(feedbackType), propertyInfo.PropertyType);
    }
}

  
        //Checking if FeedbackController exists
        [Test]
        public void FeedbackControllerExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.FeedbackController";

            Assembly assembly = Assembly.Load(assemblyName);
            Type FeedbackControllerType = assembly.GetType(typeName);

            Assert.IsNotNull(FeedbackControllerType, "FeedbackController does not exist in the assembly.");
        }

       [Test]
        public void FeedbackController_CreateMethodExists()
        {
            string assemblyName = "dotnetapp"; // Update with your correct assembly name
            string typeName = "dotnetapp.Controllers.FeedbackController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Specify the parameter types for the create method
            Type[] parameterTypes = new Type[] { }; // Update parameter types based on your method signature

            // Find the Create method with the specified parameter types
            MethodInfo createMethod = controllerType.GetMethod("Create", parameterTypes);

            // Assert that the method exists and has the correct return type
            Assert.IsNotNull(createMethod, "Create method not found");
            Assert.AreEqual(typeof(IActionResult), createMethod.ReturnType, "Create method has incorrect return type");
        }



        [Test]
        public void FeedbackController_IndexMethodExists()
        {
            string assemblyName = "dotnetapp"; // Replace with your assembly name
            string typeName = "dotnetapp.Controllers.FeedbackController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Find the Index method without any parameters
            MethodInfo indexMethod = controllerType.GetMethod("Index");
            Assert.IsNotNull(indexMethod);
        }
        
[Test]
public void FeedbackController_CreateActionReturnsIndexView()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string typeName = "dotnetapp.Controllers.FeedbackController";

    Assembly assembly = Assembly.Load(assemblyName);
    Type controllerType = assembly.GetType(typeName);

    if (controllerType != null)
    {
        var constructor = controllerType.GetConstructor(new[] { typeof(ApplicationDbContext) });

        if (constructor != null)
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDatabase").Options;
            var dbContext = new ApplicationDbContext(dbContextOptions);

            var controller = constructor.Invoke(new object[] { dbContext });

            var createAction = controllerType.GetMethod("Create", new[] { typeof(Feedback) });

            if (createAction != null)
            {
                // Provide required properties for the Feedback entity
                var feedback = new Feedback
                {
                    StudentName = "SampleStudentName",
                    Course = "SampleCourse",
                    Feedbacks = "SampleFeedback",
                    Rating = 4
                    // Add other properties as needed
                };

                IActionResult result = createAction.Invoke(controller, new object[] { feedback }) as IActionResult;
                Assert.IsInstanceOf<RedirectToActionResult>(result);
                var redirectToActionResult = result as RedirectToActionResult;

                Assert.IsNotNull(redirectToActionResult);
                Assert.AreEqual("Index", redirectToActionResult.ActionName);
            }
            else
            {
                Assert.Ignore("Create action not found. Skipping this test.");
            }
        }
        else
        {
            Assert.Ignore("FeedbackController constructor not found. Skipping this test.");
        }
    }
    else
    {
        Assert.Ignore("FeedbackController not found. Skipping this test.");
    }
}

        [Test]
        public void FeedbackController_EditMethodExists()
        {
            string assemblyName = "dotnetapp"; // Replace with your assembly name
            string controllerTypeName = "dotnetapp.Controllers.FeedbackController";
            string methodName = "Edit";

            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(controllerTypeName);

            // Specify the parameter types for the Edit method
            Type[] parameterTypes = new Type[] { typeof(int) }; // Adjust this based on your method signature

            // Find the Edit method with the specified parameter types
            MethodInfo editMethod = controllerType.GetMethod(methodName, parameterTypes);

            // Assert that the method exists and has the correct return type
            Assert.IsNotNull(editMethod, $"{methodName} method not found in {controllerTypeName}");
            Assert.AreEqual(typeof(IActionResult), editMethod.ReturnType, $"{methodName} method has incorrect return type");
        }

// [Test]
// public void FeedbackController_EditActionRedirectsToIndex()
// {
//     string assemblyName = "dotnetapp"; // Replace with your assembly name
//     string typeName = "dotnetapp.Controllers.FeedbackController";

//     Assembly assembly = Assembly.Load(assemblyName);
//     Type controllerType = assembly.GetType(typeName);

//     if (controllerType != null)
//     {
//         var constructor = controllerType.GetConstructor(new[] { typeof(ApplicationDbContext) });

//         if (constructor != null)
//         {
//             var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDatabase").Options;
//             var dbContext = new ApplicationDbContext(dbContextOptions);

//             var controller = constructor.Invoke(new object[] { dbContext });

//             var editMethod = controllerType.GetMethod("Edit", new[] { typeof(Feedback) });
//             var indexMethod = controllerType.GetMethod("Index");

//             if (editMethod != null && indexMethod != null)
//             {
//                 // Create a sample Feedback to edit with all required properties initialized
//                 var Feedback = new Feedback
//                 {
//                     DoctorFirstName = "SampleFirstName",
//                     DoctorLastName = "SampleLastName",
//                     DoctorSpecialty = "SampleSpecialty",
//                     PatientEmail = "sample@email.com",
//                     PatientFirstName = "PatientFirstName",
//                     PatientLastName = "PatientLastName",
//                     PatientPhoneNumber = "1234567890",
//                     Reason = "Sample Reason"
//                     // Initialize other properties as needed
//                 };

//                 // Save the sample Feedback to the database
//                 dbContext.Feedbacks.Add(Feedback);
//                 dbContext.SaveChanges();

//                 // Invoke the Edit method with the sample Feedback
//                 IActionResult result = editMethod.Invoke(controller, new object[] { Feedback }) as IActionResult;

//                 // Ensure the result is a RedirectToActionResult
//                 Assert.IsInstanceOf<RedirectToActionResult>(result);

//                 // Cast the result to RedirectToActionResult and check if it redirects to the "Index" action
//                 var redirectToActionResult = result as RedirectToActionResult;
//                 Assert.IsNotNull(redirectToActionResult);
//                 Assert.AreEqual("Index", redirectToActionResult.ActionName);
//             }
//             else
//             {
//                 Assert.Ignore("Edit or Index method not found. Skipping this test.");
//             }
//         }
//         else
//         {
//             Assert.Ignore("FeedbackController constructor not found. Skipping this test.");
//         }
//     }
//     else
//     {
//         Assert.Ignore("FeedbackController not found. Skipping this test.");
//     }
// }

        [Test]
        public void FeedbackController_DeleteMethodExists()
        {
            string assemblyName = "dotnetapp"; // Replace with your assembly name
            string typeName = "dotnetapp.Controllers.FeedbackController";

            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Specify the parameter types for the delete method
            Type[] parameterTypes = new Type[] { typeof(int) }; // Adjust this based on your method signature

            // Find the Delete method with the specified parameter types
            MethodInfo deleteMethod = controllerType.GetMethod("Delete", parameterTypes);

            // Assert that the method exists and has the correct return type
            Assert.IsNotNull(deleteMethod, "Delete method not found");
            Assert.AreEqual(typeof(IActionResult), deleteMethod.ReturnType, "Delete method has incorrect return type");
        }

[Test]
public void FeedbackController_DeleteActionReturnsViewResult()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string typeName = "dotnetapp.Controllers.FeedbackController";

    Assembly assembly = Assembly.Load(assemblyName);
    Type controllerType = assembly.GetType(typeName);

    if (controllerType != null)
    {
        var constructor = controllerType.GetConstructor(new[] { typeof(ApplicationDbContext) });

        if (constructor != null)
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDatabase").Options;
            var dbContext = new ApplicationDbContext(dbContextOptions);

            var controller = constructor.Invoke(new object[] { dbContext });

            var deleteMethod = controllerType.GetMethod("Delete", new[] { typeof(int) });

            if (deleteMethod != null)
            {
                // Create a sample Feedback to delete with all required properties initialized
                var Feedback = new Feedback
                {
                    DoctorFirstName = "SampleFirstName",
                    DoctorLastName = "SampleLastName",
                    DoctorSpecialty = "SampleSpecialty",
                    PatientEmail = "sample@email.com",
                    PatientFirstName = "PatientFirstName",
                    PatientLastName = "PatientLastName",
                    PatientPhoneNumber = "1234567890",
                    Reason = "Sample Reason"
                    // Initialize other properties as needed
                };

                // Save the sample Feedback to the database
                dbContext.Feedbacks.Add(Feedback);
                dbContext.SaveChanges();

                // Invoke the Delete method with the sample Feedback ID
                IActionResult result = deleteMethod.Invoke(controller, new object[] { feedback.FeedbackID }) as IActionResult;

                // Ensure the result is a ViewResult
                Assert.IsInstanceOf<ViewResult>(result);
            }
            else
            {
                Assert.Ignore("Delete method not found. Skipping this test.");
            }
        }
        else
        {
            Assert.Ignore("FeedbackController constructor not found. Skipping this test.");
        }
    }
    else
    {
        Assert.Ignore("FeedbackController not found. Skipping this test.");
    }
}


 }
}

