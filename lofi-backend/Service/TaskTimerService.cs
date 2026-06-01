using lofi_backend.Repository;
using lofi_backend.Data_Models;

namespace lofi_backend.Service
{
    public interface ITaskTimerService
    {
        TaskTimer GetTimerByTimerId(int timerId);
        Task<TaskTimer> CreateNewTimer(TaskTimer taskTimer);
        Task<TaskTimer> EditTimer(TaskTimer timer);
        Task<TaskTimer> DeleteTimer(int timerId);
    }
    public class TaskTimerService : ITaskTimerService
    {
        private readonly ITaskTimerRepository _taskTimerRepository;


        public TaskTimerService(ITaskTimerRepository taskTimerRepository)
        {
            _taskTimerRepository = taskTimerRepository;
        }

        public TaskTimer GetTimerByTimerId(int timerId)
        {
            return _taskTimerRepository.GetTimerByTimerId(timerId);
        }

        public async Task <TaskTimer> CreateNewTimer(TaskTimer taskTimer)
        {
            return await _taskTimerRepository.CreateNewTimer(taskTimer);
        }

        public async Task<TaskTimer> EditTimer(TaskTimer taskTimer)
        {
            return await _taskTimerRepository.EditTimer(taskTimer);
        }

        public async Task<TaskTimer> DeleteTimer(int id)
        {
            return await _taskTimerRepository.DeleteTimer(id);
        }
    }
}
