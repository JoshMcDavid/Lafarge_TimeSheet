using Lafarge_TimeSheet.Controllers;
using Lafarge_TimeSheet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    [TestClass]
    public class TimeSheetControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewResult_WithCorrectModel()
        {
            
            var controller = new TimeSheetController();
            var userName = "testUser";
            var timeEntries = new List<TimeEntry>
            {
                new TimeEntry { UserName = userName, ClockIn = DateTime.UtcNow }
            };
            controller.SetTimeEntries(timeEntries);

            
            var result = controller.Index(userName) as ViewResult;

           
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<TimeEntry>));
            var model = (List<TimeEntry>)result.Model;
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(userName, model[0].UserName);
        }

        [TestMethod]
        public void ClockIn_GET_ReturnsViewResult_WithCorrectModel()
        {
           
            var controller = new TimeSheetController();
            var userName = "testUser";

            
            var result = controller.ClockIn(userName) as ViewResult;

            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(TimeEntry));
            var model = (TimeEntry)result.Model;
            Assert.AreEqual(userName, model.UserName);
            Assert.IsNotNull(model.ClockIn);
        }

        [TestMethod]
        public void ClockIn_POST_AddsTimeEntry_RedirectsToIndex()
        {
            
            var controller = new TimeSheetController();
            var timeEntry = new TimeEntry { UserName = "testUser", ClockIn = DateTime.UtcNow };

            
            var result = controller.ClockIn(timeEntry) as RedirectToActionResult;

            
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual(timeEntry.UserName, result.RouteValues["userName"]);
        }

        [TestMethod]
        public void ClockOut_GET_ReturnsViewResult_WithCorrectModel()
        {
            
            var controller = new TimeSheetController();
            var timeEntry = new TimeEntry { Id = 1, UserName = "testUser", ClockIn = DateTime.UtcNow };
            controller.SetTimeEntries(new List<TimeEntry> { timeEntry });

            
            var result = controller.ClockOut(timeEntry.Id, timeEntry.UserName) as ViewResult;

            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(TimeEntry));
            var model = (TimeEntry)result.Model;
            Assert.AreEqual(timeEntry.UserName, model.UserName);
            Assert.AreEqual(timeEntry.Id, model.Id);
        }

        [TestMethod]
        public void ClockOut_POST_UpdatesTimeEntry_RedirectsToIndex()
        {
           
            var controller = new TimeSheetController();
            var timeEntry = new TimeEntry { Id = 1, UserName = "testUser", ClockIn = DateTime.UtcNow };
            controller.SetTimeEntries(new List<TimeEntry> { timeEntry });

            
            var result = controller.ClockOut(timeEntry.Id, timeEntry.UserName, null) as RedirectToActionResult;

            
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual(timeEntry.UserName, result.RouteValues["userName"]);
            Assert.IsNotNull(timeEntry.ClockOut);
        }
    }
}
