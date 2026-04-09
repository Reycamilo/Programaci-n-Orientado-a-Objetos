
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using PersonasApp.Extensions;
using PersonsApp.Database;
using PersonsApp.Services.Persons;
// using PersonsApp.Services.Persons;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container.
builder.Services.AddDbContext<PersonsDbContext>( options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddScoped
// builder.Services.AddSingleton
builder.Services.AddAuthenticationconfig(builder.Configuration);
builder.Services.AddTransient<IPersonService, PersonsService>();
builder.Services.AddOpenApi();
builder.Services.AddControllers(); // agregando los controladores.



// Creando La App
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


// Configuracion de la App
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers(); // mapeando los controladores.


// Iniciando la app.
app.Run();

