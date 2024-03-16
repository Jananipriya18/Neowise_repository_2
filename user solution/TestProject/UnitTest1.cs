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
            string typeName = "dotnetapp.Models.User"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type UserType = assembly.GetType(typeName); 
            Assert.IsNotNull(UserType); 
        } 

        [Test] 
        public void User_UserID_PropertyExists_ReturnExpectedDataTypes_int() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.User"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type UserType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = UserType.GetProperty("UserID"); 
            Assert.IsNotNull(propertyInfo, "Property UserID does not exist in User class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(int), expectedType, "Property UserID in User class is not of type int"); 
        } 

        [Test] 
        public void User_UserName_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.User"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type UserType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = UserType.GetProperty("UserName"); 
            Assert.IsNotNull(propertyInfo, "Property UserName does not exist in User class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property UserName in User class is not of type string"); 
        } 

        [Test] 
        public void User_Email_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.User"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type UserType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = UserType.GetProperty("Email"); 
            Assert.IsNotNull(propertyInfo, "Property Email does not exist in User class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property Email in User class is not of type string"); 
        } 

        [Test] 
        public void User_Password_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.User"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type UserType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = UserType.GetProperty("Password"); 
            Assert.IsNotNull(propertyInfo, "Property Password does not exist in User class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property Password in User class is not of type string"); 
        } 

        [Test] 
        public void User_DateOfBirth_PropertyExists_ReturnExpectedDataTypes_DateTime() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.User"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type UserType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = UserType.GetProperty("DateOfBirth"); 
            Assert.IsNotNull(propertyInfo, "Property DateOfBirth does not exist in User class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(DateTime), expectedType, "Property DateOfBirth in User class is not of type DateTime"); 
        } 

        [Test] 
        public void User_FirstName_PropertyExists_ReturnExpectedDataTypes_string() 
        { 

            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.User"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type UserType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = UserType.GetProperty("FirstName"); 
            Assert.IsNotNull(propertyInfo, "Property FirstName does not exist in User class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property FirstName in User class is not of type string"); 

        } 

        [Test] 
        public void User_LastName_PropertyExists_ReturnExpectedDataTypes_string() 
        { 

            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.User"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type UserType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = UserType.GetProperty("LastName"); 
            Assert.IsNotNull(propertyInfo, "Property LastName does not exist in User class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property LastName in User class is not of type string"); 

        } 

        [Test]
        public void User_UserID_PropertyHasKeyAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.User).GetProperty("UserID");
            Assert.IsNotNull(propertyInfo, "Property UserID does not exist in User class");
            var hasKeyAttribute = Attribute.IsDefined(propertyInfo, typeof(KeyAttribute));
            Assert.IsTrue(hasKeyAttribute, "Property UserID in User class does not have KeyAttribute");
        }

        [Test]
        public void User_FirstName_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.User).GetProperty("FirstName");
            Assert.IsNotNull(propertyInfo, "Property FirstName does not exist in User class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property FirstName in User class does not have RequiredAttribute");
        }

        [Test]
        public void User_LastName_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.User).GetProperty("LastName");
            Assert.IsNotNull(propertyInfo, "Property LastName does not exist in User class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property LastName in User class does not have RequiredAttribute");
        }

        [Test]
        public void User_DateOfBirth_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.User).GetProperty("DateOfBirth");
            Assert.IsNotNull(propertyInfo, "Property DateOfBirth does not exist in User class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property DateOfBirth in User class does not have RequiredAttribute");
        }

        [Test]
        public void User_UserName_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.User).GetProperty("UserName");
            Assert.IsNotNull(propertyInfo, "Property UserName does not exist in User class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property UserName in User class does not have RequiredAttribute");
        }

        [Test]
        public void User_Email_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.User).GetProperty("Email");
            Assert.IsNotNull(propertyInfo, "Property Email does not exist in User class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property Email in User class does not have RequiredAttribute");
        }

        [Test]
        public void User_Password_PropertyHasRequiredAttribute()
        {
            PropertyInfo propertyInfo = typeof(dotnetapp.Models.User).GetProperty("Password");
            Assert.IsNotNull(propertyInfo, "Property Password does not exist in User class");
            var hasRequiredAttribute = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));
            Assert.IsTrue(hasRequiredAttribute, "Property Password in User class does not have RequiredAttribute");
        }
    }
}


 

       

         

      

 

        
 

        