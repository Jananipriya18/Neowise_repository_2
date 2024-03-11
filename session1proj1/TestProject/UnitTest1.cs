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
        public void Comment_Models_ClassExists() 

        { 
            string assemblyName = "dotnetapp"; 
            string typeName = "dotnetapp.Models.Comment"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type CommentType = assembly.GetType(typeName); 
            Assert.IsNotNull(CommentType); 
        } 

        [Test] 
        public void Comment_CommentID_PropertyExists_ReturnExpectedDataTypes_int() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Comment"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type CommentType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = CommentType.GetProperty("CommentID"); 
            Assert.IsNotNull(propertyInfo, "Property CommentID does not exist in Comment class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(int), expectedType, "Property CommentID in Comment class is not of type int"); 
        } 

        [Test] 
        public void Comment_UserName_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Comment"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type CommentType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = CommentType.GetProperty("UserName"); 
            Assert.IsNotNull(propertyInfo, "Property UserName does not exist in Comment class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property UserName in Comment class is not of type string"); 
        } 

        [Test] 
        public void Comment_Location_PropertyExists_ReturnExpectedDataTypes_string() 
        { 
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Comment"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type CommentType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = CommentType.GetProperty("Location"); 
            Assert.IsNotNull(propertyInfo, "Property Location does not exist in Comment class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property Location in Comment class is not of type string"); 
        } 

        [Test] 
        public void Comment_Content_PropertyExists_ReturnExpectedDataTypes_string() 
        { 

            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Comment"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type CommentType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = CommentType.GetProperty("Content"); 
            Assert.IsNotNull(propertyInfo, "Property Content does not exist in Comment class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(string), expectedType, "Property Content in Comment class is not of type string"); 

        } 

        [Test] 
        public void Comment_CreatedAt_PropertyExists_ReturnExpectedDataTypes_DateTime() 
        { 

            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Comment"; 
            Assembly assembly = Assembly.Load(assemblyName); 
            Type CommentType = assembly.GetType(typeName); 
            PropertyInfo propertyInfo = CommentType.GetProperty("Email"); 
            Assert.IsNotNull(propertyInfo, "Property CreatedAt does not exist in Comment class"); 
            Type expectedType = propertyInfo.PropertyType; 
            Assert.AreEqual(typeof(DateTime), expectedType, "Property CreatedAt in Comment class is not of type DateTime); 

        } 
    }
}