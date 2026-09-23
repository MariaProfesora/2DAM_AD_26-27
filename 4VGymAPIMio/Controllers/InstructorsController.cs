using _4VGymAPI.Models;
using _4VGymAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("instructors")]
    public class InstructorsController : ControllerBase
    {
        private readonly InMemoryInstructorRepository _repository;

        // Inyectamos la clase concreta del repositorio
        public InstructorsController(InMemoryInstructorRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET /instructor
        /// GET /instructor?first_initial=M
        /// Devuelve el listado de monitores (filtrado opcionalmente por la inicial del nombre)
        /// </summary>
        /// <param name="firstInitial">Filtro opcional para la inicial del monitor</param>
        // Atiende a: GET /api/instructors/search?firstInitial=M
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Instructor>> FindInstructors([FromQuery(Name = "first_initial")] string? firstInitial)
        {
            try
            {
                IEnumerable<Instructor> instructors = _repository.GetByFirstInitial(firstInitial);

                return Ok(instructors);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }
      

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Instructor>> GetAll()
        {
            try
            {
                IEnumerable<Instructor> instructors = _repository.GetAll();
                return Ok(instructors);
            }
            catch (Exception ex)
            {
                // Devolvemos 500 si ocurre un fallo no previsto en el servidor
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponse(1, $"Error interno: {ex.Message}")
                );
            }
        }
    }
}
