using Lafarge_TimeSheet.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TimeSheetControllerTest
    {
        [TestMethod]
        public void Index_ReturnsViewWithUserTimeEntries()
        {
            // Arrange
            var controller = new TimeSheetController();
            var userName = "testUser";
            var timeEntry = new TimeEntry { UserName = userName };
            TimeSheetController.timeEntries.Add(timeEntry);

            // Act
            var result = controller.Index(userName) as ViewResult;
            var model = result.Model as List<TimeEntry>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userName, result.ViewBag.UserName);
            CollectionAssert.Contains(model, timeEntry);
        }

        [TestMethod]
        public void ClockIn_Get_ReturnsClockInView()
        {
            // Arrange
            var controller = new TimeSheetController();
            var userName = "testUser";

            // Act
            var result = controller.ClockIn(userName) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userName, result.ViewBag.UserName);
        }

        [TestMethod]
        public void ClockIn_Post_AddsTimeEntryAndRedirectsToIndex()
        {
            // Arrange
            var controller = new TimeSheetController();
            var userName = "testUser";
            var timeEntry = new TimeEntry { UserName = userName };

            // Act
            var result = controller.ClockIn(timeEntry) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(userName, result.RouteValues["userName"]);
            CollectionAssert.Contains(TimeSheetController.timeEntries, timeEntry);
        }

        [TestMethod]
        public void ClockOut_Get_ReturnsClockOutView()
        {
            // Arrange
            var controller = new TimeSheetController();
            var userName = "testUser";
            var timeEntry = new TimeEntry { UserName = userName };

            // Act
            var result = controller.ClockOut(timeEntry.Id, userName) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(timeEntry, result.Model);
        }

        [TestMethod]
        public void ClockOut_Post_UpdatesClockOutAndRedirectsToIndex()
        {
            // Arrange
            var controller = new TimeSheetController();
            var userName = "testUser";
            var timeEntry = new TimeEntry { Id = 1, UserName = userName };
            TimeSheetController.timeEntries.Add(timeEntry);

            // Act
            var result = controller.ClockOut(timeEntry.Id, userName, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(userName, result.RouteValues["userName"]);
            Assert.IsNotNull(timeEntry.ClockOut);
        }
    }
}
