
using _4VGymAPI.Repositories;

var builder = WebApplication.CreateBuilder(args); // Inicializa una instancia de WebApplicationBuilder en ASP.NET Core, preparando la infraestructura base necesaria para configurar y levantar una aplicación web o API.

// Añadir los controladores
builder.Services.AddControllers();

// Añadir el soporte a OpenAPI:  https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// AÑADIR ESTO
// // Inyección de la clase de repositorio concreta (Singleton para mantener los datos en memoria)
builder.Services.AddSingleton<InMemoryActivityTypeRepository>(); // Inyecta la clase de repositorio concreta (InMemoryActivityTypeRepository) como un servicio Singleton, lo que significa que se creará una única instancia de esta clase y se compartirá en toda la aplicación. Esto es útil para mantener los datos en memoria durante la vida de la aplicación, evitando la necesidad de crear múltiples instancias del repositorio y asegurando que todos los controladores y servicios que dependan de este repositorio trabajen con la misma fuente de datos.


// Contruir la aplicacion
var app = builder.Build();

if (app.Environment.IsDevelopment()) // Si estamos en modo desarrollo, habilitamos el endpoint de OpenAPI y Swagger UI
{
    // 1. Expone el endpoint del documento OpenAPI nativo (ej: /openapi/v1.json)
    app.MapOpenApi();

    // Si instalaste Swashbuckle para seguir usando Swagger UI:
    app.UseSwaggerUI(options =>
    {
        // Le indicamos dónde está el archivo JSON generado por .NET
        options.SwaggerEndpoint("/openapi/v1.json", "RestAPI for 4VGym v1");
    });

}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
