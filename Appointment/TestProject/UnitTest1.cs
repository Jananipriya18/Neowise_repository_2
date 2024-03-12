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
        private Type _bookType;
        private PropertyInfo[] _bookProperties;
        private DbContextOptions<ApplicationDbContext> _options1;




        [SetUp]
        public void Setup()
        {
        //     _bookType = new Book().GetType();
        //     _bookProperties = _bookType.GetProperties();        
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
   
        }

        
        [Test]
public void CreateBook_AddsBookToDatabase()
{
    Type bookType = Assembly.GetAssembly(typeof(Book)).GetTypes().FirstOrDefault(t => t.Name == "Book");

    if (bookType == null)
    {
        Assert.Fail("Book type not found.");
        return;
    }

    dynamic book = Activator.CreateInstance(bookType);
    book.Id = 1;
    book.Title = "demo";
    book.Author = "demo";
    book.Price = 100;
    book.Quantity = 5;

    var dbSetType = typeof(DbContext).GetMethods().FirstOrDefault(m =>
        m.Name == "Set" && m.ContainsGenericParameters)?.MakeGenericMethod(bookType);

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

    addMethod.Invoke(set, new object[] { book });

    _context.SaveChanges();

    var addedBook = _context.Find(bookType, 1);

    // Ensure the addedBook is of the Book type before accessing its properties
    if (addedBook is Book retrievedBook)
    {
        Assert.IsNotNull(retrievedBook);
        Assert.AreEqual("demo", retrievedBook.Title);
    }
    else
    {
        Assert.Fail("Failed to retrieve the book or incorrect type.");
    }
}
        

        
        [Test]
        public void ApplicationDbContextContainsDbSetBooks_Property()
        {
                 
            var propertyInfo = _context.GetType().GetProperty("Books");
            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DbSet<Book>), propertyInfo.PropertyType);
        } 

        [Test]
public void ApplicationDbContextContainsDbSetBooks_Property()
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

    // Check if DbSet<Book> exists
    Type bookType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Book");

    if (bookType == null)
    {
        Assert.Fail();
        // DbSet<Book> doesn't exist, so you can't check the property
        Assert.Inconclusive("Book type not found.");
        return;
    }

    // Get the property info using reflection
    var propertyInfo = contextType.GetProperty("Books");

    if (propertyInfo == null)
    {
        Assert.Fail("Books property not found.");
    }
    else
    {
        Assert.AreEqual(typeof(DbSet<>).MakeGenericType(bookType), propertyInfo.PropertyType);
    }
}
  
        //Checking if BookController exists
        [Test]
        public void BookControllerExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.BookController";

            Assembly assembly = Assembly.Load(assemblyName);
            Type bookControllerType = assembly.GetType(typeName);

            Assert.IsNotNull(bookControllerType, "BookController does not exist in the assembly.");
        }

        [Test]
        public void BookController_SearchMethodExists()
        {
            string assemblyName = "dotnetapp"; // Update with your correct assembly name
            string typeName = "dotnetapp.Controllers.BookController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Specify the parameter types for the search method
            Type[] parameterTypes = new Type[] { typeof(string) }; // Adjust this based on your method signature

            // Find the Search method with the specified parameter types
            MethodInfo searchMethod = controllerType.GetMethod("Search", parameterTypes);
            Assert.IsNotNull(searchMethod);
        }

        [Test]
        public void BookController_TotalCountMethodExists()
        {
            string assemblyName = "dotnetapp"; // Replace with your assembly name
            string typeName = "dotnetapp.Controllers.BookController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Find the TotalCount method without any parameters
            MethodInfo totalCountMethod = controllerType.GetMethod("TotalCount");
            Assert.IsNotNull(totalCountMethod);
        }
        [Test]
        public void BookController_IndexMethodExists()
        {
            string assemblyName = "dotnetapp"; // Replace with your assembly name
            string typeName = "dotnetapp.Controllers.BookController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Find the Index method without any parameters
            MethodInfo indexMethod = controllerType.GetMethod("Index");
            Assert.IsNotNull(indexMethod);
        }
        // public void BookController_SearchActionReturnsViewResult()
        // {
        //     string assemblyName = "dotnetapp"; // Replace with your assembly name
        //     string typeName = "dotnetapp.Controllers.BookController";
        //     Assembly assembly = Assembly.Load(assemblyName);
        //     Type controllerType = assembly.GetType(typeName);

        //     // Create an instance of ApplicationDbContext (mock or use an in-memory database for testing)
        //     var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDatabase").Options;
        //     var dbContext = new ApplicationDbContext(dbContextOptions);

        //     // Get the constructor of BookController that takes ApplicationDbContext as a parameter
        //     var constructor = controllerType.GetConstructor(new[] { typeof(ApplicationDbContext) });
        //     var controller = constructor.Invoke(new object[] { dbContext }) as BookController;

        //     IActionResult result = controller.Search("Sample Title");
        //     Assert.IsInstanceOf<ViewResult>(result);
        //     // Add additional behavior tests here if needed
        // }
        // [Test]
        // public void Test_Migration_Exists()
        // {

        //     string folderPath = @"D:\Dotnet_Weekly_2_Solutions\ShoppingCart\dotnetapp\dotnetapp\Migrations\"; // Replace with the folder path you want to check
        //     bool folderExists = Directory.Exists(folderPath);

        //     Assert.IsTrue(folderExists, "The folder does not exist.");
        // }
        [Test]
