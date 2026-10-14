using _4VGymAPI.Models;
using _4VGymAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq; // si no está ya
using System.Text.RegularExpressions;

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

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Instructor>> GetInstructors([FromQuery] int? id, [FromQuery(Name = "first_initial")] string? firstInitial)
        {
            try
            {
                // 1. Búsqueda por ID (retorna una lista de 1 solo elemento)
                if (id.HasValue)
                {
                    Instructor? instructor = _repository.GetById(id.Value);
                    if (instructor == null)
                    {
                        return NotFound(new ErrorResponse(2, $"No existe el instructor con ID {id.Value}"));
                    }
                    return Ok(new[] { instructor });
                }

                // 2. Búsqueda por inicial
                if (!string.IsNullOrWhiteSpace(firstInitial))
                {
                    IEnumerable<Instructor> instructorsByInitial = _repository.GetByFirstInitial(firstInitial);
                    return Ok(instructorsByInitial);
                }

                // 3. Sin parámetros: devuelve todos los instructores
                IEnumerable<Instructor> allInstructors = _repository.GetAll();
                return Ok(allInstructors);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponse(1, $"Error interno: {ex.Message}")
                );
            }
        }
        // Expresiones regulares para validaciones (puedes definirlas a nivel de clase)
        private static readonly Regex SoloNumerosRegex = new Regex(@"^[0-9]+$", RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        [HttpPost]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public ActionResult<Instructor> CreateInstructor([FromBody] InstructorNew instructorNew)
        {
            try
            {
                // Controlo que reciba un instructor
                if (instructorNew == null)
                {
                    return BadRequest(new ErrorResponse(3, "Invalid instructor"));
                }

                // Con nopmbre
                if (string.IsNullOrWhiteSpace(instructorNew.Name))
                {
                    return BadRequest(new ErrorResponse(4, "The name of the instructor is mandatory"));
                }

                // El teléfono es obligatorio en API y debe contener solo números 
                if (string.IsNullOrWhiteSpace(instructorNew.Telefono) || !SoloNumerosRegex.IsMatch(instructorNew.Telefono))
                {
                    return BadRequest(new ErrorResponse(4, "The telephone of the instructor is mandatory and must contain only digits"));
                }

                // El email es obligatorio y debe tener formato válido
                if (string.IsNullOrWhiteSpace(instructorNew.Email) || !EmailRegex.IsMatch(instructorNew.Email))
                {
                    return BadRequest(new ErrorResponse(4, "The email is mandatory and must be a valid email address"));
                }

                // la URL de la foto debe ser válida y empezar por http o https
                // Uri.TryCreate: Intenta convertir la cadena en una dirección URL absoluta bien formada (devolviendo true/false si lo consigue)
                // uriResult.Scheme != Uri.UriSchemeHttp Revisa el protocolo (esquema) de la URL almacenada en uriResult. Si el protocolo no es ni http ni https
                if (string.IsNullOrWhiteSpace(instructorNew.PhotoUrl) ||
                    !Uri.TryCreate(instructorNew.PhotoUrl, UriKind.Absolute, out Uri? uriResult) ||
                    (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                {
                    return BadRequest(new ErrorResponse(4, "The photo URL is invalid. It must start with http:// or https://"));
                }

                

                // LLamamos al método para añadirlo en el repositorio
                Instructor? instructor = _repository.Add(instructorNew,out String errorMessage);
                if (instructor==null)
                {
                    return BadRequest(new ErrorResponse(5,errorMessage));
                }
                // Respuesta 201 Created con el objeto Instructor realmente creado, incluyendo el Id
                return StatusCode(StatusCodes.Status201Created, instructor);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    new ErrorResponse(1, "An internal server error occurred"));
            }
        }


        /// <summary>
        /// GET /instructor
        /// GET /instructor?first_initial=M
        /// Devuelve el listado de monitores (filtrado opcionalmente por la inicial del nombre)
        /// </summary>
        /// <param name="firstInitial">Filtro opcional para la inicial del monitor</param>
        // Atiende a: GET /api/instructors/search?firstInitial=M
        //[HttpGet("search")]
        //[ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        //public ActionResult<IEnumerable<Instructor>> FindInstructors([FromQuery(Name = "first_initial")] string? firstInitial)
        //{
        //    try
        //    {
        //        IEnumerable<Instructor> instructors = _repository.GetByFirstInitial(firstInitial);

        //        return Ok(instructors);
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
        //    }
        //}


        //[HttpGet()]
        //[ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        //public ActionResult<IEnumerable<Instructor>> GetAll() // ActionResult: Permite devolver la colección en Ok(dato) como resultados HTTP 200, o devolver un error con StatusCode(500, dato) en caso de fallo
        //{
        //    try
        //    {
        //        IEnumerable<Instructor> instructors = _repository.GetAll();
        //        return Ok(instructors);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Devolvemos 500 si ocurre un fallo no previsto en el servidor
        //        return StatusCode(
        //            StatusCodes.Status500InternalServerError,
        //            new ErrorResponse(1, $"Error interno: {ex.Message}")
        //        );
        //    }
        //}


        //// GET: api/instructors/2
        //[HttpGet("{id:int}")]
        //[ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        //public ActionResult<Instructor> GetInstructorById([FromRoute] int id) //public IActionResult GetInstructorById([FromRoute] int id) // 
        //{

        //    try
        //    {
        //        Instructor? instructor = _repository.GetById(id);
        //        if (instructor != null)
        //        {
        //            return Ok(instructor);
        //        }
        //        return NotFound(new ErrorResponse(2, $"No existe el instructor {id}"));

        //    }
        //    catch (Exception)
        //    {

        //        return BadRequest(new ErrorResponse(1, "Ocurrió un error en el servidor al procesar la solicitud."));
        //    }

        //}

    }
}
