using AutenticacionNetCore.Datos;
using Microsoft.EntityFrameworkCore;
using RepositorioRoles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddCors(p => p.AddPolicy("corspolicy", Build => {
    Build.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        // Otros ajustes opcionales...
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionRoles")));

builder.Services.AddScoped<IRepositorioRoles, RepositorioRoles.RepositorioRoles>();
builder.Services.AddScoped<IAD, AD>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corspolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
