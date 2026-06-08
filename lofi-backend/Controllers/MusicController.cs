using Microsoft.AspNetCore.Mvc;
using lofi_backend.Data_Models;
using lofi_backend.Models;
using lofi_backend.Service;

namespace lofi_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusicController : ControllerBase
    {

        private readonly IMusicService _service;
        public MusicController(IMusicService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllMusics()
        {
            try
            {
                var result = _service.GetAllMusics();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult GetMusicById(int id)
        {
            try
            {
                var result = _service.GetMusicById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateMusic([FromBody] Music music)
        {
            try
            {
                var newMusic = _service.CreateMusic(music);
                return Ok(newMusic);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        [HttpDelete("{id}")]
        public IActionResult RemoveMusic(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id");
            }

            Music deletedMusic = _service.RemoveMusic(id);

            if (deletedMusic == null)
            {
                return NotFound("Music not found");
            }
            return NoContent();
        }
    }
}
