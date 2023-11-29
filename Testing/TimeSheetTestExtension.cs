using Lafarge_TimeSheet.Controllers;
using Lafarge_TimeSheet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public static class TimeSheetTestExtension
    {
        public static void SetTimeEntries(this TimeSheetController controller, List<TimeEntry> timeEntries)
        {
            var field = controller.GetType().GetField("timeEntries", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            if (field != null)
            {
                field.SetValue(controller, timeEntries);
            }
        }
    }
}
