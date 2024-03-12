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

        
[Test]
public void CreateAppointment_AddsAppointmentToDatabase()
{
    Type appointmentType = Assembly.GetAssembly(typeof(Appointment)).GetTypes()
        .FirstOrDefault(t => t.Name == "Appointment");

    if (appointmentType == null)
    {
        Assert.Fail("Appointment type not found.");
        return;
    }

    dynamic appointment = Activator.CreateInstance(appointmentType);
    appointment.AppointmentID = 0;
    appointment.PatientFirstName = "SamplePatientFirstName";
    appointment.PatientLastName = "SamplePatientLastName";
    appointment.DoctorFirstName = "SampleDoctorFirstName";
    appointment.DoctorLastName = "SampleDoctorLastName";
    appointment.DoctorSpecialty = "SampleSpecialty";
    appointment.PatientEmail = "sample@email.com";
    appointment.PatientPhoneNumber = "1234567890";
    appointment.AppointmentDate = DateTime.Now;
    appointment.Reason = "SampleReason";

    var dbSetType = typeof(DbContext).GetMethods()
        .FirstOrDefault(m => m.Name == "Set" && m.ContainsGenericParameters)
        ?.MakeGenericMethod(appointmentType);

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

    addMethod.Invoke(set, new object[] { appointment });

    _context.SaveChanges();

    var addedAppointment = _context.Find(appointmentType, 1);

    // Ensure the addedAppointment is of the Appointment type before accessing its properties
    if (addedAppointment is Appointment retrievedAppointment)
    {
        Assert.IsNotNull(retrievedAppointment);
        Assert.AreEqual("SamplePatientFirstName", retrievedAppointment.PatientFirstName);
    }
    else
    {
        Assert.Fail("Failed to retrieve the appointment or incorrect type.");
    }
}

[Test]
public void ApplicationDbContextContainsDbSetAppointments_Property()
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

    // Check if DbSet<Appointment> exists
    Type appointmentType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Appointment");

    if (appointmentType == null)
    {
        Assert.Fail("Appointment type not found.");
        return;
    }

    // Get the property info using reflection
    var propertyInfo = contextType.GetProperty("Appointments");

    if (propertyInfo == null)
    {
        Assert.Fail("Appointments property not found.");
    }
    else
    {
        Assert.AreEqual(typeof(DbSet<>).MakeGenericType(appointmentType), propertyInfo.PropertyType);
    }
}

  
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
public void AppointmentController_EditMethodExists()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string controllerTypeName = "dotnetapp.Controllers.AppointmentController";
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

[Test]
public void AppointmentController_EditActionRedirectsToIndex()
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

            var editMethod = controllerType.GetMethod("Edit", new[] { typeof(Appointment) });
            var indexMethod = controllerType.GetMethod("Index");

            if (editMethod != null && indexMethod != null)
            {
                // Create a sample appointment to edit with all required properties initialized
                var appointment = new Appointment
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

                // Save the sample appointment to the database
                dbContext.Appointments.Add(appointment);
                dbContext.SaveChanges();

                // Invoke the Edit method with the sample appointment
                IActionResult result = editMethod.Invoke(controller, new object[] { appointment }) as IActionResult;

                // Ensure the result is a RedirectToActionResult
                Assert.IsInstanceOf<RedirectToActionResult>(result);

                // Cast the result to RedirectToActionResult and check if it redirects to the "Index" action
                var redirectToActionResult = result as RedirectToActionResult;
                Assert.IsNotNull(redirectToActionResult);
                Assert.AreEqual("Index", redirectToActionResult.ActionName);
            }
            else
            {
                Assert.Ignore("Edit or Index method not found. Skipping this test.");
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
public void AppointmentController_DeleteMethodExists()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string typeName = "dotnetapp.Controllers.AppointmentController";

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
public void AppointmentController_DeleteActionReturnsViewResult()
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

            var deleteMethod = controllerType.GetMethod("Delete", new[] { typeof(int) });

            if (deleteMethod != null)
            {
                // Create a sample appointment to delete with all required properties initialized
                var appointment = new Appointment
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

                // Save the sample appointment to the database
                dbContext.Appointments.Add(appointment);
                dbContext.SaveChanges();

                // Invoke the Delete method with the sample appointment ID
                IActionResult result = deleteMethod.Invoke(controller, new object[] { appointment.AppointmentID }) as IActionResult;

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

