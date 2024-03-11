using System.Reflection; 
using Microsoft.AspNetCore.Mvc; 
using NUnit.Framework; 
using Microsoft.Data.SqlClient; 
using System; 
using Microsoft.EntityFrameworkCore; 
using dotnetapp.Models; 
// using System.Linq; 

namespace TestProject 

{ 
    public class Tests 
    { 
        [Test] 
        public void Member_Models_ClassExists() 

        { 
            string assemblyName = "dotnetapp"; 
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type MemberType = assembly.GetType(typeName); 
            Assert.IsNotNull(MemberType); 
        } 

        [Test] 
        public void Member_MemberId_PropertyExists_ReturnExpectedDataTypes_int() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type MemberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = MemberType.GetProperty("MemberId"); 
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
            Type MemberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = MemberType.GetProperty("Firstname"); 
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
            Type MemberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = MemberType.GetProperty("LastName"); 
            Assert.IsNotNull(propertyInfo, "Property LastName does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property LastName in Member class is not of type string"); 
        } 

        [Test] 
        public void Member_Password_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type MemberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = MemberType.GetProperty("Password"); 
            Assert.IsNotNull(propertyInfo, "Property Password does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property Password in Member class is not of type string"); 
        } 

        [Test] 
        public void Member_DateOfBirth_PropertyExists_ReturnExpectedDataTypes_DateTime() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Member"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type MemberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = MemberType.GetProperty("DateOfBirth"); 
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
            Type MemberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = MemberType.GetProperty("Address"); 
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
            Type MemberType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = MemberType.GetProperty("Email"); 
            Assert.IsNotNull(propertyInfo, "Property Email does not exist in Member class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property Email in Member class is not of type string"); 

        } 
    }
}

 

       

         

      

 

        
 

        