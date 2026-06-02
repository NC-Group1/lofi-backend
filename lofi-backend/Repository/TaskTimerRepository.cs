using lofi_backend.Database;
using lofi_backend.Data_Models;
using Microsoft.EntityFrameworkCore;

namespace lofi_backend.Repository
{
    public interface ITaskTimerRepository
    {
        TaskTimer GetTimerByTimerId(int timerId);
        Task<TaskTimer> CreateNewTimer(TaskTimer taskTimer);
        Task<TaskTimer> EditTimer(TaskTimer timer);
        Task<TaskTimer> DeleteTimer(int timerId);
        Task<List<TaskTimer>> GetAllTimersByProjectId(int projectId);
    }
    public class TaskTimerRepository : ITaskTimerRepository
    {
        private readonly LoFiDbContext _db;
        public TaskTimerRepository(LoFiDbContext dbContext)
        {
            _db = dbContext;
        }
        
        
        public TaskTimer GetTimerByTimerId(int timerId)
        {
            return _db.Timers.ToList().First(t => t.Id == timerId) ?? throw new Exception("Timer not found");
        }

        
        public async Task<TaskTimer> CreateNewTimer(TaskTimer taskTimer)
        {
            if (_db.Timers.Contains(taskTimer)) throw new Exception("Timer already exists");
            await _db.Timers.AddAsync(taskTimer);
            await _db.SaveChangesAsync();
            return taskTimer;
        }

        
        public async Task<TaskTimer> EditTimer(TaskTimer timer)
        {
            if (!_db.Timers.Contains(timer)) throw new Exception("Timer doesn't exist");

            var editTimer = _db.Timers.Update(timer).Entity;
            await _db.SaveChangesAsync();
            return editTimer;
        }

        
        public async Task <TaskTimer> DeleteTimer(int timerId)
        {
            var deletedTimer = _db.Timers.First(t => t.Id == timerId);
            if (deletedTimer == null)
            {
                throw new Exception("User does not exist");
            }

            _db.Timers.Remove(deletedTimer);

            await _db.SaveChangesAsync();

            return deletedTimer;

        }

        public async Task<List<TaskTimer>> GetAllTimersByProjectId(int projectId)
        {
            var timers = await _db.Timers.Where(t => t.ProjectId == projectId).ToListAsync();
            timers.ForEach(Console.WriteLine);

            if (timers == null)
            { 
                throw new Exception("No Project Found"); 
            }
            
            return timers;
        }

    }
}
