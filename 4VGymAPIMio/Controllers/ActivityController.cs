using _4VGymAPI.Models;
using _4VGymAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController]
    [Route("activities")]
    public class ActivityController : ControllerBase
    {

        private readonly InMemoryActivityRepository _repository;

        // Inyectamos la clase concreta del repositorio
        public ActivityController(InMemoryActivityRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// POST /activities
        /// Añade una nueva actividad en 4VGym.
        /// </summary>
        /// <param name="activityNew">ObjetoActivity enviado en el cuerpo de la petición (JSON).</param>
        [HttpPost]
        [ProducesResponseType(typeof(ActivityNew), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<Activity> CreateActivity([FromBody] ActivityNew activityNew)
        {
            try
            {
                Activity? activityCreated = _repository.Add(activityNew, out List<ErrorResponse> errors);
                
                // En caso de errores de validacion del repositorio
                if (activityCreated == null && errors.Count > 0)
                {
                    return BadRequest(errors);
                }

                // EN caso de OK
                return StatusCode(StatusCodes.Status201Created, activityCreated);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse(1, "An internal server error occurred"));

            }
        }
       

    }
}
