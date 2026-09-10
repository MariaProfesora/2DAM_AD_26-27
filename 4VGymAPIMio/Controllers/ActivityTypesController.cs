using _4VGymAPI.Models;
using _4VGymAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymAPI.Controllers
{
    [ApiController] //  Activa comportamientos automáticos de API (como validaciones de modelo y respuestas de error estandarizadas).
    [Route("activity-types")] // Define la URL base para este controlador
    public class ActivityTypesController : ControllerBase
    {
        private readonly InMemoryActivityTypeRepository _repository;

        // Inyectamos la clase concreta del repositorio
        public ActivityTypesController(InMemoryActivityTypeRepository repository)
        {
            _repository = repository;
        }
        // Un get sencillo podría ser el siguiente(solo puede haber 1 método GET por controlador) :
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var activityTypes = _repository.GetAll();
        //    return Ok(activityTypes); // Devuelve un código HTTP 200 OK y convierte automáticamente la lista de C# a un formato JSON para el cliente.
        //}
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
                IEnumerable<ActivityType> activityTypes = _repository.GetAll();
                if (activityTypes == null || activityTypes.Count() == 0)
                {
                    return BadRequest(new ErrorResponse { Code = 1, Description = "No activity types found" });
                }
                return Ok(activityTypes);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(2, "Any problem in the Server" ));
            }
        }
    }

}
