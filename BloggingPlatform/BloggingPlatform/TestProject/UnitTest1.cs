using System;
using System.Reflection;
using NUnit.Framework;
using BloggingPlatform.Models;
using BloggingPlatform.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Moq;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace BloggingPlatform.Tests
{
    [TestFixture]
    public class PostTests
    {
        private const string ViewsFolderPath = "Views";
        private const string PostViewsFolderPath = "D:\\Visual Studio\\W5_D1_S1_Client\\BloggingPlatform\\BloggingPlatform\\Views\\Post";
        private Type _postType;
        private Type controllerType;
        private Type _viewType;
        private Assembly _assembly1;
        private PostController _postcontroller;
        private List<Post> _fakePosts;


        [SetUp]
        public void Setup()
        {
            //_postcontroller = new PostController();
           
        }

        [TearDown]
        public void TearDown()
        {
            //_postcontroller = null;
        }

        private static MethodInfo GetMethod(Type type, string methodName, Type[] parameterTypes)
        {
            return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance, null, parameterTypes, null);
        }


        

        

        [Test]
        public void Session_3_Test_PostFolder_Exists()
        {
            bool viewsFolderExists = Directory.Exists("\\Views\\Post\\");

            Assert.IsTrue(viewsFolderExists, "Post folder does not exist.");
        }

        [Test]
        public void Session_3_Test_IndexViewFile_Exists()
        {
            string indexPath = Path.Combine(PostViewsFolderPath, "Inde.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Index.cshtml view file does not exist.");
        }
        [Test]
        public void Session_3_Test_CreateViewFile_Exists()
        {
            string indexPath = Path.Combine(PostViewsFolderPath, "Create.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Create.cshtml view file does not exist.");
        }
        [Test]
        public void Session_3_Test_DeleteViewFile_Exists()
        {
            string indexPath = Path.Combine(PostViewsFolderPath, "Delete.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Delete.cshtml view file does not exist.");
        }
        [Test]
        public void Session_3_Test_DetailsViewFile_Exists()
        {
            string indexPath = Path.Combine(PostViewsFolderPath, "Details.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Details.cshtml view file does not exist.");
        }

        [Test]
        public void Session_3_Test_EditViewFile_Exists()
        {
            string indexPath = Path.Combine(PostViewsFolderPath, "Edit.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Edit.cshtml view file does not exist.");
        }






        [Test]
        public void Session_2_Test_Create_Action()
        {
            //var controllerType = typeof(PostController);
            Assembly assembly = Assembly.Load("BloggingPlatform");
            controllerType = assembly.GetType("BloggingPlatform.Controllers.PostController");
            var createMethod = GetMethod(controllerType, "Create", new Type[] { typeof(Post) });

            Assert.NotNull(createMethod);
        }

        [Test]
        public void Session_2_Test_Edit_Action()
        {
            Assembly assembly = Assembly.Load("BloggingPlatform");
            controllerType = assembly.GetType("BloggingPlatform.Controllers.PostController"); 
            var editMethod = GetMethod(controllerType, "Edit", new Type[] { typeof(int), typeof(Post) });

            Assert.NotNull(editMethod);
            //Assert.IsTrue(editMethod.IsPublic);
            //Assert.AreEqual(typeof(ViewResult), editMethod.ReturnType);
            // Add more assertions if needed
        }

        [Test]
        public void Session_2_Test_Details_Action()
        {
            Assembly assembly = Assembly.Load("BloggingPlatform");
            controllerType = assembly.GetType("BloggingPlatform.Controllers.PostController");
            var detailsMethod = GetMethod(controllerType, "Details", new Type[] { typeof(int) });

            Assert.NotNull(detailsMethod);
            //Assert.IsTrue(detailsMethod.IsPublic);
            //Assert.AreEqual(typeof(ViewResult), detailsMethod.ReturnType);
            // Add more assertions if needed
        }

        [Test]
        public void Session_2_Test_Delete_Action()
        {
            Assembly assembly = Assembly.Load("BloggingPlatform");
            controllerType = assembly.GetType("BloggingPlatform.Controllers.PostController");
            var deleteMethod = GetMethod(controllerType, "Delete", new Type[] { typeof(int) });

            Assert.NotNull(deleteMethod);
            //Assert.IsTrue(deleteMethod.IsPublic);
            //Assert.AreEqual(typeof(RedirectToActionResult), deleteMethod.ReturnType);
            // Add more assertions if needed
        }

        [Test]
        public void Session_1_TestPost_ClassExists()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("BloggingPlatform");

            // Get the type by name (full name with namespace)
            Type postType = assembly.GetType("BloggingPlatform.Models.Post");

            Assert.NotNull(postType, "Post class does not exist.");
            Console.WriteLine(postType);
        }

        [Test]
        public void Session_1_TestIdPropertyType()
        {
            Assembly assembly = Assembly.Load("BloggingPlatform");
            _postType = assembly.GetType("BloggingPlatform.Models.Post");
            PropertyInfo idProperty = _postType.GetProperty("Id");
            Assert.NotNull(idProperty, "Id property does not exist.");
            Assert.AreEqual(typeof(int), idProperty.PropertyType, "Id property should be of type int.");
        }

        [Test]
        public void Session_1_TestTitlePropertyType()
        {
            Assembly assembly = Assembly.Load("BloggingPlatform");
            _postType = assembly.GetType("BloggingPlatform.Models.Post");
            PropertyInfo titleProperty = _postType.GetProperty("Title");
            Assert.NotNull(titleProperty, "Title property does not exist.");
            Assert.AreEqual(typeof(string), titleProperty.PropertyType, "Title property should be of type string.");
        }

        [Test]
        public void Session_1_TestContentPropertyType()
        {
            Assembly assembly = Assembly.Load("BloggingPlatform");
            _postType = assembly.GetType("BloggingPlatform.Models.Post");
            PropertyInfo contentProperty = _postType.GetProperty("Content");
            Assert.NotNull(contentProperty, "Content property does not exist.");
            Assert.AreEqual(typeof(string), contentProperty.PropertyType, "Content property should be of type string.");
        }

        [Test]
        public void Session_1_TestAuthorPropertyType()
        {
            Assembly assembly = Assembly.Load("BloggingPlatform");
            _postType = assembly.GetType("BloggingPlatform.Models.Post");
            PropertyInfo authorProperty = _postType.GetProperty("Author");
            Assert.NotNull(authorProperty, "Author property does not exist.");
            Assert.AreEqual(typeof(string), authorProperty.PropertyType, "Author property should be of type string.");
        }

        [Test]
        public void Session_1_TestPublishDatePropertyType()
        {
            Assembly assembly = Assembly.Load("BloggingPlatform");
            _postType = assembly.GetType("BloggingPlatform.Models.Post");
            PropertyInfo publishDateProperty = _postType.GetProperty("PublishDate");
            Assert.NotNull(publishDateProperty, "PublishDate property does not exist.");
            Assert.AreEqual(typeof(DateTime), publishDateProperty.PropertyType, "PublishDate property should be of type DateTime.");
        }
    }
}
