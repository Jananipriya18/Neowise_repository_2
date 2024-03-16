using System.Reflection; 
using Microsoft.AspNetCore.Mvc; 
using NUnit.Framework; 
using Microsoft.Data.SqlClient; 
using System; 
using Microsoft.EntityFrameworkCore; 
using System.Linq; 
using System.ComponentModel.DataAnnotations;

namespace TestProject 

{ 
    public class Tests 
    { 
        [Test] 
        public void User_Models_ClassExists() 

        { 
            string assemblyName = "dotnetapp"; 
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type UserType = assembly.GetType(typeName); 
            Assert.IsNotNull(UserType); 
        } 

        [Test] 
        public void Member_MemberId_PropertyExists_ReturnExpectedDataTypes_int() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type memberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = memberType.GetProperty("MemberId"); 
            Assert.IsNotNull(propertyInfo, "Property MemberId does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(int), expectedType, "Property MemberId in Member class is not of type int"); 
        } 

        [Test] 
        public void Member_Firstname_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type memberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = memberType.GetProperty("Firstname"); 
            Assert.IsNotNull(propertyInfo, "Property Firstname does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property Firstname in Member class is not of type string"); 
        } 

        [Test] 
        public void Member_LastName_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type memberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = memberType.GetProperty("LastName"); 
            Assert.IsNotNull(propertyInfo, "Property LastName does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property LastName in Member class is not of type string"); 
        } 

        [Test] 
        public void Member_DateOfBirth_PropertyExists_ReturnExpectedDataTypes_DateTime() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type memberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = memberType.GetProperty("DateOfBirth"); 
            Assert.IsNotNull(propertyInfo, "Property DateOfBirth does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(DateTime), expectedType, "Property DateOfBirth in Member class is not of type DateTime"); 
        } 

        [Test] 
        public void Member_Address_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type memberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = memberType.GetProperty("Address"); 
            Assert.IsNotNull(propertyInfo, "Property Address does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property Address in Member class is not of type string"); 
        } 

        [Test] 
        public void Member_Email_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type memberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = memberType.GetProperty("Email"); 
            Assert.IsNotNull(propertyInfo, "Property Email does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property Email in Member class is not of type string"); 
        } 

        [Test] 
        public void Member_RegistrationDate_PropertyExists_ReturnExpectedDataTypes_DateTime() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type memberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = memberType.GetProperty("RegistrationDate"); 
            Assert.IsNotNull(propertyInfo, "Property RegistrationDate does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(DateTime), expectedType, "Property RegistrationDate in Member class is not of type DateTime"); 
        } 

        [Test] 
        public void Member_InitialPayment_PropertyExists_ReturnExpectedDataTypes_Decimal() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type memberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = memberType.GetProperty("InitialPayment"); 
            Assert.IsNotNull(propertyInfo, "Property InitialPayment does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(decimal), expectedType, "Property InitialPayment in Member class is not of type decimal"); 
        } 


       [Test]
        public void Member_UserID_PropertyHasKeyAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.Member).GetProperty("MemberId");
            Assert.IsNotNull(propertyInfo, "Property MemberId does not exist in Member class");
            var hasKeyAttribute = Attribute.IsDefined(propertyInfo, typeof(KeyAttribute));
            Assert.IsTrue(hasKeyAttribute, "Property MemberId in Member class does not have KeyAttribute");
        }

        [Test]
        public void Member_FirstName_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.Member).GetProperty("Firstname");
            Assert.IsNotNull(propertyInfo, "Property Firstname does not exist in Member class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property Firstname in Member class does not have RequiredAttribute");
        }

        [Test]
        public void Member_LastName_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.Member).GetProperty("LastName");
            Assert.IsNotNull(propertyInfo, "Property LastName does not exist in Member class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property LastName in Member class does not have RequiredAttribute");
        }

        [Test]
        public void Member_DateOfBirth_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.Member).GetProperty("DateOfBirth");
            Assert.IsNotNull(propertyInfo, "Property DateOfBirth does not exist in Member class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property DateOfBirth in Member class does not have RequiredAttribute");
        }

        [Test]
        public void Member_Address_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.Member).GetProperty("Address");
            Assert.IsNotNull(propertyInfo, "Property Address does not exist in Member class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property Address in Member class does not have RequiredAttribute");
        }

        [Test]
        public void Member_Email_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.Member).GetProperty("Email");
            Assert.IsNotNull(propertyInfo, "Property Email does not exist in Member class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property Email in Member class does not have RequiredAttribute");
        }

        [Test]
        public void Member_RegistrationDate_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.Member).GetProperty("RegistrationDate");
            Assert.IsNotNull(propertyInfo, "Property RegistrationDate does not exist in Member class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property RegistrationDate in Member class does not have RequiredAttribute");
        }

        [Test]
        public void Member_InitialPayment_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.Member).GetProperty("InitialPayment");
            Assert.IsNotNull(propertyInfo, "Property InitialPayment does not exist in Member class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property InitialPayment in Member class does not have RequiredAttribute");
        }
    }
}


 

       

         

      

 

        
 

        