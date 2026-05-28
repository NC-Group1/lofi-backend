using lofi_backend.Data_Models;
using lofi_backend.Service;
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
        public IActionResult GetUser(int id)
        {
            try
            {
                return Ok(_service.GetUser(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                var newUser = _service.CreateUser(user);
                return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult EditUser([FromBody] User user)
        {
            try
            {
                return Ok(_service.EditUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest("User id must be greater than zero.");
            }

            User removeUser = _service.RemoveUser(id);


            if (removeUser == null)
            {
                return NotFound($"User with id {id} was not found.");

            }
            return NoContent();
        }

    }
}
