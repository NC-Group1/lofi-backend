namespace lofi_backend.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TaskTimer> Timers { get; set; } = new List<TaskTimer>();
        public string UserId { get; set; }
    }
}
