using _4VGymAPI.Models;

namespace _4VGymAPI.Repositories
{
    public class InMemoryActivityRepository
    {
        private List<ActivityNew> _activities = new();

        private readonly InMemoryActivityTypeRepository _repositoryActivityType;
        private readonly InMemoryInstructorRepository _repositoryInstructor;

        private int _nextId = 1; // Para el autonumérico

        public InMemoryActivityRepository(InMemoryActivityTypeRepository repositoryActivityType, InMemoryInstructorRepository repositoryInstructor)
        {
            _repositoryActivityType = repositoryActivityType;
            _repositoryInstructor = repositoryInstructor;
        }


        public Activity? Add(ActivityNew activity, out List<ErrorResponse> errorResponse)
        {

            errorResponse = new List<ErrorResponse>();

            // Validamos todas los referente a las entidades cruzadas
            // Validación 1: Que exista el tipo de actividad
            if (_repositoryActivityType.GetById(activity.ActivityTypeId) == null)
            {
                errorResponse.Add(new ErrorResponse(34, "EL tipo de actividad debe de funcionar"));
            }

            // Validacion 2: Que existan todos los monitores
            //foreach (int instuctorId in activity.InstructorsId)
            //{
            //    if (_repositoryInstructor.GetById(instuctorId) == null)
            //    {
            //        errorResponse.Add(new ErrorResponse(35, "El instructor: "+ instuctorId + " no existe"));
            //    }
            //}
            var missingInstructors = activity.InstuctorsId
                                   .Where(instructorId => _repositoryInstructor.GetById(instructorId) == null);

            foreach (var instructorId in missingInstructors)
            {
                errorResponse.Add(new ErrorResponse(35, $"El instructor: {instructorId} no existe"));
            }

            if (errorResponse.Count > 0)
            {
                return null;
            }

            // Asignamos el ID autoincremental en la capa de datos, o sea que machacamos el que venia
            activity.Id = _nextId++;

            _activities.Add(activity);

            // Devolvemos el objeto con los datos enriquecidos
            Activity newActivity = new Activity {
                ActivityType = _repositoryActivityType.GetById(activity.ActivityTypeId),
                Instructors = _repositoryInstructor.GetByIds(activity.InstuctorsId),
                MaxParticipants = activity.MaxParticipants,
                DateStart = activity.DateStart,
                DateEnd = activity.DateEnd
            };
            return newActivity;
        }
    }

}
