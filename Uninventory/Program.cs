using Microsoft.EntityFrameworkCore;
using Uninventory.DBContext;
using Uninventory.Interfaces;
using Uninventory.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<UninventoryDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();



builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAnyOrigin", policy =>
  {
    policy.AllowAnyOrigin()  // Allows any origin
          .AllowAnyMethod()  // Allows any HTTP method (GET, POST, etc.)
          .AllowAnyHeader(); // Allows any header
  });
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
