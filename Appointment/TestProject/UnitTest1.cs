using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using dotnetapp.Models;
using dotnetapp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class AppointmentTests
    {
        private Type _appointmentControllerType;

        [SetUp]
        public void Setup()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _appointmentControllerType = assembly.GetType("dotnetapp.Controllers.AppointmentController");
        }

        [Test]
        public void Test_Index_Action()
        {
            var indexMethod = _appointmentControllerType.GetMethod("Index");
            var controllerInstance = Activator.CreateInstance(_appointmentControllerType);

            var result = indexMethod.Invoke(controllerInstance, null) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_Create_Get_Action()
        {
            var createGetMethod = _appointmentControllerType.GetMethod("Create", new Type[] { });
            var controllerInstance = Activator.CreateInstance(_appointmentControllerType);

            var result = createGetMethod.Invoke(controllerInstance, null) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_Create_Post_Action()
        {
            var createPostMethod = _appointmentControllerType.GetMethod("Create", new Type[] { typeof(Appointment) });
            var controllerInstance = Activator.CreateInstance(_appointmentControllerType);
            var appointment = new Appointment();

            var result = createPostMethod.Invoke(controllerInstance, new object[] { appointment }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void Test_Edit_Get_Action()
        {
            var editGetMethod = _appointmentControllerType.GetMethod("Edit", new Type[] { typeof(int) });
            var controllerInstance = Activator.CreateInstance(_appointmentControllerType);

            var result = editGetMethod.Invoke(controllerInstance, new object[] { 1 }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_Edit_Post_Action()
        {
            var editPostMethod = _appointmentControllerType.GetMethod("Edit", new Type[] { typeof(Appointment) });
            var controllerInstance = Activator.CreateInstance(_appointmentControllerType);
            var appointment = new Appointment();

            var result = editPostMethod.Invoke(controllerInstance, new object[] { appointment }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public void Test_Delete_Get_Action()
        {
            var deleteGetMethod = _appointmentControllerType.GetMethod("Delete", new Type[] { typeof(int) });
            var controllerInstance = Activator.CreateInstance(_appointmentControllerType);

            var result = deleteGetMethod.Invoke(controllerInstance, new object[] { 1 }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_DeleteConfirmed_Post_Action()
        {
            var deleteConfirmedPostMethod = _appointmentControllerType.GetMethod("DeleteConfirmed", new Type[] { typeof(int) });
            var controllerInstance = Activator.CreateInstance(_appointmentControllerType);

            var result = deleteConfirmedPostMethod.Invoke(controllerInstance, new object[] { 1 }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
    }
}
