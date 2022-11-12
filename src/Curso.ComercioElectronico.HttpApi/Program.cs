using Curso.ComercioElectronico.Application;
using Curso.ComercioElectronico.Domain;
using Curso.ComercioElectronico.Infraestructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configuraciones de Dependencias
builder.Services.AddScoped<ComercioElectronicoDbContext>();
//builder.Services.AddScoped<IUnitOfWork, ComercioElectronicoDbContext>();

builder.Services.AddTransient<IMarcaRepository, MarcaRepository>();
builder.Services.AddTransient<IMarcaAppService, MarcaAppService>();
builder.Services.AddTransient<IAutorRepository, AutorRepository>();
builder.Services.AddTransient<IAutorAppService, AutorAppService>();
builder.Services.AddTransient<IEditorialRepository, EditorialRepository>();
builder.Services.AddTransient<IEditorialAppService, EditorialAppService>();
builder.Services.AddTransient<ILibroRepository, LibroRepository>();
builder.Services.AddTransient<ILibroAppService, LibroAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
