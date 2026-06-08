namespace lofi_backend.Models
{
    public class TaskTimer
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Duration { get; set; } // Duration in seconds
        public bool IsActive { get; set; }
        public int ProjectId { get; set; }

        public TaskTimer()
        {
            Id = 0;
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            Duration = 0;
            IsActive = false;
            ProjectId = 0;
        }
    }
}
