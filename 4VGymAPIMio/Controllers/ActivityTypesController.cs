using _4VGymAPI.Models;
using _4VGymAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("activity-types")]
    public class ActivityTypesController : ControllerBase
    {
        private readonly InMemoryActivityTypeRepository _repository;

        // Inyectamos la clase concreta del repositorio
        public ActivityTypesController(InMemoryActivityTypeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET /activity-types
        /// Devuelve el listado de tipos de actividades de 4VGym.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActivityType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ActivityType>> FindActivityTypes()
        // ActionResult<T>: Tipo de retorno flexible de Web API. Permite devolver directamente el tipo de dato T (que ASP.NET Core convierte automáticamente a JSON con un código 200 OK) o respuestas con estado HTTP específico (como NotFound(), BadRequest(), etc.).
        {
            try
            {
                var activityTypes = _repository.GetAll();
                if (activityTypes == null || !activityTypes.Any())
                {
                    return NotFound(new { code = 404, description = "No activity types found" });
                })
                return Ok(activityTypes);
            }
            catch (Exception)
            {
                return BadRequest(new { code = 400, description = "Any problem in the Server" });
            }
        }
    }

}
