using System;
using System.Reflection;
using NUnit.Framework;
using Employees.Controllers;
using Employees.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employees.Tests
{
	[TestFixture]
	public class EmployeeControllerTests
	{
        private const string ViewsPath = @"..\Employee"; // Update this path based on your project structure
        private Type _employeeControllerType;
		private Type _employeeType;
		private EmployeeController _employeeController;
		private List<Employee> _employees;
        private Assembly _mvcAssembly;


        [SetUp]
		public void Setup()
		{
            _mvcAssembly = typeof(Controller).Assembly;

            _employeeController = new EmployeeController();
			_employeeControllerType = typeof(EmployeeController);
			_employeeType = typeof(Employee);
			_employees = new List<Employee>
			{
				new Employee { Id = 1, Name = "John Doe", Email = "john@example.com", Dob = new DateTime(1990, 1, 15), Dept = "IT", Salary = 50000 },
				new Employee { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Dob = new DateTime(1985, 5, 10), Dept = "HR", Salary = 45000 }
			};
		}

		private MethodInfo GetMethodByName(EmployeeController controller, string methodName)
		{
			Type controllerType = controller.GetType();
			MethodInfo methodInfo = controllerType.GetMethod(methodName);

			Assert.IsNotNull(methodInfo, $"{methodName} method not found.");
			return methodInfo;
		}
		private Type FindTypeByName(string typeName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			foreach (Assembly assembly in assemblies)
			{
				Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);
				if (type != null)
				{
					return type;
				}
			}

			return null;
		}

		private MethodInfo GetCreateMethodInfo(EmployeeController controller)
		{
			return controller.GetType().GetMethod("Create", new[] { typeof(Employee) });
		}

		private MethodInfo GetMethodByNameWithParameters(EmployeeController controller, string methodName, params string[] parameterTypeNames)
		{
			Type controllerType = controller.GetType();

			Type[] parameterTypes = new Type[parameterTypeNames.Length];
			for (int i = 0; i < parameterTypeNames.Length; i++)
			{
				parameterTypes[i] = Type.GetType(parameterTypeNames[i]);
			}

			MethodInfo methodInfo = controllerType.GetMethod(methodName, parameterTypes);
			Assert.IsNotNull(methodInfo, $"{methodName} method not found.");
			return methodInfo;
		}
		[Test]
		public void Session2_EmployeeController_ClassName_ShouldBeCorrect()
		{
			Assert.AreEqual("EmployeeController", _employeeControllerType.Name);
		}

		[Test]
		public void Session2_Employee_ID_Property_ShouldHaveCorrectDataType()
		{
			var expectedProperties = new[]
			{
				new { Name = "Id", Type = Type.GetType("System.Int32") }
			};

			foreach (var property in expectedProperties)
			{
				var actualProperty = _employeeType.GetProperty(property.Name);
				Assert.IsNotNull(actualProperty);
				Assert.AreEqual(property.Type, actualProperty.PropertyType);
			}
		}

		[Test]
		public void Session2_Employee_Name_Property_ShouldHaveCorrectDataType()
		{
			var expectedProperties = new[]
			{
				new { Name = "Name", Type = Type.GetType("System.String") }

			};

			foreach (var property in expectedProperties)
			{
				var actualProperty = _employeeType.GetProperty(property.Name);
				Assert.IsNotNull(actualProperty);
				Assert.AreEqual(property.Type, actualProperty.PropertyType);
			}
		}
		[Test]
		public void Session2_Employee_Email_Property_ShouldHaveCorrectDataType()
		{
			var expectedProperties = new[]
			{
				new { Name = "Email", Type = Type.GetType("System.String") }
			};

			foreach (var property in expectedProperties)
			{
				var actualProperty = _employeeType.GetProperty(property.Name);
				Assert.IsNotNull(actualProperty);
				Assert.AreEqual(property.Type, actualProperty.PropertyType);
			}
		}
		[Test]
		public void Session2_Employee_DOB_Property_ShouldHaveCorrectDataType()
		{
			var expectedProperties = new[]
			{
				new { Name = "Dob", Type = Type.GetType("System.DateTime") },
			};

			foreach (var property in expectedProperties)
			{
				var actualProperty = _employeeType.GetProperty(property.Name);
				Assert.IsNotNull(actualProperty);
				Assert.AreEqual(property.Type, actualProperty.PropertyType);
			}
		}
		[Test]
		public void Session2_Employee_Dept_Property_ShouldHaveCorrectDataType()
		{
			var expectedProperties = new[]
			{
				new { Name = "Dept", Type = Type.GetType("System.String") },
			};

			foreach (var property in expectedProperties)
			{
				var actualProperty = _employeeType.GetProperty(property.Name);
				Assert.IsNotNull(actualProperty);
				Assert.AreEqual(property.Type, actualProperty.PropertyType);
			}
		}
		[Test]
		public void Session2_Employee_Salary_Property_ShouldHaveCorrectDataType()
		{
			var expectedProperties = new[]
			{
				new { Name = "Salary", Type = Type.GetType("System.Decimal") }
			};

			foreach (var property in expectedProperties)
			{
				var actualProperty = _employeeType.GetProperty(property.Name);
				Assert.IsNotNull(actualProperty);
				Assert.AreEqual(property.Type, actualProperty.PropertyType);
			}
		}
		[Test]
		public void Session2_Employee_Properties_ShouldHaveGettersAndSetters()
		{
			foreach (var property in _employeeType.GetProperties())
			{
				Assert.IsTrue(property.CanRead);
				Assert.IsTrue(property.CanWrite);
			}
		}

		[Test]
		public void Session2_Test_IndexMethod_Exists()
		{
			MethodInfo indexMethod = GetMethodByName(_employeeController, "Index");
			// Additional assertions or actions related to the method can be added here
		}

		

		[Test]
		public void Session2_Test_DetailsMethod_Exists()
		{
			MethodInfo detailsMethod = GetMethodByName(_employeeController, "Details");
			// Additional assertions or actions related to the method can be added here
		}

		[Test]
		public void Session2_Test_DeleteMethod_Exists()
		{
			MethodInfo deleteMethod = GetMethodByName(_employeeController, "Delete");
			// Additional assertions or actions related to the method can be added here
		}

		[Test]
		public void Session2_Test_CreatePostMethod_Exists()
		{
			MethodInfo createPostMethod = GetMethodByNameWithParameters(_employeeController, "Create");
			// Additional assertions or actions related to the method can be added here
		}

		[Test]
		public void Session2_Test_DeleteConfirmedPostMethod_Exists()
		{
			MethodInfo deleteConfirmedMethod = GetMethodByNameWithParameters(_employeeController, "DeleteConfirmed", "System.Int32");
			// Additional assertions or actions related to the method can be added here
		}
		
		
		

		[Test]
		public void Session2_Test_EmployeeClass_Exists()
		{
			Type employeeType = FindTypeByName("Employee");

			Assert.IsNotNull(employeeType, "Employee class not found.");
			Assert.AreEqual("Employees.Models", employeeType.Namespace);
		}

		[Test]
		public void Session3_Details_Get_ValidId_ReturnsViewResult()
		{
			// Arrange
			int employeeId = 1;

			// Act
			MethodInfo detailsMethod = GetMethodByName(_employeeController, "Details");
			ViewResult result = (ViewResult)detailsMethod.Invoke(_employeeController, new object[] { employeeId });

			// Assert
			Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData); // Check if the View property is not null
		}



		[Test]
		public void Session3_Index_Get_ReturnsViewResult()
		{
			// Arrange & Act
			MethodInfo indexMethod = GetMethodByName(_employeeController, "Index");
			ViewResult result = (ViewResult)indexMethod.Invoke(_employeeController, null);

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.ViewData);
		}

        [Test]
        public void Session3_TestEmployee_Views_Exists()
        {
            bool viewsFolderExists = Directory.Exists(@"/home/coder/project/workspace/Employees/Views/Employee");

            Assert.IsTrue(viewsFolderExists, "Employee does not exist.");
        }

        //[Test]
        //public void Create_Get_ReturnsViewResult()
        //{
        //	Arrange & Act
        //	MethodInfo createMethod = GetMethodByName(_employeeController, "Create");
        //	ViewResult result = (ViewResult)createMethod.Invoke(_employeeController, null);

        //	Assert
        //	Assert.IsNotNull(result);
        //	Assert.IsNotNull(result.ViewName);
        //}

        [Test]
		public void Session3_Edit_Get_ValidId_ReturnsViewResult()
		{
			// Arrange
			int employeeId = 1;

			// Act
			MethodInfo editMethod = GetMethodByNameWithParameters(_employeeController, "Edit", "System.Int32");
			ViewResult result = (ViewResult)editMethod.Invoke(_employeeController, new object[] { employeeId });

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.ViewData);
		}
		[Test]
		public void Session3_Delete_Get_ValidId_ReturnsViewResult()
		{
			// Arrange
			int employeeId = 1;

			// Act
			MethodInfo deleteMethod = GetMethodByNameWithParameters(_employeeController, "Delete", "System.Int32");
			ViewResult result = (ViewResult)deleteMethod.Invoke(_employeeController, new object[] { employeeId });

			// Assert
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.ViewData);
		}

		//[Test]
		//public void Views_AreCreatedForAllActions()
		//{
		//    Type controllerType = typeof(EmployeeController);
		//    var methods = controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public);

		//    foreach (var method in methods)
		//    {
		//        // Skip non-action methods
		//        if (!IsActionMethod(method))
		//            continue;

		//        string actionName = method.Name;
		//        string expectedViewName = GetExpectedViewName(actionName);

		//        // Assert that the view exists
		//        AssertViewExists(expectedViewName);
		//    }
		//}

		//private bool IsActionMethod(MethodInfo method)
		//{
		//    return method.IsPublic && !method.IsSpecialName && method.DeclaringType == typeof(EmployeeController);
		//}

		//private string GetExpectedViewName(string actionName)
		//{
		//	return $"Views/Employee/{actionName}.cshtml";
		//}

		//private void AssertViewExists(string viewName)
		//{
		//	bool viewExists = _mvcAssembly.GetManifestResourceNames().Any(rn => rn.EndsWith(viewName));
		//	Assert.IsTrue(viewExists, $"View '{viewName}' does not exist.");
		//}

		//[Test]
		//public void Views_AreCreatedForAllActions()
		//{
		//	Type controllerType = typeof(EmployeeController);
		//	MethodInfo[] methods = controllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public);

		//	foreach (MethodInfo method in methods)
		//	{
		//		if (IsActionMethod(method))
		//		{
		//			string actionName = method.Name;
		//			if (actionName != "Index" && actionName != "Delete" && actionName != "Details")
		//			{
		//				string expectedViewName = $"Views/Employee/{actionName}.cshtml";

		//				AssertViewExists(expectedViewName);
		//			}
		//		}
		//	}
		//}

		//private bool IsActionMethod(MethodInfo method)
		//{
		//	return method.IsPublic && !method.IsSpecialName && method.DeclaringType == typeof(EmployeeController);
		//}

		//private void AssertViewExists(string viewName)
		//{
		//	bool viewExists = _mvcAssembly.GetManifestResourceNames().Any(rn => rn.EndsWith(viewName));
		//	Assert.IsTrue(viewExists, $"View '{viewName}' does not exist.");
		//}


		//[TestCase("Index")]
		//[TestCase("Details")]
		//[TestCase("Create")]
		//[TestCase("Edit")]
		//[TestCase("Delete")]
		//public void ViewExists(string viewName)
		//{
		//	Arrange

		//	string viewPath = Path.Combine(ViewsPath, "Employee", $"{viewName}.cshtml");

		//	Assert

		//	Assert.IsTrue(File.Exists(viewPath), $"View '{viewName}.cshtml' does not exist.");
		//}


	}

	

	
}
