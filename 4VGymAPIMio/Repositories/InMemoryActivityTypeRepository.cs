using _4VGymAPI.Models;

namespace _4VGymAPI.Repositories
{
    public class InMemoryActivityTypeRepository
    {
        // Diccionario interno: Clave = Id (long), Valor = Objeto ActivityType
        //private readonly Dictionary<long, ActivityType> _activityTypes = new();
        private Dictionary<long, ActivityType> _activityTypes = new();
        public InMemoryActivityTypeRepository()
        {
            // Precargamos algunos datos de prueba
            SeedData();
        }

        private void SeedData()
        {
            _activityTypes = new Dictionary<long, ActivityType>()
            {
                [10] = new ActivityType { Id = 10, Name = "BodyPump", NumberMonitors = 2 },
                [20] = new ActivityType { Id = 20, Name = "Spinning", NumberMonitors = 1 },
                [30] = new ActivityType { Id = 30, Name = "Pilates", NumberMonitors = 1 }
            };
            _activityTypes.Add(40, new ActivityType { Id = 40, Name = "CrossFit", NumberMonitors = 3 });

            //var initialList = new List<ActivityType>
            //{
            //    new() { Id = 10, Name = "BodyPump", NumberMonitors = 2 },
            //    new() { Id = 20, Name = "Spinning", NumberMonitors = 1 },
            //    new() { Id = 30, Name = "Pilates", NumberMonitors = 1 },
            //    new() { Id = 40, Name = "CrossFit", NumberMonitors = 3 }
            //};
            //foreach (var activity in initialList)0
            //{
            //    _activityTypes.Add(activity.Id, activity);
            //}
        }

        // Método para obtener todos los tipos de actividades
        public IEnumerable<ActivityType> GetAll()
        {
            return _activityTypes.Values;
        }
    }
}
