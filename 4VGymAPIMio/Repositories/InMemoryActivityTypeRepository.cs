using _4VGymAPI.Models;

namespace _4VGymAPI.Repositories
{
    public class InMemoryActivityTypeRepository
    {
        private List<ActivityType> _activityTypes = new();
        public InMemoryActivityTypeRepository()
        {
            SeedData(); // Precargamos algunos datos de prueba
        }
        private void SeedData()
        {
            //_activityTypes = new List<ActivityType>()
            //{
            //    new ActivityType (10, "BodyPump",  2 ),
            //    new ActivityType ( 20, "Spinning",  1 ),
            //    new ActivityType { Id = 30, Name = "Pilates", NumberMonitors = 1 }
            //};
            //_activityTypes.Add(new ActivityType ( 40, "CrossFit", 3));
        }
        // Método para obtener todos los tipos de actividades
        public IEnumerable<ActivityType> GetAll() // Al declarar como tipo de retorno IEnumerable<ActivityType>, la vista externa solo puede leer los datos (recorrerlos con un foreach o filtrarlos con LINQ), evitando que se agreguen o eliminen elementos de la lista privada desde fuera
        {
            return _activityTypes;
        }
    }
}
