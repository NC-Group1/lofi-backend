using lofi_backend.Data_Models;
using lofi_backend.Service;
using Microsoft.AspNetCore.Mvc;

namespace lofi_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskTimersController : ControllerBase
    {
        private readonly ITaskTimerService _taskTimerService;

        public TaskTimersController(ITaskTimerService taskTimerService)
        {
            _taskTimerService = taskTimerService;
        }

        [HttpGet("{id}")]
        public IActionResult GetTimerByTimerId(int id)
        {
            try
            {
                var result = _taskTimerService.GetTimerByTimerId(id);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                if (id <= 0)
                { 
                    return BadRequest(ex.Message); 
                }
                else
                {
                    return NotFound("Timer not found");
                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTimer([FromBody]TaskTimer taskTimer)
        {
            try
            {
                var newTimer = await _taskTimerService.CreateNewTimer(taskTimer);
                return Ok(newTimer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditTimer([FromBody] TaskTimer timer)
        {
            try
            {
                var result = _taskTimerService.EditTimer(timer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimer(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Timer id must be greater than zero.");
            }
            var deleteTimer = _taskTimerService.DeleteTimer(id);
            if(deleteTimer == null)
            {
                return NotFound($"Timer with id {id} was not found.");
            }

            return NoContent();
        }

        [HttpGet("Project/{ProjectId}")]
        public async Task<IActionResult> GetTimerByProjectId(int projectId)
        {
            try
            {
                var result = _taskTimerService.GetTimerByTimerId(projectId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (projectId <= 0)
                {
                    return BadRequest(ex.Message);
                }
                else
                {
                    return NotFound("Project not found by projectId");
                }
            }
        }
    }
}
