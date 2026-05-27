namespace lofi_backend.Data_Models
{
    public class TaskTimer
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int Duration { get; set; } // Duration in seconds
        public bool IsActive { get; set; }
        public int ProjectId { get; set; }
    }
}
