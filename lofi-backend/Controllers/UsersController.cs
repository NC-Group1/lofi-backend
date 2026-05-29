using lofi_backend.Data_Models;
using lofi_backend.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace lofi_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = _service.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsync(string id, string password)
        {
            try
            {
                var result = await _service.GetUserAsync(id, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserWithPassword user)
        {
            try
            {
                var newUser = await _service.CreateUser(user);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult EditUser([FromBody] UserData user)
        {
            try
            {
                var result = _service.EditUser(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveUser(string id)
        {
            if (id == "")
            {
                return BadRequest("User id must be greater than zero.");
            }

            UserData removeUser = _service.RemoveUser(id);


            if (removeUser == null)
            {
                return NotFound($"User with id {id} was not found.");

            }
            return NoContent();
        }

    }
}