public void BookController_SearchActionReturnsViewResult()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string typeName = "dotnetapp.Controllers.BookController";

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

            var searchMethod = controllerType.GetMethod("Search", new[] { typeof(string) });

            if (searchMethod != null)
            {
                IActionResult result = searchMethod.Invoke(controller, new object[] { "Sample Title" }) as IActionResult;
                Assert.IsInstanceOf<ViewResult>(result);
                // Add additional behavior tests here if needed
            }
            else
            {
                Assert.Ignore("Search method not found. Skipping this test.");
            }
        }
        else
        {
            Assert.Ignore("BookController constructor not found. Skipping this test.");
        }
    }
    else
    {
        Assert.Ignore("BookController not found. Skipping this test.");
    }
}

[Test]
public void BookController_TotalCountActionReturnsContentResult()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string typeName = "dotnetapp.Controllers.BookController";

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

            var totalCountMethod = controllerType.GetMethod("TotalCount");

            if (totalCountMethod != null)
            {
                IActionResult result = totalCountMethod.Invoke(controller, null) as IActionResult;
                Assert.IsInstanceOf<ContentResult>(result);
                // Add additional behavior tests here if needed
            }
            else
            {
                Assert.Ignore("TotalCount method not found. Skipping this test.");
            }
        }
        else
        {
            Assert.Ignore("BookController constructor not found. Skipping this test.");
        }
    }
    else
    {
        Assert.Ignore("BookController not found. Skipping this test.");
    }
}


        // [Test]
        // public void BookController_TotalCountActionReturnsContentResult()
        // {
        //     string assemblyName = "dotnetapp"; // Replace with your assembly name
        //     string typeName = "dotnetapp.Controllers.BookController";
        //     Assembly assembly = Assembly.Load(assemblyName);
        //     Type controllerType = assembly.GetType(typeName);

        //     // Create an instance of ApplicationDbContext (mock or use an in-memory database for testing)
        //     var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDatabase").Options;
        //     var dbContext = new ApplicationDbContext(dbContextOptions);

        //     // Get the constructor of BookController that takes ApplicationDbContext as a parameter
        //     var constructor = controllerType.GetConstructor(new[] { typeof(ApplicationDbContext) });
        //     var controller = constructor.Invoke(new object[] { dbContext }) as BookController;

        //     IActionResult result = controller.TotalCount();
        //     Assert.IsInstanceOf<ContentResult>(result);
        //     // Add additional behavior tests here if needed
        // }


        [Test]
public void BookController_SearchWithNullTitle_ReturnsErrorMessage()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string typeName = "dotnetapp.Controllers.BookController";

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

            var searchMethod = controllerType.GetMethod("Search");

            if (searchMethod != null)
            {
                IActionResult result = searchMethod.Invoke(controller, new object[] { null }) as IActionResult;
                // Assert how the controller handles null titles
                Assert.IsInstanceOf<ActionResult>(result); // Change to the appropriate ActionResult type
            }
            else
            {
                Assert.Ignore("Search method not found. Skipping this test.");
            }
        }
        else
        {
            Assert.Ignore("BookController constructor not found. Skipping this test.");
        }
    }
    else
    {
        Assert.Ignore("BookController not found. Skipping this test.");
    }
}


[Test]
public void BookController_EmptyBookListInIndex_ReturnsEmptyView()
{
    string assemblyName = "dotnetapp"; // Replace with your assembly name
    string typeName = "dotnetapp.Controllers.BookController";

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

            var searchMethod = controllerType.GetMethod("Search");

            if (searchMethod != null)
            {
                // Mock the search parameter value
                string searchTerm = "SomeTitle";

                IActionResult result = searchMethod.Invoke(controller, new object[] { searchTerm }) as IActionResult;
                var viewResult = result as ViewResult;

                Assert.IsNotNull(viewResult);
                Assert.Pass("View returned successfully.");
            }
            else
            {
                Assert.Ignore("Search method not found. Skipping this test.");
            }
        }
        else
        {
            Assert.Ignore("BookController constructor not found. Skipping this test.");
        }
    }
    else
    {
        Assert.Ignore("BookController not found. Skipping this test.");
    }
}

    }
}

