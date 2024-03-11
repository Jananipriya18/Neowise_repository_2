 

using System.Numerics; 

using System.Reflection; 

using Microsoft.AspNetCore.Mvc; 

using NUnit.Framework; 

using Microsoft.Data.SqlClient; 

using System; 

using Microsoft.EntityFrameworkCore; 

using dotnetapp.Models; 

using System.Linq; 

 

 

 

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
    }
}

 

       

         

      

 

        
 

        