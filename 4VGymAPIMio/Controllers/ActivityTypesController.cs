using _4VGymAPI.Models;
using _4VGymAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

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
    {
        try
        {
            IEnumerable<ActivityType> activityTypes = _repository.GetAll();

            if (activityTypes == null || activityTypes.Count() == 0)
            {
                return BadRequest(new ErrorResponse(2, "No activity types found"));
            }

            return Ok(activityTypes);
        }
        catch (Exception)
        {
            return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
        }
    }
}