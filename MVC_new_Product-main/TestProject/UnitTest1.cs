using System.Numerics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Microsoft.Data.SqlClient;
using System;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;


namespace TestProject
{

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
        public void BeautySpa_Models_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.BeautySpa";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaType = assembly.GetType(typeName);
            Assert.IsNotNull(BeautySpaType);
        }

        // Test to Check BeautySpa Models Property Id Exists with correcct datatype int    
        [Test]
        public void BeautySpa_Id_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.BeautySpa";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BeautySpaType.GetProperty("Id");
            Assert.IsNotNull(propertyInfo, "Property Id does not exist in BeautySpa class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property Id in BeautySpa class is not of type int");
         }

        [Test]
        public void BeautySpa_Name_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.BeautySpa";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BeautySpaType.GetProperty("Name");
            Assert.IsNotNull(propertyInfo, "Property Name does not exist in BeautySpa class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Name in BeautySpa class is not of type string");
        }

        [Test]
        public void BeautySpa_Description_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.BeautySpa";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BeautySpaType.GetProperty("Description");
            Assert.IsNotNull(propertyInfo, "Property Name does not exist in BeautySpa class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Description in BeautySpa class is not of type string");
        }

        [Test]
        public void BeautySpa_Price_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.BeautySpa";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BeautySpaType.GetProperty("Price");
            Assert.IsNotNull(propertyInfo, "Property Name does not exist in BeautySpa class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(decimal), expectedType, "Property Price in BeautySpa class is not of type string");
        }

        [Test]
        public void BeautySpa_Price_PropertyExists_ReturnExpectedDataTypes_decimal()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.BeautySpa";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BeautySpaType.GetProperty("DurationInMinutes");
            Assert.IsNotNull(propertyInfo, "Property Name does not exist in BeautySpa class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property Price in BeautySpa class is not of type decimal");
        }

        [Test]
        public void BeautySpa_Category_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.BeautySpa";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BeautySpaType.GetProperty("Category");
            Assert.IsNotNull(propertyInfo, "Property Category does not exist in BeautySpa class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Category in BeautySpa class is not of type int");
        }

        [Test]
        public void BeautySpaController_Controllers_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.BeautySpaController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaControllerType = assembly.GetType(typeName);
            Assert.IsNotNull(BeautySpaControllerType);
         }

        [Test]
        public void BeautySpaController_View_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.BeautySpaController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = BeautySpaControllerType.GetMethod("View", );
            Assert.IsNotNull(methodInfo, "Method does not exist in BeautySpaController class");
        }

        [Test]
        public void BeautySpaController_ViewBeautySpas_MethodReturns_IActionResult()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.BeautySpaController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BeautySpaControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = BeautySpaControllerType.GetMethod("ViewBeautySpas");
            Assert.AreEqual(typeof(IActionResult), methodInfo.ReturnType, "Method ViewBeautySpas in BeautySpaController class is not of type IActionResult");
        }

//         [Test]
//         public void BeautySpaController_Create_MethodExists()
//         {
//             string assemblyName = "dotnetapp";
//             string typeName = "dotnetapp.Controllers.BeautySpaController";
//             Assembly assembly = Assembly.Load(assemblyName);
//             Type BeautySpaControllerType = assembly.GetType(typeName);
//             MethodInfo methodInfo = BeautySpaControllerType.GetMethod("Create", Type.EmptyTypes);
//             Assert.IsNotNull(methodInfo, "Method Create does not exist in BeautySpaController class");
//         }

//         [Test]
//         public void BeautySpaController_Create_Method_with_NoParams_Returns_IActionResult()
//         {
//             string assemblyName = "dotnetapp";
//             string typeName = "dotnetapp.Controllers.BeautySpaController";
//             Assembly assembly = Assembly.Load(assemblyName);
//             Type BeautySpaControllerType = assembly.GetType(typeName);
//             MethodInfo methodInfo = BeautySpaControllerType.GetMethod("Create", Type.EmptyTypes);
//             Assert.AreEqual(typeof(IActionResult), methodInfo.ReturnType, "Method Create in BeautySpaController class is not of type IActionResult");
//         }

//         [Test]
//         public void Create_in_BeautySpaController_Add_new_BeautySpa_to_DB()
//         {
//             string assemblyName = "dotnetapp";
//             Assembly assembly = Assembly.Load(assemblyName);
//             string modelTypeName = "dotnetapp.Models.BeautySpa";
//             string controllerTypeName = "dotnetapp.Controllers.BeautySpaController";
//             Type controllerType = assembly.GetType(controllerTypeName);
//             Type modelType = assembly.GetType(modelTypeName);

//             // Prepare data for creating a new BeautySpa
//             var BeautySpaData = new Dictionary<string, object>
//             {
//                 { "Name", "Sample BeautySpa" },
//                 { "Description", "Sample Description" },
//                 { "Category", "Sample Category" },
//                 { "Price", 10.99m },
//                 { "StockQuantity", 100 },
//                 { "ExpiryDate", DateTime.Now.AddDays(30) }
//             };

//             var BeautySpa = new BeautySpa();
//             foreach (var kvp in BeautySpaData)
//             {
//                 var propertyInfo = modelType.GetProperty(kvp.Key);
//                 if (propertyInfo != null)
//                 {
//                     propertyInfo.SetValue(BeautySpa, kvp.Value);
//                 }
//             }

//             // Invoke the Create method in the BeautySpaController
//             MethodInfo method = controllerType.GetMethod("Create", new[] { modelType });
//             if (method != null)
//             {
//                 // Create DbContextOptions using DbContextOptionsBuilder
//                 var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
//                     .UseInMemoryDatabase(databaseName: "TestDatabase");
//                 var options = optionsBuilder.Options;

//                 var dbContextInstance = new ApplicationDbContext(options);
//                 var controller = Activator.CreateInstance(controllerType, dbContextInstance);
//                 var result = method.Invoke(controller, new object[] { BeautySpa });

//                 // Validate the result
//                 var savedBeautySpa = dbContextInstance.BeautySpas.FirstOrDefault(p => p.Name == "Sample BeautySpa");
//                 Assert.IsNotNull(savedBeautySpa);
//                 Assert.AreEqual("Sample Description", savedBeautySpa.Description);
//                 // Add more assertions as needed

//                 // Optionally, clean up by deleting the added BeautySpa from the database
//                 dbContextInstance.BeautySpas.Remove(savedBeautySpa);
//                 dbContextInstance.SaveChanges();
//             }
//             else
//             {
//                 Assert.Fail("Create method not found in BeautySpaController");
//             }
//         }


 }
}