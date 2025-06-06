using Microsoft.EntityFrameworkCore;
using Uninventory.Common.Exceptions;
using Uninventory.Interfaces;
using Uninventory.Middleware;
using Uninventory.Persistence;
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
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserRole, UserRoleService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IAuthService , AuthService>();
builder.Services.AddScoped<ISessionService , SessionService>();






var app = builder.Build();

app.UseCors(builer =>
{
  builer
  .AllowAnyOrigin()
  .AllowAnyMethod()
  .AllowAnyHeader();
});







// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}







app.UseCors("AllowAnyOrigin");

//middlewares
//app.UseMiddleware<ExceptionsMiddleware>();
//app.UseMiddleware<SessionMiddleware>();


app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
