
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container.
builder.Services.AddDbContext<PersonsDbContext>( options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnecion")));
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

