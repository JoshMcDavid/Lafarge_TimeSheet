namespace Lafarge_TimeSheet.Models
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime? ClockOut { get; set; }
    }
}
