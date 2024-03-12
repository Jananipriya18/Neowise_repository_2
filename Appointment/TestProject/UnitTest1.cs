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
        private Type _AppointmentType;
        private PropertyInfo[] _bookProperties;
        private DbContextOptions<ApplicationDbContext> _options1;




        [SetUp]
        public void Setup()
        {
        //     _AppointmentType = new Book().GetType();
        //     _bookProperties = _AppointmentType.GetProperties();        
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
   
        }

        
//         [Test]
// public void CreateAppointment_AddsAppointmentToDatabase()
// {
//     Type appointmentType = Assembly.GetAssembly(typeof(Appointment)).GetTypes()
//         .FirstOrDefault(t => t.Name == "Appointment");

//     if (appointmentType == null)
//     {
//         Assert.Fail("Appointment type not found.");
//         return;
//     }

//     dynamic appointment = Activator.CreateInstance(appointmentType);
//     appointment.AppointmentID = 1;
//     appointment.PatientFirstName = "John";
//     appointment.PatientLastName = "Doe";
//     appointment.DoctorFirstName = "Dr. Smith";
//     appointment.AppointmentDate = DateTime.Now;

//     var dbSetType = typeof(DbContext).GetMethods()
//         .FirstOrDefault(m => m.Name == "Set" && m.ContainsGenericParameters)
//         ?.MakeGenericMethod(appointmentType);

//     if (dbSetType == null)
//     {
//         Assert.Fail("Set method not found.");
//         return;
//     }

//     var set = dbSetType.Invoke(_context, null);

//     MethodInfo addMethod = set.GetType().GetMethod("Add");

//     if (addMethod == null)
//     {
//         Assert.Fail("Add method not found.");
//         return;
//     }

//     addMethod.Invoke(set, new object[] { appointment });

//     _context.SaveChanges();

//     var addedAppointment = _context.Find(appointmentType, 1);

//     // Ensure the addedAppointment is of the Appointment type before accessing its properties
//     if (addedAppointment is Appointment retrievedAppointment)
//     {
//         Assert.IsNotNull(retrievedAppointment);
//         Assert.AreEqual("John", retrievedAppointment.PatientFirstName);
//     }
//     else
//     {
//         Assert.Fail("Failed to retrieve the appointment or incorrect type.");
//     }
// }


        
        

//         [Test]
// public void ApplicationDbContextContainsDbSetBooks_Property()
// {
//     // Get the assembly that contains your ApplicationDbContext
//     Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));

//     // Get the ApplicationDbContext type
//     Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));

//     if (contextType == null)
//     {
//         Assert.Fail("ApplicationDbContext type not found.");
//         return;
//     }

//     // Check if DbSet<Book> exists
//     Type AppointmentType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Book");

//     if (AppointmentType == null)
//     {
//         Assert.Fail();
//         // DbSet<Book> doesn't exist, so you can't check the property
//         Assert.Inconclusive("Book type not found.");
//         return;
//     }

//     // Get the property info using reflection
//     var propertyInfo = contextType.GetProperty("Books");

//     if (propertyInfo == null)
//     {
//         Assert.Fail("Books property not found.");
//     }
//     else
//     {
//         Assert.AreEqual(typeof(DbSet<>).MakeGenericType(AppointmentType), propertyInfo.PropertyType);
//     }
// }
  
        //Checking if AppointmentController exists
        [Test]
        public void AppointmentControllerExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.AppointmentController";

            Assembly assembly = Assembly.Load(assemblyName);
            Type AppointmentControllerType = assembly.GetType(typeName);

            Assert.IsNotNull(AppointmentControllerType, "AppointmentController does not exist in the assembly.");
        }

       [Test]
        public void AppointmentController_CreateMethodExists()
        {
            string assemblyName = "dotnetapp"; // Update with your correct assembly name
            string typeName = "dotnetapp.Controllers.AppointmentController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Specify the parameter types for the create method
            Type[] parameterTypes = new Type[] { typeof(Appointment) }; // Adjust this based on your method signature

            // Find the Create method with the specified parameter types
            MethodInfo createMethod = controllerType.GetMethod("Create", parameterTypes);

            // Assert that the method exists and has the correct return type
            Assert.IsNotNull(createMethod, "Create method not found");
            Assert.AreEqual(typeof(IActionResult), createMethod.ReturnType, "Create method has incorrect return type");
        }


        [Test]
        public void AppointmentController_IndexMethodExists()
        {
            string assemblyName = "dotnetapp"; // Replace with your assembly name
            string typeName = "dotnetapp.Controllers.AppointmentController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Find the Index method without any parameters
            MethodInfo indexMethod = controllerType.GetMethod("Index");
            Assert.IsNotNull(indexMethod);
        }
        
        [Test]
public void AppointmentController_CreateActionReturnsIndexView()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string typeName = "dotnetapp.Controllers.AppointmentController";

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

            var createAction = controllerType.GetMethod("Create", new[] { typeof(Appointment) });

            if (createAction != null)
            {
                // Provide required properties for the Appointment entity
                var appointment = new Appointment
                {
                    DoctorFirstName = "SampleDoctorFirstName",
                    DoctorLastName = "SampleDoctorLastName",
                    DoctorSpecialty = "SampleSpecialty",
                    PatientEmail = "sample@email.com",
                    PatientFirstName = "SamplePatientFirstName",
                    PatientLastName = "SamplePatientLastName",
                    PatientPhoneNumber = "1234567890",
                    Reason = "SampleReason"
                };

                IActionResult result = createAction.Invoke(controller, new object[] { appointment }) as IActionResult;
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
            Assert.Ignore("AppointmentController constructor not found. Skipping this test.");
        }
    }
    else
    {
        Assert.Ignore("AppointmentController not found. Skipping this test.");
    }
}

[Test]
public void AppointmentController_EditActionExists()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string typeName = "dotnetapp.Controllers.AppointmentController";

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

            var editAction = controllerType.GetMethod("Edit", new[] { typeof(int) });

            Assert.IsNotNull(editAction);
        }
        else
        {
            Assert.Ignore("AppointmentController constructor not found. Skipping this test.");
        }
    }
    else
    {
        Assert.Ignore("AppointmentController not found. Skipping this test.");
    }
}






 }
}

