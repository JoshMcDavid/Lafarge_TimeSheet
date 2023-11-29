using Lafarge_TimeSheet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lafarge_TimeSheet.Controllers
{
    public class TimeSheetController : Controller
    {
        private static List<TimeEntry> timeEntries = new List<TimeEntry>();

        public ActionResult Index(string userName)
        {
            var userTimeEntries = timeEntries.Where(t => t.UserName == userName).ToList();
            return View(userTimeEntries);
        }

        [HttpGet]
        public ActionResult ClockIn(string userName)
        {
            return View(new TimeEntry { UserName = userName, ClockIn = DateTime.UtcNow });
        }

        [HttpPost]
        public ActionResult ClockIn(TimeEntry timeEntry)
        {
            timeEntries.Add(timeEntry);
            return RedirectToAction("Index", new { userName = timeEntry.UserName });
        }

        [HttpGet]
        public ActionResult ClockOut(int id, string userName)
        {
            var timeEntry = timeEntries.FirstOrDefault(t => t.Id == id && t.UserName == userName);
            return View(timeEntry);
        }

        [HttpPost]
        public ActionResult ClockOut(int id, string userName, IFormCollection form)
        {
            var timeEntry = timeEntries.FirstOrDefault(t => t.Id == id && t.UserName == userName);
            if (timeEntry != null)
            {
                timeEntry.ClockOut = DateTime.UtcNow;
            }
            return RedirectToAction("Index", new { userName = userName });
        }

        [HttpPost]
        public ActionResult ClockOutFromIndex(string username, IFormCollection form)
        {
            var latestTimeEntry = timeEntries
                .Where(t => t.UserName == username && !t.ClockOut.HasValue)
                .OrderByDescending(t => t.ClockIn)
                .FirstOrDefault();

            if (latestTimeEntry != null)
            {
                latestTimeEntry.ClockOut = DateTime.UtcNow;
            }

            return RedirectToAction("Index", new { userName = username });
        }
    }
}
