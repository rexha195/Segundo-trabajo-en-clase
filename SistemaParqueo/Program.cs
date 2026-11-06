using SistemaParqueo.Models;
using SistemaParqueo.Repositories.Interfaces;
using SistemaParqueo.Repositories.Context;
var builder = WebApplication.CreateBuilder(args);
// Usaremos los de Context (Base de datos) para este API
// using SistemaParqueo.Repositories.Json; 

// ... (código existente de builder)

builder.Services.AddControllersWithViews(); // Asegúrate de que esta línea esté
// Registramos el contexto de BD
builder.Services.AddDbContext<ParqueoContext>(); 

// Inyección de dependencias para los repositorios
builder.Services.AddScoped<IAutomovilesRepository, AutomovilesContextRepository>();
builder.Services.AddScoped<IParqueosRepository, ParqueosContextRepository>();
builder.Services.AddScoped<IIngresosRepository, IngresosContextRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews()
.AddJsonOptions(options =>
    {
        // Esto le dice al serializador JSON que ignore las referencias circulares
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
